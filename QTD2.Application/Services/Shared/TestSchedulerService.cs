
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.EntityFrameworkCore;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Data;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Reports.Generation.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
    public class TestSchedulerService : ITestSchedulerService
    {
        //always pass DB instance as a parameter from the caller
        //we have to write base 

        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IDbContextBuilder _dbContextBuilder;
        private readonly IDatabaseResolver _dbResolver;

        public TestSchedulerService(
            IInstanceFetcher instanceFetcher,
            IDbContextBuilder dbContextBuilder,
            IDatabaseResolver dbResolver
            )
        {
            _instanceFetcher = instanceFetcher;
            _dbContextBuilder = dbContextBuilder;
            _dbResolver = dbResolver;
        }

        async System.Threading.Tasks.Task AddEvaluationToRoaster(QTDContext _context, ClassSchedule cs, DateTime releaseDate, List<ClassSchedule_StudentEvaluations_Link> linkedEvals, bool finalGradeRequired)
        {
            var csEmployees = cs.ClassSchedule_Employee.Where(x => !finalGradeRequired || x.FinalGrade != null && x.IsEnrolled).ToList();

            if (csEmployees == null || csEmployees.Count == 0)
            {
                return;
            }
            var roasters = cs.ClassSchedule_Evaluation_Rosters;
            foreach (var csEmp in csEmployees)
            {
                foreach (var linkedEval in linkedEvals)
                {
                    var roster = roasters.Where(x => x.EmployeeId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.StudentEvaluationId == linkedEval.StudentEvaluationId).FirstOrDefault();
                    if (roster != null && !roster.IsReleased && !roster.IsRecalled)
                    {
                        roster.Release(releaseDate);
                        _context.ClassSchedule_Evaluation_Roster.Update(roster);
                    }
                    else if (roster == null)
                    {
                        roster = new ClassSchedule_Evaluation_Roster(releaseDate, null, cs.Id, csEmp.EmployeeId, false, true, false, linkedEval.StudentEvaluationId);
                        roster.Release(releaseDate);
                        await _context.ClassSchedule_Evaluation_Roster.AddAsync(roster);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
        public async System.Threading.Tasks.Task InitializeEvaluationReleases()
        {
            var instances = await _instanceFetcher.GetAllActiveInstancesAsync();
            var currentDateUTC = DateTime.Now.ToUniversalTime();
            foreach (var instance in instances)
            {
                try
                {
                    var qtdContext = _dbContextBuilder.BuildQtdContext(instance.DatabaseName);
                    _dbResolver.SetConnectionString(instance.DatabaseName);

                    var classSchedules = await qtdContext.ClassSchedules.Where(x =>
                                         x.Active && x.ILA.Active && x.ILA.IsPublished
                                         && x.ILA.EvaluationReleaseEMPSetting != null
                                         && !x.ILA.EvaluationReleaseEMPSetting.EvaluationUsedToDeployStudentEvaluation
                                         && !x.ILA.EvaluationReleaseEMPSetting.EvaluationRequired
                                         && x.ILA.ILA_StudentEvaluation_Links.Count > 0
                                         && x.ClassSchedule_StudentEvaluations_Links.Count > 0
                                         && x.ClassSchedule_Employee.Count > 0
                                         && x.Active
                                         && x.StartDateTime < currentDateUTC
                                         && x.EndDateTime.AddDays(1) > currentDateUTC
                                         && (!x.ILA.EvaluationReleaseEMPSetting.FinalGradeRequired || x.ClassSchedule_Employee.Any(y => y.FinalGrade != null))
                    )
                        .Select(s => new
                            ClassSchedule
                        {
                            Id = s.Id,
                            Active = s.Active,
                            StartDateTime = s.StartDateTime,
                            EndDateTime = s.EndDateTime,
                            ClassSchedule_Employee = s.ClassSchedule_Employee,
                            ClassSchedule_Evaluation_Rosters = s.ClassSchedule_Evaluation_Rosters,
                            ClassSchedule_StudentEvaluations_Links = s.ClassSchedule_StudentEvaluations_Links,
                            ILA = new ILA
                            {
                                EvaluationReleaseEMPSetting = s.ILA.EvaluationReleaseEMPSetting,
                                ILA_StudentEvaluation_Links = s.ILA.ILA_StudentEvaluation_Links
                            }
                        }).ToListAsync();

                    if (classSchedules == null || classSchedules.Count == 0)
                        continue;

                    foreach (var cs in classSchedules)
                    {
                        var evalSetting = cs.ILA.EvaluationReleaseEMPSetting;

                        var evaluationLinks = cs.ClassSchedule_StudentEvaluations_Links.ToList();

                        if (evalSetting.EvaluationAvailableOnStartDate)
                        {
                            if (DateTime.Compare(cs.StartDateTime, DateTime.UtcNow) <= 0)
                            {
                                var releaseDate = cs.StartDateTime;

                                if (evalSetting.FinalGradeRequired)
                                {
                                    releaseDate = new[] { cs.StartDateTime, currentDateUTC }.Max();
                                }

                                await AddEvaluationToRoaster(qtdContext, cs, releaseDate, evaluationLinks, evalSetting.FinalGradeRequired);
                            }
                        }
                        else if (evalSetting.EvaluationAvailableOnEndDate)
                        {
                            if (DateTime.Compare(cs.EndDateTime, DateTime.UtcNow) <= 0)
                            {
                                var releaseDate = cs.EndDateTime;

                                if (evalSetting.FinalGradeRequired)
                                {
                                    releaseDate = new[] { cs.EndDateTime, currentDateUTC }.Max();
                                }

                                await AddEvaluationToRoaster(qtdContext, cs, releaseDate, evaluationLinks, evalSetting.FinalGradeRequired);
                            }
                        }
                        else if (evalSetting.ReleaseOnSpecificTimeAfterClassEndDate)
                        {
                            var time = evalSetting.ReleaseAfterEndTime;
                            if (time != null)
                            {
                                if (evalSetting.ReleasePrior == true)
                                {
                                    time = -time;
                                }
                                DateTime scheduledDate = cs.EndDateTime.AddMinutes((int)time);
                                if (DateTime.Compare(scheduledDate, DateTime.UtcNow) <= 0)
                                {
                                    await AddEvaluationToRoaster(qtdContext, cs, scheduledDate, evaluationLinks, evalSetting.FinalGradeRequired);
                                }
                            }
                        }
                        else if (evalSetting.ReleaseAfterGradeAssigned)
                        {
                            DateTime scheduledDate = DateTime.UtcNow;

                            evalSetting.FinalGradeRequired = true;
                            await AddEvaluationToRoaster(qtdContext, cs, cs.StartDateTime, evaluationLinks, evalSetting.FinalGradeRequired);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logExceptionHandler(ex);
                    //Console.WriteLine(ex?.Message+ ":Exception thrown from InitializeEvaluationReleases instance:"+instance.DatabaseName);
                }

                //peform operation on qtd context
            }

        }

        public void logExceptionHandler(Exception ex)
        {
            Log.ForContext<TestSchedulerService>().Fatal(ex.StackTrace);
            Log.ForContext<TestSchedulerService>().Fatal(ex.Message);

            var e = ex.InnerException;

            while (e != null)
            {
                Log.ForContext<TestSchedulerService>().Fatal(e.StackTrace ?? "NO Stacktrace");
                Log.ForContext<TestSchedulerService>().Fatal(e.Message ?? "NO Inner message");
                e = e.InnerException;
            }
        }

        public async System.Threading.Tasks.Task InitializeTestReleases()
        {
            //have to update this
            var instances = await _instanceFetcher.GetAllActiveInstancesAsync();
            foreach (var instance in instances)
            {
                try
                {
                    var currentTimeUTC = DateTime.Now.ToUniversalTime();

                    var qtdContext = _dbContextBuilder.BuildQtdContext(instance.DatabaseName);
                    _dbResolver.SetConnectionString(instance.DatabaseName);

                    var classSchedules = await qtdContext.ClassSchedules.Where
                                        (
                                            x => x.ILA.Active
                                            && x.ILA.IsPublished
                                            && x.ILA.UseForEMP
                                            && x.ClassSchedule_TestReleaseEMPSettings != null
                                            && !x.ClassSchedule_TestReleaseEMPSettings.UsePreTestAndTest      // Pre Tests can be made availabel before class Start so we will not check for class start time
                                            && x.EndDateTime.AddDays(1) > currentTimeUTC            // Final Tests are some times Made available after end time of class           
                                            &&
                                            (
                                                x.ClassSchedule_TestReleaseEMPSettings.PreTestId != null
                                                ||
                                                x.ClassSchedule_TestReleaseEMPSettings.FinalTestId != null
                                             )
                                            && x.Active == true
                                            && x.ClassSchedule_Employee.Count > 0
                                            &&
                                                (
                                                    x.ClassSchedule_Rosters.Count == 0  // In case when no test is added for any student
                                                    || x.ClassSchedule_Rosters.Where(rt => rt.TestId == x.ClassSchedule_TestReleaseEMPSettings.PreTestId).Count() < x.ClassSchedule_Employee.Count // In case some of the stundents are enrolled later  we should check if the test is added for them or not
                                                    || x.ClassSchedule_Rosters.Where(rt => rt.TestId == x.ClassSchedule_TestReleaseEMPSettings.FinalTestId).Count() < x.ClassSchedule_Employee.Count
                                                 )
                                            ).
                            Select(s => new ClassSchedule
                            {
                                Id = s.Id,
                                Active = s.Active,
                                StartDateTime = s.StartDateTime,
                                EndDateTime = s.EndDateTime,
                                ClassSchedule_Employee = s.ClassSchedule_Employee,
                                ClassSchedule_Rosters = s.ClassSchedule_Rosters,
                                ClassSchedule_TestReleaseEMPSettings = s.ClassSchedule_TestReleaseEMPSettings,
                                ILA = new ILA
                                {
                                    ILATraineeEvaluations = s.ILA.ILATraineeEvaluations
                                }
                            })
                            .ToListAsync();


                    if (classSchedules == null || classSchedules.Count == 0)
                        continue;


                    var notStarted = await qtdContext.ClassSchedule_Roster_Statuses.Where(x => x.Name == "Not Started").FirstOrDefaultAsync();


                    foreach (var cs in classSchedules)
                    {

                        var evalSettings = cs.ClassSchedule_TestReleaseEMPSettings;

                        ////Get the Evaluation for Pretes
                        var preTest = cs.ILA.ILATraineeEvaluations.FirstOrDefault(x => x.TestId == evalSettings.PreTestId);

                        ////Get the Evaluation for Final Test
                        var finalTest = cs.ILA.ILATraineeEvaluations.FirstOrDefault(x => x.TestId == evalSettings.FinalTestId);

                        if (preTest != null)
                        {
                            await ReleasePreTest(qtdContext, evalSettings, cs, notStarted.Id, preTest);
                        }
                        if (finalTest != null)
                        {
                            await ReleaseFinalTests(qtdContext, evalSettings, cs, notStarted.Id, finalTest);
                        }
                    }

                }
                catch (Exception ex)
                {
                    logExceptionHandler(ex);
                }

                //peform operation on qtd context
            }
        }

        public async System.Threading.Tasks.Task ReleasePreTest(QTDContext _context, ClassSchedule_TestReleaseEMPSetting settings, ClassSchedule cs, int statusID, ILATraineeEvaluation preTest)
        {
            try
            {
                if (settings.PreTestRequired && settings.PreTestAvailableOneStartDate && settings.DaysOrWeeks != null)
                {
                    var FinaltimeTocompare = DateTime.Now.ToUniversalTime().AddDays(2);
                    if (settings.DaysOrWeeks == 1 && settings.MakeAvailableBeforeDays != null)
                    {
                        FinaltimeTocompare = cs.StartDateTime.AddDays(settings.MakeAvailableBeforeDays * (-1) ?? 0);
                    }
                    if (settings.DaysOrWeeks == 2 && settings.MakeAvailableBeforeWeeks != null)
                    {
                        FinaltimeTocompare = cs.StartDateTime.AddDays(settings.MakeAvailableBeforeWeeks * (-7) ?? 0);
                    }
                    if (DateTime.Compare(FinaltimeTocompare, DateTime.Now.ToUniversalTime()) <= 0)
                    {
                        await AddTestsForClass(_context, cs, statusID, preTest, FinaltimeTocompare);
                    }
                }
                //final test avaliable on start date
                else if (settings.PreTestRequired && settings.PreTestAvailableOneStartDate && settings.MakeAvailableBeforeDays == null && DateTime.Compare(cs.StartDateTime, DateTime.UtcNow) <= 0)
                {
                    await AddTestsForClass(_context, cs, statusID, preTest, DateTime.Now.ToUniversalTime());
                }

                else if (settings.PreTestRequired && settings.PreTestAvailableOnEnrollment)
                {
                    await AddTestsForClass(_context, cs, statusID, preTest, DateTime.Now.ToUniversalTime());
                }

            }
            catch (Exception ex)
            {
                logExceptionHandler(ex);
            }


        }


        public async System.Threading.Tasks.Task AddTestsForClass(QTDContext _context, ClassSchedule cs, int statusID, ILATraineeEvaluation traineeEvaluation, DateTime date)
        {
            var csEmployees = cs.ClassSchedule_Employee.Where(x=>x.IsEnrolled);
            if (csEmployees == null)
            {
                return;
            }
            foreach (var csEmp in csEmployees)
            {
                if (traineeEvaluation != null)
                {
                    var roster = cs.ClassSchedule_Rosters.Where(x => x.EmpId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.TestId == traineeEvaluation.TestId && x.RetakeOrder == null).FirstOrDefault();
                    if (roster != null && roster.IsReleased == false)
                    {
                        roster.Release(date);
                        _context.ClassSchedule_Roster.Update(roster);
                    }
                    else if (roster == null)
                    {
                        roster = new ClassSchedule_Roster(cs.Id, traineeEvaluation.TestId, (int)traineeEvaluation.TestTypeId, csEmp.EmployeeId, true, null, false, false, null, date, null, true, statusID);
                        roster.Release(date);
                        await _context.ClassSchedule_Roster.AddAsync(roster);
                    }

                }
            }
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task ReleaseFinalTests(QTDContext _context, ClassSchedule_TestReleaseEMPSetting settings, ClassSchedule cs, int statusId, ILATraineeEvaluation finalTest)
        {
            var currentUTCTime = DateTime.Now.ToUniversalTime();
            if (settings.MakeFinalTestAvailableImmediatelyAfterStartDate && cs.StartDateTime < currentUTCTime)
            {
                await AddTestsForClass(_context, cs, statusId, finalTest, currentUTCTime);
            }
            // final test avaliable on class end date
            else if (settings.MakeFinalTestAvailableOnClassEndDate)
            {
                if (DateTime.Compare(cs.EndDateTime, currentUTCTime) <= 0)
                {
                    await AddTestsForClass(_context, cs, statusId, finalTest, currentUTCTime);
                }
            }
            // final test avaliable on specific time of end date
            else if (settings.MakeFinalTestAvailableOnSpecificTime != null)
            {
                var time = settings.MakeFinalTestAvailableOnSpecificTime;
                if (settings.FinalTestSpecificTimePrior == false)
                {
                    time = -time;
                }

                if (DateTime.Compare(cs.EndDateTime, currentUTCTime.AddMinutes((int)time)) <= 0)
                {
                    await AddTestsForClass(_context, cs, statusId, finalTest, currentUTCTime);
                }
            }
            else if (settings.MakeFinalTestAvailableAfterCBTCompleted)
            {
                cs.ClassSchedule_Employee = cs.ClassSchedule_Employee.Where(r => r.CBTStatusId == 3).ToList();
                await AddTestsForClass(_context, cs, statusId, finalTest, currentUTCTime);
            }
        }

        public async Task<QTDContext> GetQtdContextAsync(string instanceName)
        {
            var setting = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            return _dbContextBuilder.BuildQtdContext(setting.DatabaseName);
        }
    }
}
