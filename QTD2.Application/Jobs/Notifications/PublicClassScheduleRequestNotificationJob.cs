using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using QTD2.Data;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model.QtdUser;
using QTD2.Infrastructure.Model.TaskReQualificationEmp;
using QTD2.Infrastructure.Notification.Settings;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Jobs.Notifications
{
    public class PublicClassScheduleRequestNotificationJob : IJob
    {
        public bool RunAtStartup => false;

        private readonly IInstanceFetcher _instanceFetcher;
        private ILogger<PublicClassScheduleRequestNotificationJob> _logger;
        private IDbContextBuilder _dbContextBuilder;
        public PublicClassScheduleRequestNotificationJob(IInstanceFetcher instanceFetcher, ILogger<PublicClassScheduleRequestNotificationJob> logger, IDbContextBuilder dbContextBuilder)
        {
            _instanceFetcher = instanceFetcher;
            _logger = logger;
            _dbContextBuilder = dbContextBuilder;
        }

        async System.Threading.Tasks.Task IJob.Execute(IJobExecutionContext context)
        {
            await ExecuteAsync();
        }
        public async System.Threading.Tasks.Task ExecuteAsync()
        {
            var instances = await _instanceFetcher.GetAllActiveInstancesAsync();
            foreach (var instance in instances)
            {
                try
                {
                    _logger.LogInformation($"Public Class Schedule Request job for {instance.DatabaseName} beginning");
                    var db = _dbContextBuilder.BuildQtdContext(instance.DatabaseName);

                    var clientSettings_Notification = (await db.ClientSettings_Notifications.Include("CustomSettings").Include("Steps.CustomSettings").Where(n => n.Name == "Public Class Schedule Request").ToListAsync()).FirstOrDefault();

                    var notificationStep = await db.ClientSettings_Notification_Steps.Where(step => step.ClientSettingsNotificationId == clientSettings_Notification.Id).FirstOrDefaultAsync();
                    var notificationCustomSettings = await db.ClientSettings_Notification_Step_CustomSettings.Where(customSetting => customSetting.ClientSettingsNotificationStepId == notificationStep.Id).ToListAsync();

                    if (clientSettings_Notification != null && clientSettings_Notification.Enabled)
                    {
                        var startDate = DateTime.Today;
                        var stepEmailFrequency = notificationCustomSettings.Where(r => r.Key == "Email Frequency").FirstOrDefault().Value;
                        var otherEmails = notificationCustomSettings.Where(r => r.Key == "Send To Others").FirstOrDefault().Value;

                        switch (stepEmailFrequency)
                            {
                                case "Daily":
                                    startDate = startDate.Date.AddDays(-1);
                                    await SendNotification(startDate, notificationStep, otherEmails, db);
                                    break;

                                case "Weekly":
                                    if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
                                    {
                                        startDate = startDate.Date.AddDays(-7);
                                        await SendNotification(startDate, notificationStep, otherEmails, db);
                                    }
                                    break;
                                case "As Submitted":
                                    break;
                        }

                    }

                if (clientSettings_Notification.Enabled == false) return;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Public Class Schedule Request for {instance.DatabaseName} failed {e}", e);
                }
            }
        }

        public async System.Threading.Tasks.Task SendNotification(DateTime startDate, ClientSettings_Notification_Step notificationStep, string otherEmails, QTDContext db)
        {
            var publicClassScheduleRequestNotifications = new List<PublicClassScheduleRequestNotification>();
            var publicClassScheduleRequest = await db.PublicClassScheduleRequests.Where(x => x.CreatedDate >= startDate).OrderBy(x => x.Id).LastOrDefaultAsync();
            var qTDUsers = await db.QTDUsers.Where(x => x.Active).ToListAsync();
            if (publicClassScheduleRequest != null)
            {
                var notifications = await db.Notifications
                .OfType<PublicClassScheduleRequestNotification>()
                .Where(n => n.PublicClassScheduleRequestNotification_PublicClassScheduleRequestId == publicClassScheduleRequest.Id)
                .Where(n => n.DueDate >= startDate)
                .ToListAsync();
                if (!notifications.Any())
                {
                    foreach (var qTDUser in qTDUsers)
                    {
                        publicClassScheduleRequestNotifications.Add(new PublicClassScheduleRequestNotification(DateTime.UtcNow, publicClassScheduleRequest.Id, qTDUser.PersonId, notificationStep.Id));
                    }
                }

                var emails = otherEmails.Split(',');
                if (!emails.IsNullOrEmpty() && !notifications.Any())
                {                    
                    foreach (var email in emails)
                    {                   
                       publicClassScheduleRequestNotifications.Add(new PublicClassScheduleRequestNotification(DateTime.UtcNow, publicClassScheduleRequest.Id, email, notificationStep.Id));
                    }                    
                }
            }
            db.Notifications.AddRange(publicClassScheduleRequestNotifications);
            await db.SaveChangesAsync();
        }
    }
}
