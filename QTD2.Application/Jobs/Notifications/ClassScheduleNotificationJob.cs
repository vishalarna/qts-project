using QTD2.Infrastructure.Database.Interfaces;
using Quartz;
using System.Linq;
using Quartz.Spi;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using QTD2.Application.Interfaces.Services.Shared;
using Microsoft.Extensions.Logging;
using QTD2.Application.Interfaces.Services.QTD;
using QTD2.Data;
using ClosedXML.Parser;

namespace QTD2.Application.Jobs.Notifications
{
    public class ClassScheduleNotificationJob : IJob
    {
        public bool RunAtStartup => false;

        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IDbContextBuilder _dbContextBuilder;
        private readonly IJobNotificationService _jobNotificationService;
        private ILogger<NotificationJob> _logger;

        public ClassScheduleNotificationJob(
            IInstanceFetcher instanceFetcher, 
            IJobNotificationService jobNotificationService,
            IDbContextBuilder dbContextBuilder, 
            ILogger<NotificationJob> logger)
        {
            _instanceFetcher = instanceFetcher;
            _jobNotificationService = jobNotificationService;
            _dbContextBuilder = dbContextBuilder;
            _logger = logger;
        }

        async System.Threading.Tasks.Task IJob.Execute(IJobExecutionContext context)
        {
            await ExecuteAsync();
        }

        public async System.Threading.Tasks.Task ExecuteAsync()
        {
            //foreach database/instance

            var instances = await _instanceFetcher.GetAllActiveInstancesAsync();
            foreach(var instance in instances)
            {
                try 
                {
                    var qtdContext = _dbContextBuilder.BuildQtdContext(instance.DatabaseName);

                    var notificationSetting = qtdContext.ClientSettings_Notifications.Include(cs => cs.CustomSettings).FirstOrDefault(r => r.Name == "Class Schedule");

                    if (notificationSetting.Enabled == false) return;

                    var notificationStep = qtdContext.ClientSettings_Notification_Steps.Where(step => step.ClientSettingsNotificationId == notificationSetting.Id).FirstOrDefault();
                    var notificationCustomSettings = qtdContext.ClientSettings_Notification_Step_CustomSettings.Where(customSetting => customSetting.ClientSettingsNotificationStepId == notificationStep.Id).ToList();
                    if (notificationSetting != null)
                    {
                        var emailFrequency = notificationCustomSettings.Where(r => r.Key == "Email Frequency").FirstOrDefault().Value;
                        var scheduleRange = notificationCustomSettings.Where(r => r.Key == "Schedule for next").FirstOrDefault().Value;

                        //if Email Frequency == Monthly and today.date != 1 -> continue else ok
                        //if Email Frequency == Weekly and today.dow != 1 -> continue else ok
                        //if Email Frequency == Daily -> ok

                        var classSchedules = qtdContext.ClassSchedules.Include("ClassSchedule_Employee.Employee.Person");
                        if (emailFrequency == "Monthly" && DateTime.Today.Day != 1)
                        {
                            continue;
                        }
                        else
                        {
                            var classes = classSchedules.Where(dd => dd.StartDateTime <= DateTime.Today && dd.EndDateTime >= DateTime.Today.AddMonths(Convert.ToInt32(scheduleRange))).ToList();
                            await SendNotifications(classes, notificationStep, qtdContext);
                        }
                        if (emailFrequency == "Weekly" && DateTime.Today.DayOfWeek != DayOfWeek.Monday)
                        {
                            continue;
                        }
                        else
                        {
                            // query the classes that are within the schedule for next range
                            //for example if today is 06/01 and schedule for next is 5 then we select classes from 06/01 to 06/06
                            //then send notification foreach ClassSchedule_Employee
                            var classes = classSchedules.Where(dd => dd.StartDateTime <= DateTime.Today && dd.EndDateTime >= DateTime.Today.AddDays(Convert.ToInt32(scheduleRange) * 7)).ToList();
                            await SendNotifications(classes, notificationStep, qtdContext);
                        }
                    }
                }
                catch(Exception e)
                {
                    _logger.LogError($"Failed to run job for {instance.DatabaseName} {e}", e);
                }
            }
        }

        public async System.Threading.Tasks.Task SendNotifications(List<ClassSchedule> classSchedules, ClientSettings_Notification_Step notificationStep, QTDContext qtdContext)
        {
            if(classSchedules.Count > 0)
            {
                foreach (var cs in classSchedules)
                {
                    foreach (var classEmployee in cs.ClassSchedule_Employee)
                    {
                        if (classEmployee.IsEnrolled)
                        {
                            // Create Notification
                            ClassScheduleNotification notification = new ClassScheduleNotification(DateTime.Now, classEmployee.Id, classEmployee.Employee.PersonId, notificationStep.Id);
                            notification.Status = NotificationSendStatus.Sending;
                            qtdContext.Notifications.Add(notification);
                            await qtdContext.SaveChangesAsync();

                            // Send Notification
                            var success = await _jobNotificationService.SendClassScheduleNotification(classEmployee.EmployeeId, 1, qtdContext);
                            if (success)
                            {
                                notification.Send();
                            }
                            else
                            {
                                notification.Error();
                            }

                            try
                            {
                                qtdContext.Notifications.Update(notification);
                                await qtdContext.SaveChangesAsync();
                            }
                            catch (Exception e)
                            {
                                _logger.LogError($"Failed to update notification {e}", e);
                            }
                        }
                    }
                }
            }
        }
    }
}
