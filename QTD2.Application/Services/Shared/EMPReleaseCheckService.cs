using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using IClassSchdeuleEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using IEvaluationReleaseDomainService = QTD2.Domain.Interfaces.Service.Core.IEvaluationReleaseEMPSettingsService;
using IEvalRosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Evaluation_RosterService;
using ITestRosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_RosterService;
using IILAEvalLinkService = QTD2.Domain.Interfaces.Service.Core.IILA_StudentEvaluation_LinkService;
using IILATraineeEvaluationService = QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService;
using IClassScheduleRosterStatusService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Roster_StatusesService;
using ITestDomainService = QTD2.Domain.Interfaces.Service.Core.ITestService;
using IClassScheduleTestReleaseEMPSettingsService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_TestReleaseEMPSettingsService;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;

namespace QTD2.Application.Services.Shared
{
    public class EMPReleaseCheckService : IEMPReleaseCheckService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<DashboardService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IILADomainService _ilaService;
        private readonly IClassScheduleDomainService _classSchdeulService;
        private readonly IClassSchdeuleEmployeeDomainService _classSchedule_employeeService;
        private readonly IEvaluationReleaseDomainService _evalReleaseService;
        private readonly IEvalRosterDomainService _evalRosterService;
        private readonly IILAEvalLinkService _ilaEvalLinkService;
        private readonly IClassScheduleTestReleaseEMPSettingsService _classScheduleTestReleaseEMPSettingsService;
        private readonly IILATraineeEvaluationService _iLATraineeEvaluationService;
        private readonly ITestRosterDomainService _testRosterDomainService;
        private readonly IClassScheduleRosterStatusService _classScheduleRosterStatusService;
        private readonly ITestDomainService _testService;

