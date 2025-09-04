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
    public class InactivateExpiredEmployeePositionsJob : IJob
    {
        public bool RunAtStartup => false;

        private readonly IInstanceFetcher _instanceFetcher;
        private ILogger<InactivateExpiredEmployeePositionsJob> _logger;
        private IDbContextBuilder _dbContextBuilder;

        public InactivateExpiredEmployeePositionsJob(
            IInstanceFetcher instanceFetcher,
            ILogger<InactivateExpiredEmployeePositionsJob> logger,
            IDbContextBuilder dbContextBuilder
            )
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
                    _logger.LogInformation($"Employee Position Expiration job for {instance.DatabaseName} beginning");
                    var db = _dbContextBuilder.BuildQtdContext(instance.DatabaseName);

                    var employeePositions = db.EmployeePositions.Where(r => r.Active && !r.Deleted && r.EndDate < DateOnly.FromDateTime(DateTime.Today));
                    foreach (var empPosition in employeePositions)
                    {
                        empPosition.Deactivate();
                    }
                    db.UpdateRange(employeePositions);
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Employee Position Expiration for {instance.DatabaseName} failed {e}", e);
                }
            }
        }
    }
}
