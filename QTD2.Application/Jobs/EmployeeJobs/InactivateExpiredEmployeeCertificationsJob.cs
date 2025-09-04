using QTD2.Infrastructure.Database.Interfaces;
using Quartz;
using System.Linq;
using Quartz.Spi;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using Microsoft.Extensions.Logging;
using QTD2.Domain.Interfaces.Service.Core;

namespace QTD2.Application.Jobs.EmployeeJobs
{
    public class InactivateExpiredEmployeeCertificationsJob : IJob
    {
        public bool RunAtStartup => false;

        private readonly IInstanceFetcher _instanceFetcher;
        private IDbContextBuilder _dbContextBuilder;
        private ILogger<InactivateExpiredEmployeeCertificationsJob> _logger;

        public InactivateExpiredEmployeeCertificationsJob(
            IInstanceFetcher instanceFetcher,
            IDbContextBuilder dbContextBuilder,
            ILogger<InactivateExpiredEmployeeCertificationsJob> logger)
        {
            _instanceFetcher = instanceFetcher;
            _dbContextBuilder = dbContextBuilder;
            _logger = logger;
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
                    _logger.LogInformation($"Employee Certification Expiration job for {instance.DatabaseName} beginning");
                    var db = _dbContextBuilder.BuildQtdContext(instance.DatabaseName);

                    var employeeCertifications = db.EmployeeCertifications.Where(r => r.Active && !r.Deleted && r.ExpirationDate <  DateOnly.FromDateTime(DateTime.Today));
                    foreach (var empCertification in employeeCertifications)
                    {
                        empCertification.Deactivate();
                    }

                    db.UpdateRange(employeeCertifications);
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Employee Certification for {instance.DatabaseName} failed {e}", e);
                }
            }
        }
    }
}
