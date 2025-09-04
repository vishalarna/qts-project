using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using QTD2.Application.Interfaces.Services.QTD;
using QTD2.Application.Jobs.Notifications;
using QTD2.Data;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model.QtdUser;
using QTD2.Infrastructure.Notification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnPublicClassScheduleRequestSubmittedHandler : INotificationHandler<OnPublicClassScheduleRequestSubmitted>
    {
        private readonly IInstanceFetcher _instanceFetcher;
        private IDbContextBuilder _dbContextBuilder;
        private readonly QTDContext _qTDContext;
        public OnPublicClassScheduleRequestSubmittedHandler(IInstanceFetcher instanceFetcher, IDbContextBuilder dbContextBuilder,QTDContext qTDContext)
        {
            _instanceFetcher = instanceFetcher;
            _dbContextBuilder = dbContextBuilder;
            _qTDContext = qTDContext;
        }

        public async System.Threading.Tasks.Task Handle(OnPublicClassScheduleRequestSubmitted notification, CancellationToken cancellationToken)
        {
            var publicClassScheduleRequestNotifications = new List<PublicClassScheduleRequestNotification>();
            var clientSettings_Notification = (await _qTDContext.ClientSettings_Notifications.Include("CustomSettings").Include("Steps.CustomSettings").Where(n => n.Name == "Public Class Schedule Request").ToListAsync()).FirstOrDefault();

            if (clientSettings_Notification.Steps.SelectMany(x => x.CustomSettings).Where(x => x.Value == "As Submitted").Any()) 
            {
                var notificationStep = clientSettings_Notification?.Steps?.FirstOrDefault();
                var otherEmails = notificationStep.CustomSettings.Where(x => x.Key == "Send To Others").FirstOrDefault().Value;
                var qTDUsers = await _qTDContext.QTDUsers.Where(x => x.Active).ToListAsync();
                foreach (var qTDUser in qTDUsers)
                {
                    publicClassScheduleRequestNotifications.Add(new PublicClassScheduleRequestNotification(DateTime.UtcNow, notification.PublicClassScheduleRequest.Id, qTDUser.PersonId, clientSettings_Notification.Steps.FirstOrDefault().Id));
                }
                if (!otherEmails.IsNullOrEmpty())
                {
                    var emails = otherEmails.Split(","); 
                    foreach (var email in emails)
                    {
                        publicClassScheduleRequestNotifications.Add(new PublicClassScheduleRequestNotification(DateTime.UtcNow, notification.PublicClassScheduleRequest.Id, email, notificationStep.Id));
                    }
                }

                await _qTDContext.Notifications.AddRangeAsync(publicClassScheduleRequestNotifications);
                await _qTDContext.SaveChangesAsync();
            }

        }


    }
}
