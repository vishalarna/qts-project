using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Jobs.Interfaces
{
    public interface IJobScheduler
    {
        public Task Start();
        void AddClassScheduleNotificationJob(IScheduler scheduler, string identity,  string cron);
        public Task AddCertificationExpirationNotificationJob(IScheduler scheduler);
    }
}
