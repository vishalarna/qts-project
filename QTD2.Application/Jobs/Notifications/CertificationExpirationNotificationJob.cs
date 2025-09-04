using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Database.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QTD2.Infrastructure.Helpers;
using System.Runtime.ConstrainedExecution;
using DocumentFormat.OpenXml.Presentation;

namespace QTD2.Application.Jobs.Notifications
{
    public class CertificationExpirationNotificationJob : IJob
    {
        public bool RunAtStartup => false;

        private readonly IInstanceFetcher _instanceFetcher;
        private ILogger<CertificationExpirationNotificationJob> _logger;
        private IDbContextBuilder _dbContextBuilder;

        public CertificationExpirationNotificationJob(
            IInstanceFetcher instanceFetcher,
            ILogger<CertificationExpirationNotificationJob> logger,
            IDbContextBuilder dbContextBuilder
            )
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
                    _logger.LogInformation($"Certification expiration job for {instance.DatabaseName} beginning");
                    var db = _dbContextBuilder.BuildQtdContext(instance.DatabaseName);

                    var clientSettings_Notification = (await db.ClientSettings_Notifications.Include("CustomSettings").Include("Steps.CustomSettings").Where(n => n.Name == "Certification Expiration").ToListAsync()).First();

                    var certSetting = clientSettings_Notification.CustomSettings.Where(r => r.Key == "CertificationType").FirstOrDefault();

                    if (certSetting == null) continue;

                    var certs = certSetting.Value.Split(',');

                    if (certs.Length == 0) continue;

                    if (clientSettings_Notification.Active == false) continue;


                    // Longest Expiring Within step
                    var maxExpiringWithinValue = clientSettings_Notification.Steps.Select(s => int.Parse(s.CustomSettings.Where(r => r.Key == "Expiring Within").FirstOrDefault().Value)).Max();

                    // EmployeeCertifications expiring within max days
                    var employeeCertificationsExpiringWithinMaxDate = await db.EmployeeCertifications
                        .Include("Employee.Person")
                        .Where(r => r.ExpirationDate.HasValue && DateOnly.FromDateTime(DateTime.UtcNow) < r.ExpirationDate && r.ExpirationDate <= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(maxExpiringWithinValue)))
                        .Where(r => (certs.Select(s => s.ToUpper()).Contains("NERC") && r.Certification.CertifyingBody.Name == "NERC") || certs.Select(s => s.ToUpper()).Contains(r.Certification.CertAcronym.ToUpper()))
                        .ToListAsync();

                    var certificationExpiringNotifications = new List<CertificationExpiringNotification>();
                    foreach (var employeeCertification in employeeCertificationsExpiringWithinMaxDate)
                    {
                        // determine most fitting step
                        // chooses based on smallest ExpiringWithin, then if multiple, smallest frequency timeframe, then if multiple, by system step ID (for reliability)
                        var step = clientSettings_Notification.Steps
                            .Where(s => employeeCertification.ExpirationDate <= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(int.Parse(s.CustomSettings.Where(r => r.Key == "Expiring Within").FirstOrDefault().Value))))
                            .OrderBy(s => int.Parse(s.CustomSettings.Where(r => r.Key == "Expiring Within").FirstOrDefault().Value))
                            .ThenBy(s => s.CustomSettings.Where(r => r.Key == "Email Frequency").FirstOrDefault().Value == "Daily" ? 1 : (s.CustomSettings.Where(r => r.Key == "Email Frequency").FirstOrDefault().Value == "Weekly" ? 2 : 3))
                            .ThenBy(s => s.Id)
                            .FirstOrDefault();

                        //Check if we should send because of frequency(query notifications table)
                        //determine look-back date to check for prior notifications for this EmployeeCert between
                        var startDate = DateTime.UtcNow;
                        var stepEmailFrequency = step.CustomSettings.Where(r => r.Key == "Email Frequency").FirstOrDefault().Value;
                        if (stepEmailFrequency == "Daily")
                        {
                            startDate = startDate.AddDays(-1);
                        }
                        else if (stepEmailFrequency == "Weekly")
                        {
                            startDate = startDate.AddDays(-7);
                        }
                        else if (stepEmailFrequency == "Monthly")
                        {
                            startDate = startDate.AddMonths(-1);
                        }

                        var notifications = await db.Notifications
                            .OfType<CertificationExpiringNotification>()
                            .Where(n => n.EmployeeCertificationId == employeeCertification.Id)
                            .Where(n => n.DueDate >= startDate)
                            .ToListAsync();

                        if (notifications.Count() == 0)
                        {
                            certificationExpiringNotifications.Add(new CertificationExpiringNotification(DateTime.UtcNow, employeeCertification.Id, employeeCertification.Employee.PersonId, step.Id));
                        }
                    }

                    db.Notifications.AddRange(certificationExpiringNotifications);
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Certification Expiration for {instance.DatabaseName} failed {e}", e);
                }
            }
        }
    }
}

