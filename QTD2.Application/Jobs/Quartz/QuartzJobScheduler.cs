
using QTD2.Infrastructure.Jobs.Interfaces;
using System;
using System.Threading.Tasks;

using QTD2.Application.Jobs.Notifications;

using Quartz.Spi;
using Quartz;
using Quartz.Impl;

namespace QTD2.Application.Jobs.Quartz
{
    public class QuartzJobScheduler : IJobScheduler
    {
        IScheduler _scheduler;

        public QuartzJobScheduler(IJobFactory myJobFactory)
        {
            _scheduler = new StdSchedulerFactory().GetScheduler().Result;
            _scheduler.JobFactory = myJobFactory;
        }

        public async Task Start()
        {
            try
            {
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                IScheduler scheduler = await schedulerFactory.GetScheduler();

                ////had to use http://www.cronmaker.com for the crons?  
                await AddCertificationExpirationNotificationJob(scheduler);
                //AddClassScheduleNotificationJob(scheduler, "ClassScheduleNotification", "0 * 15 * * ?"); 

                await scheduler.Start();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        public async Task AddCertificationExpirationNotificationJob(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<CertificationExpirationNotificationJob>()
                   .WithIdentity(typeof(CertificationExpirationNotificationJob).Name, "Appjob")
                   .Build();
            ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity(typeof(CertificationExpirationNotificationJob).Name + "_trigger", "Appjob")
                  .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
                  .Build();
            await scheduler.ScheduleJob(job, trigger);
        }

        public void AddClassScheduleNotificationJob(IScheduler scheduler, string identity, string cron)
        {
            IJobDetail job = JobBuilder.Create<ClassScheduleNotificationJob>()
                      .WithIdentity(identity)
                      .Build();
            ITrigger trigger = TriggerBuilder.Create()
                   .WithIdentity(identity + "_Trigger")
                   .WithSchedule(CronScheduleBuilder.CronSchedule(cron))
                   .Build();
            scheduler.ScheduleJob(job, trigger);
        }

    }
}
