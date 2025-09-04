using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using QTD2.Application.Startup.Middleware.Extensions;
using QTD2.Infrastructure.ErrorHandler;
using QTD2.Infrastructure.Hashing.Interfaces;
using QTD2.Infrastructure.JWT;
using QTD2.Infrastructure.Notification;
using QTD2.Infrastructure.Notification.Interfaces;
using QTD2.Infrastructure.Notification.Notifications;
using QTD2.Infrastructure.Notification.Settings;
using Serilog;
using System.Linq;
using QTD2.Domain.Exceptions;
using Org.BouncyCastle.Utilities.Encoders;

namespace QTD2.Application.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly ErrorHandlerSettings _errorHandlerSettings;
        private readonly NotificationSettings _notificationSettings;
        private readonly INotifierFactory _notifierFactory;
        private readonly IHasher _hasher;

        public ErrorHandlingMiddleware(
            RequestDelegate next, 
            IWebHostEnvironment env, 
            ILogger<ErrorHandlingMiddleware> logger,
            IOptions<ErrorHandlerSettings> options,
            IOptions<NotificationSettings> notificationSettingOptions,
            INotifierFactory notifierFactory,
            IHasher hasher)
        {
            _next = next;
            _logger = logger;
            _env = env;
            _errorHandlerSettings = options.Value;
            _notificationSettings = notificationSettingOptions.Value;
            _notifierFactory = notifierFactory;
            _hasher = hasher;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            // identify type of error

            // log full error message

            // if UAT/Dev -> return full error

            // if Prod -> return generic error
            Log.ForContext<ErrorHandlingMiddleware>().Fatal(ex.StackTrace);
            Log.ForContext<ErrorHandlingMiddleware>().Fatal(ex.Message);

            var e = ex.InnerException;

            while (e != null)
            {
                Log.ForContext<ErrorHandlingMiddleware>().Fatal(e.StackTrace);
                Log.ForContext<ErrorHandlingMiddleware>().Fatal(e.Message);
                e = e.InnerException;
            }

            context.Response.StatusCode = (int)GetHttpStatusCode(ex);
            if (_errorHandlerSettings.SendErrorServiceEmails && 
                ex is not ValidationException && 
                ex.Message != "InvalidRefreshJWT" && 
                (ex is not QTDServerException qtdEx || qtdEx.SendEmail))
            {
                await SendErrorEmailAsync(context, ex);
            } 


            if (_env.EnvironmentName.ToLower() == "production")
            {
                if (ex is QTDServerException)
                {
                    var qEx = (ex as QTDServerException);
                    if (!qEx.UseGenericMessage)
                    {
                        await context.Response.AddApplicationError(getExceptionMessage(ex));
                    }
                    else
                    {
                        await context.Response.AddApplicationError(getGenericErrorMessage(ex));
                    }
                }
                else
                {
                    await context.Response.AddApplicationError(getGenericErrorMessage(ex));
                }
            }
            else
            {
                await context.Response.AddApplicationError(getExceptionMessage(ex));
            }
        }

        private async Task SendErrorEmailAsync(HttpContext context, Exception ex)
        {
            string emailBody = $"<strong>Error occurred:</strong><br/>" +
                      $"<strong>Exception Message:</strong> {getExceptionMessage(ex)}<br/>" +
                      $"<strong>Stack Trace:</strong>{ex.StackTrace}<br/>" +
                      $"<strong>Request Path:</strong> {context.Request.Path}<br/>" +
                      $"<strong>User:</strong> {(context.User.Identity.IsAuthenticated ? context.User.Identity.Name : "Anonymous")}<br/>" +
                      $"<strong>Instance:</strong> {context.User.Claims.Where(c => c.Type == Domain.CustomClaimTypes.InstanceName).Select(c => c.Value).SingleOrDefault()}<br/>";
            if (context.Request.Query.ContainsKey("id"))
            {
                var id = context.Request.Query["id"];
                var hashedId = _hasher.Encode(id.ToString());
                emailBody += $"<strong>Hashed ID:</strong> {hashedId}<br/>";
            }

            var emailNotification = new Infrastructure.Notification.Notifications.EmailNotification(
                emailBody.ToString(), 
                "Application Error",   
                NotificationMethod.Email,
                new List<string> { _notificationSettings.MimeKitEmailSettings.DefaultFrom }
            );

            var notifier = _notifierFactory.GetNotifier(emailNotification);
            await notifier.NotifyAsync(emailNotification);
        }

        private string getGenericErrorMessage(Exception ex)
        {
            if (ex is ValidationException)
            {
                return ex.Message;
            }
            else
            {
                return "This is an error";
            }
        }

        private HttpStatusCode GetHttpStatusCode(Exception ex)
        {
            if (ex is ValidationException)
            {
                return HttpStatusCode.UnprocessableEntity;
            }
            else if (ex is UnauthorizedAccessException)
            {
                return HttpStatusCode.Forbidden;
            }
            else if (ex is QTDServerException)
            {
                return (ex as QTDServerException).StatusCode;
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        private string getExceptionMessage(Exception ex)
        {

            var e = ex;
            string message = e.Message;

            while (e.InnerException != null)
            {
                e = e.InnerException;
                message += Environment.NewLine + e.Message;
            }

            return message;
        }

    }
}