        public EMPReleaseCheckService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<DashboardService> localizer,
            UserManager<AppUser> userManager,
            IILADomainService ilaService,
            IClassScheduleDomainService classSchdeulService,
            IClassSchdeuleEmployeeDomainService classSchedule_employeeService,
            IEvaluationReleaseDomainService evalReleaseService,
            IEvalRosterDomainService evalRosterService,
            IILAEvalLinkService ilaEvalLinkService,
            IClassScheduleTestReleaseEMPSettingsService classScheduleTestReleaseEMPSettingsService,
            IILATraineeEvaluationService iLATraineeEvaluationService,
            ITestRosterDomainService testRosterDomainService,
            IClassScheduleRosterStatusService classScheduleRosterStatusService,
            ITestDomainService testService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _ilaService = ilaService;
            _classSchdeulService = classSchdeulService;
            _classSchedule_employeeService = classSchedule_employeeService;
            _evalReleaseService = evalReleaseService;
            _evalRosterService = evalRosterService;
            _ilaEvalLinkService = ilaEvalLinkService;
            _classScheduleTestReleaseEMPSettingsService = classScheduleTestReleaseEMPSettingsService;
            _iLATraineeEvaluationService = iLATraineeEvaluationService;
            _testRosterDomainService = testRosterDomainService;
            _classScheduleRosterStatusService = classScheduleRosterStatusService;
            _testService = testService;
        }

        public async System.Threading.Tasks.Task CheckEvaluationRelease()
        {
            var ilas = await _ilaService.FindQuery(x => x.Active == true && x.IsPublished).Select(s => new ILA { Id = s.Id, Active = s.Active }).ToListAsync();
            foreach (var ila in ilas)
            {
                var linkedEvals = await _ilaEvalLinkService.FindQuery(x => x.ILAId == ila.Id).ToListAsync();
                var evalSetting = await _evalReleaseService.FindQuery(x => x.ILAId == ila.Id).FirstOrDefaultAsync();
                if (evalSetting != null && !evalSetting.EvaluationUsedToDeployStudentEvaluation && !evalSetting.EvaluationRequired)
                {
                    var classes = await _classSchdeulService.FindQuery(x => x.ILAID == ila.Id && x.Active == true).Select(s => new ClassSchedule { Id = s.Id, Active = s.Active, StartDateTime = s.StartDateTime, EndDateTime = s.EndDateTime }).ToListAsync();
                    foreach (var cs in classes)
                    {
                        if (evalSetting.EvaluationAvailableOnStartDate)
                        {
                            if (DateTime.Compare(cs.StartDateTime, DateTime.Now) <= 0 && evalSetting.FinalGradeRequired)
                            {
                                var csEmployees = await _classSchedule_employeeService.FindQuery(x => x.ClassScheduleId == cs.Id && x.FinalGrade != null).ToListAsync();
                                foreach (var csEmp in csEmployees)
                                {
                                    foreach (var linkedEval in linkedEvals)
                                    {
                                        var roster = await _evalRosterService.FindQuery(x => x.EmployeeId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.StudentEvaluationId == linkedEval.studentEvalFormID).FirstOrDefaultAsync();
                                        var releaseDate = new[] { cs.StartDateTime, DateTime.UtcNow }.Max();
                                        if (roster != null && roster.IsReleased == false)
                                        {
                                            roster.ReleaseDate = releaseDate;
                                            roster.IsReleased = true;
                                            await _evalRosterService.UpdateAsync(roster);
                                        }
                                        else if (roster == null)
                                        {
                                            roster = new ClassSchedule_Evaluation_Roster(releaseDate, null, cs.Id, csEmp.EmployeeId, false, true, false, linkedEval.studentEvalFormID);
                                            roster.SetReleaseDate(releaseDate);
                                            await _evalRosterService.AddAsync(roster);
                                        }
                                    }
                                }
                            }
                            else if (DateTime.Compare(cs.StartDateTime, DateTime.Now) <= 0)
                            {
                                var csEmployees = await _classSchedule_employeeService.FindQuery(x => x.ClassScheduleId == cs.Id).ToListAsync();
                                //var rosters = await _evalRosterService.FindQuery(x => x.ClassScheduleId == cs.Id && x.IsReleased == false).Select(s => new ClassSchedule_Evaluation_Roster { Id = s.Id, Active = s.Active, ClassScheduleId = s.ClassScheduleId, EmployeeId = s.EmployeeId, StudentEvaluationId = s.StudentEvaluationId }).ToListAsync();
                                //foreach (var roster in rosters)
                                //{
                                //    roster.IsReleased = true;
                                //    roster.ReleaseDate = DateTime.UtcNow;
                                //    await _evalRosterService.UpdateAsync(roster);
                                //}
                                foreach (var csEmp in csEmployees)
                                {
                                    foreach (var linkedEval in linkedEvals)
                                    {
                                        var roster = await _evalRosterService.FindQuery(x => x.EmployeeId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.StudentEvaluationId == linkedEval.studentEvalFormID).FirstOrDefaultAsync();
                                        if (roster != null && roster.IsReleased == false)
                                        {
                                            roster.ReleaseDate = cs.StartDateTime;
                                            roster.IsReleased = true;
                                            await _evalRosterService.UpdateAsync(roster);
                                        }
                                        else if (roster == null)
                                        {
                                            roster = new ClassSchedule_Evaluation_Roster(cs.StartDateTime, null, cs.Id, csEmp.EmployeeId, false, true, false, linkedEval.studentEvalFormID);
                                            await _evalRosterService.AddAsync(roster);
                                        }
                                    }
                                }
                            }
                        }
                        else if (evalSetting.EvaluationAvailableOnEndDate)
                        {
                            if (DateTime.Compare(cs.EndDateTime, DateTime.Now) <= 0 && evalSetting.FinalGradeRequired)
                            {
                                var csEmployees = await _classSchedule_employeeService.FindQuery(x => x.ClassScheduleId == cs.Id && x.FinalGrade != null).ToListAsync();
                                foreach (var csEmp in csEmployees)
                                {
                                    foreach (var linkedEval in linkedEvals)
                                    {
                                        var roster = await _evalRosterService.FindQuery(x => x.EmployeeId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.StudentEvaluationId == linkedEval.studentEvalFormID).FirstOrDefaultAsync();
                                        var releaseDate = new[] { cs.StartDateTime, DateTime.UtcNow }.Max();
                                        if (roster != null && roster.IsReleased)
                                        {
                                            roster.ReleaseDate = releaseDate;
                                            roster.IsReleased = true;
                                            await _evalRosterService.UpdateAsync(roster);
                                        }
                                        else if (roster == null)
                                        {
                                            roster = new ClassSchedule_Evaluation_Roster(releaseDate, null, cs.Id, csEmp.EmployeeId, false, true, false, linkedEval.studentEvalFormID);
                                            await _evalRosterService.AddAsync(roster);
                                        }
                                    }
                                }
                            }
                            else if (DateTime.Compare(cs.EndDateTime, DateTime.Now) <= 0)
                            {
                                var csEmployees = await _classSchedule_employeeService.FindQuery(x => x.ClassScheduleId == cs.Id).ToListAsync();
                                foreach (var csEmp in csEmployees)
                                {
                                    foreach (var linkedEval in linkedEvals)
                                    {
                                        var roster = await _evalRosterService.FindQuery(x => x.EmployeeId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.StudentEvaluationId == linkedEval.studentEvalFormID).FirstOrDefaultAsync();
                                        if (roster != null && roster.IsReleased == false)
                                        {
                                            roster.ReleaseDate = cs.EndDateTime;
                                            roster.IsReleased = true;
                                            await _evalRosterService.UpdateAsync(roster);
                                        }
                                        else if (roster == null)
                                        {
                                            roster = new ClassSchedule_Evaluation_Roster(cs.EndDateTime, null, cs.Id, csEmp.EmployeeId, false, true, false, linkedEval.studentEvalFormID);
                                            await _evalRosterService.AddAsync(roster);
                                        }
                                    }
                                }
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
                                if (evalSetting.ReleasePrior == false ? DateTime.Compare(cs.EndDateTime, DateTime.Now.AddMinutes((int)time)) <= 0 : (DateTime.Compare(cs.EndDateTime, DateTime.Now.AddMinutes((int)time)) >= 0 || DateTime.Compare(cs.EndDateTime, DateTime.Now) <= 0))
                                {
                                    var csEmployees = await _classSchedule_employeeService.FindQuery(x => x.ClassScheduleId == cs.Id).ToListAsync();
                                    foreach (var csEmp in csEmployees)
                                    {
                                        foreach (var linkedEval in linkedEvals)
                                        {
                                            var roster = await _evalRosterService.FindQuery(x => x.EmployeeId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.StudentEvaluationId == linkedEval.studentEvalFormID).FirstOrDefaultAsync();
                                            if (roster != null && roster.IsReleased == false)
                                            {
                                                roster.ReleaseDate = cs.EndDateTime.AddMinutes((int)time);
                                                roster.IsReleased = true;
                                                await _evalRosterService.UpdateAsync(roster);
                                            }
                                            else if (roster == null)
                                            {
                                                roster = new ClassSchedule_Evaluation_Roster(cs.EndDateTime.AddMinutes((int)time), null, cs.Id, csEmp.EmployeeId, false, true, false, linkedEval.studentEvalFormID);
                                                await _evalRosterService.AddAsync(roster);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (evalSetting.ReleaseAfterGradeAssigned)
                        {
                            var csEmployees = await _classSchedule_employeeService.FindQuery(x => x.ClassScheduleId == cs.Id && x.FinalGrade != null).ToListAsync();
                            foreach (var csEmp in csEmployees)
                            {
                                foreach (var linkedEval in linkedEvals)
                                {
                                    var roster = await _evalRosterService.FindQuery(x => x.EmployeeId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.StudentEvaluationId == linkedEval.studentEvalFormID).FirstOrDefaultAsync();
                                    if (roster != null && roster.IsReleased == false)
                                    {
                                        roster.ReleaseDate = DateTime.UtcNow;
                                        roster.IsReleased = true;
                                        await _evalRosterService.UpdateAsync(roster);
                                    }
                                    else if (roster == null)
                                    {
                                        roster = new ClassSchedule_Evaluation_Roster(DateTime.UtcNow, null, cs.Id, csEmp.EmployeeId, false, true, false, linkedEval.studentEvalFormID);
                                        await _evalRosterService.AddAsync(roster);
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }




        //test release settings 
        public async System.Threading.Tasks.Task CheckTestRelease()
        {
            var ilas = await _ilaService.FindQuery(x => x.Active == true && x.IsPublished).Select(s => new ILA { Id = s.Id, Active = s.Active }).ToListAsync();
            var notStarted = _classScheduleRosterStatusService.FindQuery(x => x.Name == "Not Started").FirstOrDefault();
            foreach (var ila in ilas)
            {
                //var linkedEvals = await _iLATraineeEvaluationService.FindQuery(x => x.ILAId == ila.Id).ToListAsync();
                //condition 
                var classes = await _classSchdeulService.FindQuery(x => x.ILAID == ila.Id && x.Active == true).Select(s => new ClassSchedule { Id = s.Id, Active = s.Active, StartDateTime = s.StartDateTime, EndDateTime = s.EndDateTime }).ToListAsync();
                foreach (var cs in classes)
                {
                    var evalSetting = await _classScheduleTestReleaseEMPSettingsService.FindQuery(x => x.ClassScheduleId == cs.Id).FirstOrDefaultAsync();
                    if (evalSetting != null && evalSetting.UsePreTestAndTest == false)
                    {
                        //pretest settings if required along with release on start time and number of days
                        if (evalSetting.PreTestRequired && evalSetting.PreTestAvailableOneStartDate && evalSetting.MakeAvailableBeforeDays != null)
                        {
                            var time = evalSetting.MakeAvailableBeforeDays;
                            time = -time;

                            if (DateTime.Compare(cs.StartDateTime, DateTime.Now.AddMinutes((int)time)) <= 0 || DateTime.Compare(cs.StartDateTime, DateTime.Now) <= 0)
                            {
                                var csEmployees = await _classSchedule_employeeService.FindQuery(x => x.ClassScheduleId == cs.Id).ToListAsync();
                                foreach (var csEmp in csEmployees)
                                {
                                    var pretest = await _iLATraineeEvaluationService.FindQuery(x => x.TestId == evalSetting.PreTestId && x.ILAId == evalSetting.ClassSchedule.ILAID).FirstOrDefaultAsync();
                                    if (pretest != null)
                                    {

                                        var roster = await _testRosterDomainService.FindQuery(x => x.EmpId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.TestId == pretest.TestId && x.RetakeOrder == null).FirstOrDefaultAsync();
                                        if (roster != null && roster.IsReleased == false)
                                        {
                                            roster.ReleaseDate = cs.StartDateTime.AddMinutes((int)time);
                                            roster.IsReleased = true;
                                            await _testRosterDomainService.UpdateAsync(roster);
                                        }
                                        else if (roster == null)
                                        {
                                            roster = new ClassSchedule_Roster(cs.Id, pretest.TestId, (int)pretest.TestTypeId, csEmp.EmployeeId, true, null, false, false, null, cs.StartDateTime.AddMinutes((int)time), null, true, notStarted.Id);
                                            await _testRosterDomainService.AddAsync(roster);
                                        }

                                    }

                                }
                            }
                        }

                        //final test settings which are going to run in every case

                        //final test avaliable on start date
                        if (evalSetting.MakeFinalTestAvailableImmediatelyAfterStartDate)
                        {
                            if (DateTime.Compare(cs.StartDateTime, DateTime.Now) <= 0)
                            {
                                var csEmployees = await _classSchedule_employeeService.FindQuery(x => x.ClassScheduleId == cs.Id).ToListAsync();
                                foreach (var csEmp in csEmployees)
                                {
                                    var finalTest = await _iLATraineeEvaluationService.FindQuery(x => x.TestId == evalSetting.FinalTestId && x.ILAId == evalSetting.ClassSchedule.ILAID).FirstOrDefaultAsync();
                                    if (finalTest != null)
                                    {
                                        var roster = await _testRosterDomainService.FindQuery(x => x.EmpId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.TestId == finalTest.TestId && x.RetakeOrder == null).FirstOrDefaultAsync();
                                        if (roster != null && roster.IsReleased == false)
                                        {
                                            roster.ReleaseDate = cs.StartDateTime;
                                            roster.IsReleased = true;
                                            roster.RetakeOrder = null;
                                            await _testRosterDomainService.UpdateAsync(roster);
                                        }
                                        else if (roster == null)
                                        {
                                            roster = new ClassSchedule_Roster(cs.Id, finalTest.TestId, (int)finalTest.TestTypeId, csEmp.EmployeeId, true, null, false, false, null, cs.StartDateTime, null, true, notStarted.Id);
                                            roster.RetakeOrder = null;
                                            await _testRosterDomainService.AddAsync(roster);
                                        }

                                    }
                                }
                            }
                        }

                        // final test avaliable on class end date
                        else if (evalSetting.MakeFinalTestAvailableOnClassEndDate)
                        {
                            if (DateTime.Compare(cs.EndDateTime, DateTime.Now) <= 0)
                            {
                                var csEmployees = await _classSchedule_employeeService.FindQuery(x => x.ClassScheduleId == cs.Id).ToListAsync();
                                foreach (var csEmp in csEmployees)
                                {
                                    var finalTest = await _iLATraineeEvaluationService.FindQuery(x => x.TestId == evalSetting.FinalTestId && x.ILAId == evalSetting.ClassSchedule.ILAID).FirstOrDefaultAsync();
                                    if (finalTest != null)
                                    {
                                        var roster = await _testRosterDomainService.FindQuery(x => x.EmpId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.TestId == finalTest.TestId && x.RetakeOrder == null).FirstOrDefaultAsync();
                                        if (roster != null && roster.IsReleased == false)
                                        {
                                            roster.ReleaseDate = cs.StartDateTime;
                                            roster.IsReleased = true;
                                            roster.RetakeOrder = null;
                                            await _testRosterDomainService.UpdateAsync(roster);
                                        }
                                        else if (roster == null)
                                        {
                                            roster = new ClassSchedule_Roster(cs.Id, finalTest.TestId, (int)finalTest.TestTypeId, csEmp.EmployeeId, true, null, false, false, null, cs.StartDateTime, null, true, notStarted.Id);
                                            roster.RetakeOrder = null;
                                            await _testRosterDomainService.AddAsync(roster);
                                        }

                                    }
                                }
                            }

                        }

                        // final test avaliable on specific time of end date
                        else if (evalSetting.MakeFinalTestAvailableOnSpecificTime != null)
                        {
                            var time = evalSetting.MakeFinalTestAvailableOnSpecificTime;
                            if (evalSetting.FinalTestSpecificTimePrior == true)
                            {
                                time = time;
                            }
                            else if (evalSetting.FinalTestSpecificTimePrior == false)
                            {
                                time = -time;
                            }

                            if (DateTime.Compare(cs.EndDateTime, DateTime.Now.AddMinutes((int)time)) <= 0 || DateTime.Compare(cs.EndDateTime, DateTime.Now) <= 0)
                            {
                                var csEmployees = await _classSchedule_employeeService.FindQuery(x => x.ClassScheduleId == cs.Id).ToListAsync();
                                foreach (var csEmp in csEmployees)
                                {
                                    var finalTest = await _iLATraineeEvaluationService.FindQuery(x => x.TestId == evalSetting.FinalTestId && x.ILAId == evalSetting.ClassSchedule.ILAID).FirstOrDefaultAsync();
                                    if (finalTest != null)
                                    {
                                        var roster = await _testRosterDomainService.FindQuery(x => x.EmpId == csEmp.EmployeeId && x.ClassScheduleId == csEmp.ClassScheduleId && x.TestId == finalTest.TestId && x.RetakeOrder == null).FirstOrDefaultAsync();
                                        if (roster != null && roster.IsReleased == false)
                                        {
                                            roster.ReleaseDate = cs.StartDateTime;
                                            roster.IsReleased = true;
                                            roster.RetakeOrder = null;
                                            await _testRosterDomainService.UpdateAsync(roster);
                                        }
                                        else if (roster == null)
                                        {
                                            roster = new ClassSchedule_Roster(cs.Id, finalTest.TestId, (int)finalTest.TestTypeId, csEmp.EmployeeId, true, null, false, false, null, cs.StartDateTime, null, true, notStarted.Id);
                                            roster.RetakeOrder = null;
                                            await _testRosterDomainService.AddAsync(roster);
                                        }

                                    }
                                }
                            }
                        }


                    }
                }
            }

        }
    }
}
