using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Data;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Database.Interfaces;
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Jobs.ClassScheduleJobs
{
    public class EMPSettingsJob : IJob
    {

        private readonly ITestSchedulerService _testSchedulerService;

        public EMPSettingsJob(
                    ITestSchedulerService testSchedulerService
            )
        {
           _testSchedulerService = testSchedulerService;
        }
       
        public async System.Threading.Tasks.Task Execute(IJobExecutionContext context)
        {
            try
            {
                await _testSchedulerService.InitializeEvaluationReleases();
            }
            catch (Exception ex)
            {
                logExceptionHandler(ex);
            }
            try
            {
                await _testSchedulerService.InitializeTestReleases();
            }
            catch (Exception ex)
            {
                logExceptionHandler(ex);
            }
        }
        

        public void logExceptionHandler(Exception ex)
        {
            Log.ForContext<EMPSettingsJob>().Fatal(ex.StackTrace);
            Log.ForContext<EMPSettingsJob>().Fatal(ex.Message);

            var e = ex.InnerException;

            while (e != null)
            {
                Log.ForContext<EMPSettingsJob>().Fatal(e.StackTrace ?? "NO Stacktrace");
                Log.ForContext<EMPSettingsJob>().Fatal(e.Message ?? "NO Inner message");
                e = e.InnerException;
            }
        }
    }
}
