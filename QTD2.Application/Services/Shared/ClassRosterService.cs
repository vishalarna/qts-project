using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IClassRosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_RosterService;
using ITestTypeDomainService = QTD2.Domain.Interfaces.Service.Core.ITestTypeService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using IClassSchedule_EmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using IClassScheduleRosterStatusDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Roster_StatusesService;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;
using IClassSchedule_Evaluation_RosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Evaluation_RosterService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using IClassTestReleaseEmpSettingDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_TestReleaseEMPSettingsService;
using ITestReleaseEMPSetting_RetakeLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITestReleaseEMPSetting_Retake_LinkService;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using ITestReleaseEMPSetting_Retake_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITestReleaseEMPSetting_Retake_LinkService;
using ITestItemDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemService;
using ICBT_ScormRegistrationService = QTD2.Domain.Interfaces.Service.Core.ICBT_ScormRegistrationService;
using IClassScheduleRosterResponseDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Roster_ResponseService;
using QTD2.Infrastructure.Model.EmployeeTest;
using IClientUserSettings_GeneralSettingService = QTD2.Domain.Interfaces.Service.Core.IClientUserSettings_GeneralSettingService;
using ITestApplicationService = QTD2.Application.Interfaces.Services.Shared.ITestService;
using QTD2.Infrastructure.Reports.Generation.Models;


namespace QTD2.Application.Services.Shared
{
    public class ClassRosterService : Interfaces.Services.Shared.IClassRosterService
    {
        private readonly IClassRosterDomainService _classRosterService;
        private readonly Employee _employee;
        private readonly IEmployeeDomainService _employeeDomainService;
        private readonly ITestTypeDomainService _testtypeService;
        private readonly IStringLocalizer<ClassRosterService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IClassSchedule_EmployeeDomainService _class_empService;
        private readonly IClassScheduleRosterStatusDomainService _classScheduleRosterStatusDomainService;
        private readonly IClassSchedule_Evaluation_RosterDomainService _cs_eval_rosterService;
        private readonly IPersonDomainService _personService;
        private readonly ITestReleaseEMPSetting_RetakeLinkDomainService _testRelease_emp_RetakeLinkService;
        private readonly IILADomainService _ilaService;
        private readonly IClassScheduleDomainService _classScheduleService;
        private readonly ITestReleaseEMPSetting_Retake_LinkDomainService _testRelease_retakeLinkService;
        private readonly ICBT_ScormRegistrationService _cbtScormRegistrationService;
        private readonly ITestItemDomainService _testItemDomainService;
        private readonly IClassScheduleRosterResponseDomainService _classScheduleRosterResponseService;
        private readonly IClientUserSettings_GeneralSettingService _clientUserSettingsGeneralSettingsService;
        private readonly ITestApplicationService _testApplicationService;
        private readonly IClassTestReleaseEmpSettingDomainService _classTestReleaseEmpSettingDomainService;

        public ClassRosterService(
                IClassRosterDomainService classRosterService,
                IStringLocalizer<ClassRosterService> localizer,
                IHttpContextAccessor httpContextAccessor,
                IAuthorizationService authorizationService,
                UserManager<AppUser> userManager,
                ITestTypeDomainService testtypeService,
                IEmployeeDomainService employeeDomainService,
                IClassSchedule_EmployeeDomainService class_empService,
                IClassScheduleRosterStatusDomainService classScheduleRosterStatusDomainService,
                IClassSchedule_Evaluation_RosterDomainService cs_eval_rosterService,
                IPersonDomainService personService,
                ITestReleaseEMPSetting_RetakeLinkDomainService testRelease_emp_RetakeLinkService,
                IILADomainService ilaService, IClassScheduleDomainService classScheduleService,
                ITestReleaseEMPSetting_Retake_LinkDomainService testRelease_retakeLinkService,
                ICBT_ScormRegistrationService cbtScormRegistrationService,
                ITestItemDomainService testItemDomainService,
                IClassScheduleRosterResponseDomainService classScheduleRosterResponseService,
                ITestApplicationService testApplicationService,
                IClientUserSettings_GeneralSettingService clientUserSettingsGeneralSettingsService,
                IClassTestReleaseEmpSettingDomainService classTestReleaseEmpSettingDomainService)
        {
            _classRosterService = classRosterService;
            _testtypeService = testtypeService;
            _employee = new Employee();
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _employeeDomainService = employeeDomainService;
            _class_empService = class_empService;
            _cs_eval_rosterService = cs_eval_rosterService;
            _classScheduleRosterStatusDomainService = classScheduleRosterStatusDomainService;
            _personService = personService;
            _testRelease_emp_RetakeLinkService = testRelease_emp_RetakeLinkService;
            _ilaService = ilaService;
            _classScheduleService = classScheduleService;
            _testRelease_retakeLinkService = testRelease_retakeLinkService;
            _cbtScormRegistrationService = cbtScormRegistrationService;
            _testItemDomainService = testItemDomainService;
            _classScheduleRosterResponseService = classScheduleRosterResponseService;
            _clientUserSettingsGeneralSettingsService = clientUserSettingsGeneralSettingsService;
            _testApplicationService = testApplicationService;
            _classTestReleaseEmpSettingDomainService = classTestReleaseEmpSettingDomainService;
        }
        #region Roster
        public async Task<bool> CreateRoster(ClassRoasterModel options)
        {
            var statusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Not Started").FirstOrDefault().Id;
            int testItemType = 0;
            switch (options.TestItemType)
            {
                case "Pretest":
                default:
                    testItemType = (await _testtypeService.FindAsync(x => x.Description == "Pretest")).FirstOrDefault().Id;
                    break;
                case "Test":
                    testItemType = (await _testtypeService.FindAsync(x => x.Description == "Final Test")).FirstOrDefault().Id;
                    break;
                case "Retake":
                    testItemType = (await _testtypeService.FindAsync(x => x.Description == "Retake")).FirstOrDefault().Id;
                    break;
                case "CBT":
                    testItemType = (await _testtypeService.FindAsync(x => x.Description == "CBT")).FirstOrDefault().Id;
                    break;
                case "StudentEvaluation":
                    testItemType = (await _testtypeService.FindAsync(x => x.Description == "StudentEvaluation")).FirstOrDefault().Id;
                    break;
            }

            if (options.empIDs != null && options.empIDs.Count > 0)
            {
                foreach (var empID in options.empIDs)
                {
                    var emp = (await _classRosterService.FindAsync(x => x.ClassScheduleId == options.ClassScheduleId && x.EmpId == options.EmpId && x.TestTypeId == testItemType)).FirstOrDefault();
                    if (emp == null)
                    {
                        emp = new ClassSchedule_Roster(options.ClassScheduleId, options.TestId, testItemType, empID, options.Disclaimer, options.Grade, options.Interrupted, options.Restarted, null, options.ReleaseDate, options.Score, null, statusId);
                        emp.CompleteTest(options.CompletedDate, null, null);

                        var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, emp, ClassSchedule_RosterOperations.Create);
                        if (result.Succeeded)
                        {
                            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                            emp.Create(userName);
                            var validationResult = await _classRosterService.AddAsync(emp);

                            if (!validationResult.IsValid)
                            {
                                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                            }
                        }
                        else
                        {
                            throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public async Task<List<ClassScheduleEMPWithGradesVM>> GetRoasterEmployeesAsync(RosterFetchOptions options)
        {
            int testTypeId = 0;
            var testType = "";
            switch (options.TestType.Trim().ToLower())
            {
                case "pretest":
                default:
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Pretest")).FirstOrDefault().Id;
                    testType = "pretest";
                    break;
                case "test":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Final Test")).FirstOrDefault().Id;
                    testType = "test";
                    break;
                case "retake":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Retake")).FirstOrDefault().Id;
                    testType = "retake";
                    break;
                case "cbt":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "CBT")).FirstOrDefault().Id;
                    testType = "cbt";
                    break;
            }

            List<ClassScheduleEMPWithGradesVM> csEMPs = new List<ClassScheduleEMPWithGradesVM>();
            var emps = await _class_empService.FindQueryWithIncludeAsync(x => x.ClassScheduleId == options.ClassId && x.IsEnrolled == true, new string[] { "Employee.Person" }).Select(s => s.Employee).ToListAsync();
            foreach (var emp in emps)
            {
                if (testType == "cbt")
                {
                    var rosterData = await _cbtScormRegistrationService
                                            .FindQuery(x => x.ClassScheduleEmployee.EmployeeId == emp.Id
                                                    && x.ClassScheduleEmployee.ClassScheduleId == options.ClassId
                                                    && x.CBTScormUploadId == options.TestId)
                                            .FirstOrDefaultAsync();
                    ClassScheduleEMPWithGradesVM csemp = new ClassScheduleEMPWithGradesVM();
                    csemp.EmpId = emp.Id;

                    csemp.Grade = rosterData?.Grade;
                    csemp.Score = (int?)rosterData?.Score;
                    csemp.CompletedDate = rosterData?.CompletedDate;
                    csemp.ReleaseDate = rosterData?.CreatedDate;
                    csemp.EmpEmail = emp.Person.Username;
                    csemp.EmployeeName = emp.Person.FirstName + " " + emp.Person.LastName;
                    csemp.Image = emp.Person.Image;
                    csEMPs.Add(csemp);
                }
                else if (testType == "retake")
                {
                    var rosterDatas = await _classRosterService.FindQuery(x => x.EmpId == emp.Id && x.ClassScheduleId == options.ClassId && x.TestTypeId == testTypeId && x.TestId == options.TestId).ToListAsync();

                    foreach (var rosterData in rosterDatas)
                    {

                        ClassScheduleEMPWithGradesVM csemp = new ClassScheduleEMPWithGradesVM();
                        csemp.EmpId = emp.Id;
                        csemp.Disclaimer = rosterData.Disclaimer;
                        csemp.Interrupted = rosterData.Interrupted;
                        csemp.Grade = rosterData.Grade;
                        csemp.Score = rosterData.Score;
                        csemp.Restarted = rosterData.Restarted;
                        csemp.CompletedDate = rosterData.CompletedDate;
                        csemp.ReleaseDate = rosterData.ReleaseDate;
                        csemp.EmpEmail = emp.Person.Username;
                        csemp.EmployeeName = emp.Person.FirstName + " " + emp.Person.LastName;
                        csemp.Image = emp.Person.Image;
                        csemp.RetakeOrder = rosterData?.RetakeOrder;
                        csEMPs.Add(csemp);
                    }
                }
                else
                {
                    var rosterData = await _classRosterService.FindQuery(x => x.EmpId == emp.Id && x.ClassScheduleId == options.ClassId && x.TestTypeId == testTypeId && x.TestId == options.TestId && x.RetakeOrder == options.RetakeOrder).FirstOrDefaultAsync();
                    if (rosterData != null)
                    {
                        ClassScheduleEMPWithGradesVM csemp = new ClassScheduleEMPWithGradesVM();
                        csemp.EmpId = emp.Id;
                        csemp.Disclaimer = rosterData.Disclaimer;
                        csemp.Interrupted = rosterData.Interrupted;
                        csemp.Grade = rosterData.Grade;
                        csemp.Score = rosterData.Score;
                        csemp.Restarted = rosterData.Restarted;
                        csemp.CompletedDate = rosterData.CompletedDate;
                        csemp.ReleaseDate = rosterData.ReleaseDate;
                        csemp.EmpEmail = emp.Person.Username;
                        csemp.EmployeeName = emp.Person.FirstName + " " + emp.Person.LastName;
                        csemp.Image = emp.Person.Image;
                        csEMPs.Add(csemp);
                    }
                    else
                    {
                        ClassScheduleEMPWithGradesVM csemp = new ClassScheduleEMPWithGradesVM();
                        csemp.EmpId = emp.Id;
                        csemp.Disclaimer = false;
                        csemp.Interrupted = false;
                        csemp.Grade = null;
                        csemp.Score = null;
                        csemp.Restarted = false;
                        csemp.CompletedDate = null;
                        csemp.ReleaseDate = null;
                        csemp.EmpEmail = emp.Person.Username;
                        csemp.EmployeeName = emp.Person.FirstName + " " + emp.Person.LastName;
                        csemp.Image = emp.Person.Image;
                        csEMPs.Add(csemp);
                    }
                }
            }

            return csEMPs;
        }

        public async Task<List<ClassSchedule_Roster>> UpdateBulkGradeAsync(int id, int testId, ClassRoasterUpdateOptions options)
        {
            int testTypeId = 0;
            switch (options.TestItemType)
            {
                case "Pretest":
                default:
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Pretest")).FirstOrDefault().Id;
                    break;
                case "Test":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Final Test")).FirstOrDefault().Id;
                    break;
                case "Retake":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Retake")).FirstOrDefault().Id;
                    break;
                case "CBT":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "CBT")).FirstOrDefault().Id;
                    break;
                case "StudentEvaluation":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "StudentEvaluation")).FirstOrDefault().Id;
                    break;
            }

            var modifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            List<ClassSchedule_Roster> rosters = new List<ClassSchedule_Roster>();
            var emps = await _class_empService.FindQuery(x => x.ClassScheduleId == id && !x.Deleted).Select(s => s.Employee).ToListAsync();
            foreach (var emp in emps)
            {
                var roster = await _classRosterService.FindQuery(x => x.ClassScheduleId == id && x.EmpId == emp.Id && x.TestTypeId == testTypeId && x.TestId == testId && !x.Deleted).FirstOrDefaultAsync();
                //if(roster != null && roster.Score != null)
                if (roster != null)
                {
                    roster.CompleteTest(roster.CompletedDate, roster.Score, options.BulkGrade.ToUpper(), modifiedBy);

                    var validationResult = await _classRosterService.UpdateAsync(roster);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    var statusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Not Started").FirstOrDefault().Id;
                    roster = new ClassSchedule_Roster { Active = true, ClassScheduleId = (int)options.ClassId, CompletedDate = null, Deleted = false, Disclaimer = false, EmpId = emp.Id, Interrupted = false, IsReleased = false, ReleaseDate = null, Restarted = false, Score = null, StatusId = statusId, TestId = (int)options.TestId, Grade = options.BulkGrade.ToUpper(), TestTypeId = testTypeId, RetakeOrder = options.RetakeOrder };

                    roster.CompleteTest(options.CompDate, options.Score, options.BulkGrade.ToUpper(), modifiedBy);

                    var validationResult = await _classRosterService.AddAsync(roster);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }

                rosters.Add(roster);
            }

            rosters = rosters.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, ClassSchedule_RosterOperations.Read).Result.Succeeded).ToList();
            return rosters;
        }

        public async Task<ClassSchedule_Roster> UpdateGradeAsync(int empId, ClassRoasterUpdateOptions options)
        {
            int testTypeId = 0;
            var statusId = await _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Not Started").Select(s => s.Id).FirstOrDefaultAsync();
            switch (options.TestType)
            {
                case "Pretest":
                default:
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Pretest")).FirstOrDefault().Id;
                    break;
                case "Test":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Final Test")).FirstOrDefault().Id;
                    break;
                case "Retake":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Retake")).FirstOrDefault().Id;
                    break;
                case "CBT":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "CBT")).FirstOrDefault().Id;
                    break;
                case "StudentEvaluation":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "StudentEvaluation")).FirstOrDefault().Id;
                    break;
            }
            var classRoster = await _classRosterService.FindQuery(x => x.EmpId == empId && x.TestId == options.TestId && x.TestTypeId == testTypeId && options.ClassId == x.ClassScheduleId && !x.Deleted && x.RetakeOrder == options.RetakeOrder).FirstOrDefaultAsync();
            var modifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;

            if (classRoster != null)
            {               
                classRoster.CompleteTest(classRoster.CompletedDate, classRoster.Score, options.Grade.ToUpper(), modifiedBy);

                var validationResult = await _classRosterService.UpdateAsync(classRoster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                classRoster = new ClassSchedule_Roster((int)options.ClassId, (int)options.TestId, testTypeId, empId, false, options.Grade.ToUpper(), false, false, null, null, null, null, statusId);
                classRoster.CompleteTest(options.CompDate, options.Score, options.Grade.ToUpper(), modifiedBy);

                classRoster.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classRoster.CreatedDate = DateTime.Now;
                classRoster.RetakeOrder = options.RetakeOrder;

                var validationResult = await _classRosterService.AddAsync(classRoster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            return classRoster;
        }

        public async Task<ClassSchedule_Roster> UpdateCompDateAsync(int empId, ClassRoasterUpdateOptions options)
        {
            int testTypeId = 0;
            var statusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Not Started").FirstOrDefault().Id;
            switch (options.TestItemType)
            {
                case "Pretest":
                default:
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Pretest")).FirstOrDefault().Id;
                    break;
                case "Test":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Final Test")).FirstOrDefault().Id;
                    break;
                case "Retake":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Retake")).FirstOrDefault().Id;
                    break;
                case "CBT":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "CBT")).FirstOrDefault().Id;
                    break;
                case "StudentEvaluation":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "StudentEvaluation")).FirstOrDefault().Id;
                    break;
            }
            var modifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var classRoster = await _classRosterService.FindQuery(x => x.EmpId == empId && x.TestId == options.TestId && x.TestTypeId == testTypeId && options.ClassId == x.ClassScheduleId && !x.Deleted && x.RetakeOrder == options.RetakeOrder).FirstOrDefaultAsync();
            if (classRoster != null)
            {
                classRoster.CompleteTest(options.CompDate.HasValue ? options.CompDate.Value.ToUniversalTime() : null, classRoster.Score, classRoster.Grade, modifiedBy);
                classRoster.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classRoster.ModifiedDate = DateTime.Now;
                var validationResult = await _classRosterService.UpdateAsync(classRoster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                classRoster = new ClassSchedule_Roster((int)options.ClassId, (int)options.TestId, testTypeId, empId, false, null, false, false, options.CompDate.HasValue ? options.CompDate.Value.ToUniversalTime() : null, null, null, null, statusId);
                classRoster.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classRoster.CreatedDate = DateTime.Now;
                classRoster.RetakeOrder = options.RetakeOrder;
                var validationResult = await _classRosterService.AddAsync(classRoster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            return classRoster;
        }

        public async Task<ClassSchedule_Roster> UpdateScoreAsync(int empId, ClassRoasterUpdateOptions options)
        {
            int testTypeId = 0;
            var statusId = await _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Not Started").Select(s => s.Id).FirstOrDefaultAsync();
            switch (options.TestType)
            {
                case "Pretest":
                default:
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Pretest")).FirstOrDefault().Id;
                    break;
                case "Test":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Final Test")).FirstOrDefault().Id;
                    break;
                case "Retake":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Retake")).FirstOrDefault().Id;
                    break;
                case "CBT":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "CBT")).FirstOrDefault().Id;
                    break;
                case "StudentEvaluation":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "StudentEvaluation")).FirstOrDefault().Id;
                    break;
            }
            var classRoster = await _classRosterService.FindQuery(x => x.EmpId == empId && x.TestId == options.TestId && x.TestTypeId == testTypeId && options.ClassId == x.ClassScheduleId && !x.Deleted && x.RetakeOrder == options.RetakeOrder).FirstOrDefaultAsync();
            var modifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;

            if (classRoster != null)
            {
                classRoster.CompleteTest(classRoster.CompletedDate, options.Score, classRoster.Grade, modifiedBy);

                var validationResult = await _classRosterService.UpdateAsync(classRoster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                classRoster = new ClassSchedule_Roster((int)options.ClassId, (int)options.TestId, testTypeId, empId, false, null, false, false, null, null, options.Score, null, statusId);
                classRoster.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classRoster.CreatedDate = DateTime.Now;

                classRoster.CompleteTest(options.CompDate, options.Score, options.Grade, modifiedBy);

                classRoster.RetakeOrder = options.RetakeOrder;
                var validationResult = await _classRosterService.AddAsync(classRoster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }

            return classRoster;
        }

        public async System.Threading.Tasks.Task RemoveEmployeeAsync(int classId, int testId, string testType, int empId)
        {
            int testTypeId = 0;
            switch (testType.Trim().ToLower())
            {
                case "pretest":
                default:
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Pretest")).FirstOrDefault().Id;
                    break;
                case "test":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Final Test")).FirstOrDefault().Id;
                    break;
                case "retake":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Retake")).FirstOrDefault().Id;
                    break;
                case "cbt":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "CBT")).FirstOrDefault().Id;
                    break;
                case "studentevaluation":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "StudentEvaluation")).FirstOrDefault().Id;
                    break;
            }

            var roasters = await _classRosterService.FindQuery(x => x.TestTypeId == testTypeId && x.TestId == testId && x.ClassScheduleId == classId && x.EmpId == empId).ToListAsync();
            foreach (var roaster in roasters)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, roaster, ClassSchedule_RosterOperations.Delete);
                if (authResult.Succeeded)
                {
                    var validationResult = await _classRosterService.DeleteAsync(roaster);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
        }

        public async Task<ClassSchedule_Roster> ReleaseTestAsync(int empId, ClassRoasterUpdateOptions options)
        {
            int testTypeId = 0;
            var statusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Not Started").FirstOrDefault().Id;
            string testType = "";
            switch (options.TestType)
            {
                case "Pretest":
                default:
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Pretest")).FirstOrDefault().Id;
                    testType = "pretest";
                    break;
                case "Test":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Final Test")).FirstOrDefault().Id;
                    testType = "test";
                    break;
                case "Retake":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Retake")).FirstOrDefault().Id;
                    testType = "retake";
                    break;
                case "CBT":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "CBT")).FirstOrDefault().Id;
                    testType = "cbt";
                    break;
            }
            var classRoster = new ClassSchedule_Roster();
            var order = options.RetakeOrder;
            if (testType == "retake")
            {
                classRoster = await _classRosterService.FindQuery(x => x.EmpId == empId && x.TestId == options.TestId && x.TestTypeId == testTypeId && options.ClassId == x.ClassScheduleId && !x.Deleted && x.RetakeOrder == options.RetakeOrder).FirstOrDefaultAsync();
            }
            else
            {
                classRoster = await _classRosterService.FindQuery(x => x.EmpId == empId && x.TestId == options.TestId && x.TestTypeId == testTypeId && options.ClassId == x.ClassScheduleId && !x.Deleted).FirstOrDefaultAsync();
            }

            if (classRoster != null)
            {
                classRoster.IsReleased = true;
                classRoster.ReleaseDate = options.ReleaseDate;
                classRoster.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classRoster.ModifiedDate = DateTime.Now;
                var validationResult = await _classRosterService.UpdateAsync(classRoster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

            }
            else
            {
                classRoster = new ClassSchedule_Roster((int)options.ClassId, (int)options.TestId, testTypeId, empId, false, null, false, false, null, null, options.Score, true, statusId);
                classRoster.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classRoster.CreatedDate = DateTime.Now;
                classRoster.ReleaseDate = DateTime.UtcNow;
                classRoster.RetakeOrder = order;
                var validationResult = await _classRosterService.AddAsync(classRoster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }


            //  if (testType == "pretest")
            //{
            //    var emps = await _class_empService.FindQuery(x => x.ClassScheduleId == options.ClassId && x.EmployeeId == empId && !x.Deleted).FirstOrDefaultAsync();
            //    emps.PreTestStatus = 
            //}
            return classRoster;
        }

        public async Task<ClassSchedule_Roster> RecallTestAsync(int empId, ClassRoasterUpdateOptions options)
        {
            int testTypeId = 0;
            var statusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Not Started").FirstOrDefault().Id;
            switch (options.TestType)
            {
                case "Pretest":
                default:
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Pretest")).FirstOrDefault().Id;
                    break;
                case "Test":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Final Test")).FirstOrDefault().Id;
                    break;
                case "Retake":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "Retake")).FirstOrDefault().Id;
                    break;
                case "CBT":
                    testTypeId = (await _testtypeService.FindAsync(x => x.Description == "CBT")).FirstOrDefault().Id;
                    break;
            }
            var classRoster = await _classRosterService.FindQuery(x => x.EmpId == empId && x.TestId == options.TestId && x.TestTypeId == testTypeId && options.ClassId == x.ClassScheduleId && !x.Deleted && x.RetakeOrder == options.RetakeOrder).FirstOrDefaultAsync();
            if (classRoster != null)
            {
                classRoster.IsReleased = false;
                classRoster.ReleaseDate = null;
                classRoster.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classRoster.ModifiedDate = DateTime.Now;
                var validationResult = await _classRosterService.UpdateAsync(classRoster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                classRoster = new ClassSchedule_Roster((int)options.ClassId, (int)options.TestId, testTypeId, empId, false, null, false, false, null, null, options.Score, false, statusId);
                classRoster.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                classRoster.CreatedDate = DateTime.Now;
                classRoster.ReleaseDate = null;
                var validationResult = await _classRosterService.AddAsync(classRoster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }

            return classRoster;
        }

        public async Task<List<RosterOverviewVM>> GetRosterOverviewDataAsync(int classId)
        {
            var classSchedules = await _class_empService.FindQueryWithIncludeAsync(x => x.ClassScheduleId == classId && x.IsEnrolled == true, new string[] { "ScormRegistrations" }).ToListAsync();
            var currClass = await _classScheduleService.FindQuery(x => x.Id == classId).Select(s => new ClassSchedule { Id = s.Id, Active = s.Active, EndDateTime = s.EndDateTime, ILAID = s.ILAID }).FirstOrDefaultAsync();
            bool isSelfPaced = false;
            if (currClass != null)
            {
                isSelfPaced = await _ilaService.FindQuery(x => x.Id == currClass.ILAID).Select(s => s.IsSelfPaced).FirstOrDefaultAsync();
            }
            List<RosterOverviewVM> overviews = new List<RosterOverviewVM>();
            foreach (var schedule in classSchedules)
            {
                var rosters = (await _classRosterService.FindWithIncludeAsync(x => x.EmpId == schedule.EmployeeId && x.ClassScheduleId == classId, new string[] { "TestType" })).ToList();
                RosterScoreGradeModel rosterScoreGrade = await SetFinalScoreAndGradeValueAsync(schedule, rosters);
                var activeRegistration = schedule?.ScormRegistrations?.FirstOrDefault(r => r.Active);
                var overview = new RosterOverviewVM();
                overview.RetakeCount = 0;
                overview.TestStatus = "N/A";
                overview.PretestStatus = "N/A";
                overview.CbtStatus = activeRegistration?.RegistrationCompletion.ToString() ?? "N/A";
                overview.Grade = schedule.FinalGrade;
                overview.Score = schedule.FinalScore;
                overview.GradeNotes = schedule.GradeNotes;
                overview.EvaluationCompletedDate = schedule.CompletionDate;
                overview.ClassScheduleEmployeeId = schedule.Id;
                overview.TaskQualificationCompleted = schedule.PopulateOJTRecord;

                foreach (var roster in rosters)
                {
                    if (roster.TestType == null)
                    {
                        throw new BadHttpRequestException(message: _localizer["Test Type Not Found"]);
                    }
                    else
                    {

                        if (roster.CompletedDate != null)
                        {
                            overview.CompletedDate = true;

                        }
                        var status = await _classScheduleRosterStatusDomainService.FindQuery(x => x.Id == roster.StatusId).FirstOrDefaultAsync();
                        if (status != null)
                        {
                            switch (roster.TestType.Description)
                            {
                                case "Final Test":
                                    overview.TestStatus = status.Name;
                                    overview.isTestReleased = roster.IsReleased;
                                    break;
                                case "Pretest":
                                    overview.PretestStatus = status.Name;
                                    overview.isPreTestReleased = roster.IsReleased;
                                    break;
                            }
                        }
                    }
                }

                    var setting = await _classTestReleaseEmpSettingDomainService.FindQuery(x => x.ClassScheduleId == classId).FirstOrDefaultAsync();
                    if (setting != null && setting.RetakeEnabled)
                    {
                        overview.RetakeCount = setting.NumberOfRetakes ?? 0;
                    }


                var emp = await _employeeDomainService.FindQuery(x => x.Id == schedule.EmployeeId).FirstOrDefaultAsync();
                if (emp == null)
                {
                    throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
                }
                else
                {
                    var person = await _personService.FindQuery(x => x.Id == emp.PersonId).FirstOrDefaultAsync();
                    if (person == null)
                    {
                        throw new BadHttpRequestException(message: _localizer["Person Not Found"]);
                    }
                    else
                    {
                        overview.EmployeeEmail = person.Username;
                        overview.EmployeeImage = person.Image;
                        overview.EmployeeName = person.FirstName + " " + person.LastName;
                        overview.ClassEmployeeId = emp.Id;
                        overviews.Add(overview);
                    }
                }
            }
            return overviews;
        }

        public async Task<RosterScoreGradeModel> SetFinalScoreAndGradeValueAsync(ClassSchedule_Employee classSchedule_Employee, List<ClassSchedule_Roster> classSchedule_Rosters)
        {
            RosterScoreGradeModel rosterScoreGrade = new RosterScoreGradeModel();
            var testRosters = classSchedule_Rosters.Where(x => x.TestType.Description == "Final Test" || x.TestType.Description == "Retake").ToList();
            var cbtScormRegistration = (await _cbtScormRegistrationService.FindAsync(x => x.ClassScheduleEmployeeId == classSchedule_Employee.EmployeeId && x.RegistrationCompletion == CBT_ScormRegistrationCompletion.COMPLETED)).ToList();
            if (testRosters.Count() > 0)
            {
                var scoreGrade = testRosters.OrderByDescending(x => x.CompletedDate).FirstOrDefault();
                rosterScoreGrade.Score = scoreGrade.Score;
                rosterScoreGrade.Grade = scoreGrade.Grade;
            }
            else if (cbtScormRegistration.Count() > 0)
            {
                var scoreGrade = cbtScormRegistration.FirstOrDefault();
                rosterScoreGrade.Score = (int)scoreGrade.Score;
                rosterScoreGrade.Grade = scoreGrade.Grade;
            }
            return rosterScoreGrade;
        }


        #endregion Roster


        public async System.Threading.Tasks.Task SubmitEvaluationAsync(int classId, int evalId, int empId)
        {
            var evalRoster = await _cs_eval_rosterService.FindQuery(x => x.ClassScheduleId == classId && x.StudentEvaluationId == evalId && x.EmployeeId == empId).FirstOrDefaultAsync();
            if (evalRoster == null)
            {
                evalRoster = new ClassSchedule_Evaluation_Roster(null, DateTime.Now, classId, empId, true, true, true, evalId);
                evalRoster.IsAllowed = false;
                var auth = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, evalRoster, ClassSchedule_Evaluation_RosterOperations.Read);
                if (auth.Succeeded)
                {
                    var validationResult = await _cs_eval_rosterService.AddAsync(evalRoster);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            else
            {
                var auth = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, evalRoster, ClassSchedule_Evaluation_RosterOperations.Read);
                if (auth.Succeeded)
                {
                    evalRoster.IsCompleted = true;
                    evalRoster.IsAllowed = false;
                    evalRoster.CompletedDate = DateTime.Now;
                    var validationResult = await _cs_eval_rosterService.UpdateAsync(evalRoster);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
        }

        public async Task<List<EmployeeTestModel>> GetEmployeesTestAsync()
        {
            var list = new List<EmployeeTestModel>();
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var classScheduleRosters = await _classRosterService.GetReleasedByEmployeeUsernameAsync(userName);

            var model = classScheduleRosters.Select(r => new EmployeeTestModel()
            {
                DueDate = r.TestType.Description.ToUpper() == "PRETEST" ? r.ClassSchedule.EndDateTime : r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.GetDueDate(r.ClassSchedule.EndDateTime),
                Score = r.Score,
                Grade = r.Grade,
                TestId = r.TestId,
                TestType = r.TestType.Description,
                TestTypeId = r.TestTypeId,
                EmpId = r.EmpId,
                ILA = (r.ClassSchedule?.ILA?.Number ?? "N/A") + " - " + r.ClassSchedule?.ILA?.Name ?? "N/A",
                Instructpr = r.ClassSchedule?.Instructor?.InstructorName ?? "N/A",
                Location = r.ClassSchedule?.Location?.LocName ?? "N/A",
                ClassDate = r.ClassSchedule.StartDateTime,
                ClassScheduleId = r.ClassSchedule.Id,
                ILANumber = r.ClassSchedule?.ILA?.Number ?? "N/A",
                TestHours = r.Test.ILATraineeEvaluations.Where(x => x.TestId == r.TestId).Select(x => x.TestTimeLimitHours).FirstOrDefault(),
                TestMinutes = r.Test.ILATraineeEvaluations.Where(x => x.TestId == r.TestId).Select(x => x.TestTimeLimitMinutes).FirstOrDefault(),
                Status = r.Status.Name,
                ProviderName = r.ClassSchedule?.ILA?.Name ?? "N/A",
                CompletedDate = r.CompletedDate,
                Instructions = r.Test.ILATraineeEvaluations.Where(x => x.TestId == r.TestId).Select(s => s.TestInstruction).FirstOrDefault(),
                RosterId = r.Id,
                TestSubmittedAnswers = r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.ShowStudentSubmittedFinalTestAnswers,
                TestCorrectIncorrectAnswers = r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.ShowCorrectIncorrectFinalTestAnswers,
                PretestSubmittedAnswers = r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.ShowStudentSubmittedPreTestAnswers,
                PretestCorrectIncorrectAnswers = r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.ShowCorrectIncorrectPreTestAnswers,
                TestRetakeSubmittedAnswers = r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.ShowStudentSubmittedRetakeTestAnswers,
                TestRetakeCorrectIncorrectAnswers = r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.ShowCorrectIncorrectRetakeTestAnswers
            }).ToList();

            return model;
        }

        public async Task<List<ReviewTestModel>> ReviewTestAsync(int rosterId)
        {
            var classScheduleRoster = await _classRosterService.GetWithTestItemDetailsAsync(rosterId);

            var model = classScheduleRoster.Test.Test_Item_Links.OrderBy(o => o.Sequence).Select(r =>
                {
                    var random = classScheduleRoster.Test.RandomizeDistractors;
                    var response = classScheduleRoster.Responses.Where(s => s.TestItemId == r.TestItemId).FirstOrDefault();
                    var item = new ReviewTestModel()
                    {
                        testItem = _testApplicationService.TestItemToTestItemVM(r.TestItem,true,true),
                        Status = response == null ? "Incomplete" : "Complete"
                    };

                    //Multiple Choice Questions 
                    //True / False
                    //Short Answers
                    if (r.TestItem.TestItemTypeId == 1 || r.TestItem.TestItemTypeId == 3 || r.TestItem.TestItemTypeId == 4)
                    {
                        item.UserAnswer = response?.Selections?.FirstOrDefault() == null ? "" : response.Selections.First().UserAnswer;
                    }

                    //Fill in the Blank
                    if (r.TestItem.TestItemTypeId == 2)
                    {
                        item.BlankIndexWithAnswer = new List<FillintheBlank>();

                        foreach (var selection in response?.Selections.OrderBy(x => x.CorrectIndex))
                        {
                            item.BlankIndexWithAnswer.Add(new FillintheBlank()
                            {
                                CorrectIndex = selection.CorrectIndex ?? 0,
                                UserValue = selection.UserAnswer

                            });
                        }
                    }

                    //Match the Column
                    if (r.TestItem.TestItemTypeId == 5)
                    {
                        item.MatchValueWithCorrectValue = new List<MatchColumns>();

                        foreach (var selection in response?.Selections)
                        {
                            item.MatchValueWithCorrectValue.Add(new MatchColumns()
                            {
                                MatchValue = selection.MatchValue,
                                UserValue = selection.UserAnswer,
                                CorrectIndex = selection.CorrectIndex ?? 0
                            });
                        }
                    }

                    //Multiple Correct Answers
                    if (r.TestItem.TestItemTypeId == 6)
                    {
                        item.MultipleCorrectAnswer = new List<string>();

                        foreach (var selection in response?.Selections)
                        {
                            item.MultipleCorrectAnswer.Add(selection.UserAnswer);
                        }
                    }

                    return item;

                }).ToList();

            return model;
        }

        public async Task<ClassSchedule_Roster_Response> CreateRosterResponseAsync(EmpTestCreateOptions options)
        {
            var response = await _classScheduleRosterResponseService.GetByQuestionAndClassScheduleRoster(options.QuestionId, options.RosterId);
            if (response == null) response = new ClassSchedule_Roster_Response(options.RosterId, options.QuestionId, null);

            response.ClearSelection();

            var testItem = await _testItemDomainService.GetAsync(options.QuestionId);

            //Multiple Choice Questions
            //True / False
            //Short Answers
            if (testItem.TestItemTypeId == 1 || testItem.TestItemTypeId == 3 || testItem.TestItemTypeId == 4)
            {
                response.LoadSelection(options.UserAnswer.FirstOrDefault());
            }
            //Fill in the Blank
            else if (testItem.TestItemTypeId == 2)
            {
                foreach (var blank in options.BlankIndexWithAnwer)
                {
                    response.LoadSelection(blank.UserValue, blank.CorrectIndex);
                }
            }
            //Match the Column
            else if (testItem.TestItemTypeId == 5)
            {
                foreach (var match in options.MatchValueWithCorrectValue)
                {
                    response.LoadSelection(match.UserValue, match.MatchValue,match.CorrectIndex);
                }
            }
            //Multiple Correct Answers
            else if (testItem.TestItemTypeId == 6)
            {
                foreach (var ans in options.UserAnswer)
                {
                    response.LoadSelection(ans);
                }
            }


            if (response.Id > 0)
            {
                response.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);
                await _classScheduleRosterResponseService.UpdateAsync(response);
            }
            else
            {
                response.Create(_httpContextAccessor.HttpContext.User.Identity.Name);
                await _classScheduleRosterResponseService.AddAsync(response);
            }

            return response;

        }

        public async Task<EmployeeAnswerModel> GetTestAnswerAsync(int classId, int testId, int questionId, int rosterId)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var response = await _classScheduleRosterResponseService.GetByQuestionAndClassScheduleRoster(questionId, rosterId);

            if (response == null)
            {
                var employee = await _employeeDomainService.GetEmployeeByUsernameAsync(userName);
                var question = await _testItemDomainService.GetAsync(questionId);

                return new EmployeeAnswerModel()
                {
                    TestId = testId,
                    QuestionId = questionId,
                    TestItemTypeId = question.TestItemTypeId,
                    UserAnswer = null,
                    EmployeeId = employee.Id,
                };
            }

            EmployeeAnswerModel model = new EmployeeAnswerModel()
            {
                TestId = testId,
                QuestionId = questionId,
                EmployeeId = response.ClassSchedule_Roster.EmpId,
                TestTypeId = response.ClassSchedule_Roster.TestTypeId,
                TestItemTypeId = response.TestItem.TestItemTypeId
            };

            //Multiple Choice Questions
            //True / False
            //Short Answers
            if (model.TestItemTypeId == 1 || model.TestItemTypeId == 3 || model.TestItemTypeId == 4)
            {
                model.UserAnswer = response.Selections?.FirstOrDefault()?.UserAnswer;
            }
            //Fill in the Blank
            else if (model.TestItemTypeId == 2)
            {
                model.BlankIndexWithAnswer = new List<FillintheBlank>();

                foreach (var selection in response.Selections)
                {
                    model.BlankIndexWithAnswer.Add(new FillintheBlank()
                    {
                        CorrectIndex = selection.CorrectIndex.GetValueOrDefault(),
                        UserValue = selection.UserAnswer
                    });
                }
            }
            //Match the Column
            else if (model.TestItemTypeId == 5)
            {
                model.MatchValueWithCorrectValue = new List<MatchColumns>();

                foreach (var selection in response.Selections)
                {
                    model.MatchValueWithCorrectValue.Add(new MatchColumns()
                    {
                        MatchValue = selection.MatchValue,
                        UserValue = selection.UserAnswer,
                        CorrectIndex = selection.CorrectIndex ?? 0
                    });
                }

            }
            //Multiple Correct Answers
            else if (model.TestItemTypeId == 6)
            {
                model.MultipleCorrectAnswer = new List<string>();

                foreach (var selection in response.Selections)
                {
                    model.MultipleCorrectAnswer.Add(selection.UserAnswer);
                }
            }

            return model;
        }

        public async Task<List<SubmitTestModel>> SubmitTestAsync(int classId, int testId, int rosterId, DateTime completionDate)
        {
            var classScheduleRoster = await _classRosterService.GetWithTestItemDetailsAsync(rosterId);
            var classSchedule = await _classScheduleService.GetWithIncludeAsync(classScheduleRoster.ClassScheduleId.GetValueOrDefault(), new string[] { "ILA.Provider" });
            var classScheduleEmployee = await _class_empService.GetByEmployeeIdAndClassScheduleIdAsync(classScheduleRoster.EmpId, classId);
            var testSettings = (await _classTestReleaseEmpSettingDomainService.FindAsync(x=>x.ClassScheduleId==classId)).FirstOrDefault();

            foreach (var testItemLink in classScheduleRoster.Test.Test_Item_Links)
            {
                var response = classScheduleRoster.Responses.Where(s => s.TestItemId == testItemLink.TestItemId).FirstOrDefault();
                if (response == null) continue;
                //Multiple Choice Questions 
                if (testItemLink.TestItem.TestItemTypeId == 1)
                {
                    var userAsnwer = response.Selections.FirstOrDefault()?.UserAnswer;
                    var correctAnswer = response.TestItem.TestItemMCQs.Where(r => r.IsCorrect).First().ChoiceDescription;
                    if (string.IsNullOrEmpty(userAsnwer))
                    {
                        response.MarkAsIncomplete();
                    }
                    else
                    {
                        response.MarkAsComplete(correctAnswer.ToUpper() == userAsnwer.ToUpper());
                    }
                }
                //Fill in the Blank
                else if (testItemLink.TestItem.TestItemTypeId == 2)
                {
                    var blanks = testItemLink.TestItem.TestItemFillBlanks;
                    var responseSelects = response.Selections;

                    if (blanks.Count() != responseSelects.Count())
                    {
                        response.MarkAsIncomplete();
                    }
                    else
                    {
                        bool correct = true;
                        foreach (var blank in blanks)
                        {
                            var selection = responseSelects.Where(r => r.CorrectIndex == blank.CorrectIndex).FirstOrDefault();
                            correct = selection?.UserAnswer.Trim().ToUpper() == blank.Correct.Trim().ToUpper();
                            if (!correct) break;
                        }

                        response.MarkAsComplete(correct);
                    }

                }
                //True / False
                else if (testItemLink.TestItem.TestItemTypeId == 3)
                {
                    var userAsnwer = response.Selections.FirstOrDefault()?.UserAnswer;
                    var correctAnswer = response.TestItem.TestItemTrueFalses.Where(r => r.IsCorrect).First().Choices;
                    if (string.IsNullOrEmpty(userAsnwer))
                    {
                        response.MarkAsIncomplete();
                    }
                    else
                    {
                        response.MarkAsComplete(correctAnswer.ToUpper() == userAsnwer.ToUpper());
                    }
                }
                //Short Answers
                else if (testItemLink.TestItem.TestItemTypeId == 4)
                {
                    var userAsnwer = response.Selections.FirstOrDefault()?.UserAnswer;
                    var correctAnswers = response.TestItem.TestItemShortAnswers;

                    if (string.IsNullOrEmpty(userAsnwer))
                    {
                        response.MarkAsIncomplete();
                    }
                    else
                    {
                        bool correct = true;
                        foreach (var correctAnswer in correctAnswers.Where(r => r.AcceptableResponses == 1))
                        {
                            if (correctAnswer.IsCaseSensitive)
                            {
                                correct = userAsnwer.Contains(correctAnswer.Responses);
                            }
                            else
                            {
                                correct = userAsnwer.ToUpper().Contains(correctAnswer.Responses.ToUpper());
                            }

                            if (!correct)
                            {
                                response.MarkAsComplete(correct);
                                break;
                            }
                        }

                        response.MarkAsComplete(correct);
                    }
                }
                //Match the Column
                else if (testItemLink.TestItem.TestItemTypeId == 5)
                {
                    var columns = testItemLink.TestItem.TestItemMatches;
                    var responseSelects = response.Selections;

                    bool complete = true;
                    bool correct = true;
                    foreach (var column in columns.Where(r => r.CorrectValue != null))
                    {
                        var selection = responseSelects.Where(r => r.CorrectIndex == column.Number).FirstOrDefault();
                        complete = selection != null;

                        if (!complete)
                        {
                            correct = false;
                            break;
                        }

                        if (selection.UserAnswer is null || (selection.UserAnswer.ToUpper() != column.CorrectValue.ToString().ToUpper()))
                        {
                            correct = false;
                            break;
                        }
                    }

                    if (complete)
                        response.MarkAsComplete(correct);

                    else response.MarkAsIncomplete();
                }
                //Multiple Correct Answers
                else if (testItemLink.TestItem.TestItemTypeId == 6)
                {
                    var answers = testItemLink.TestItem.TestItemMCQs;
                    var responseSelects = response.Selections;

                    if (!responseSelects.Any())
                    {
                        response.MarkAsIncomplete();
                    }
                    else
                    {
                        bool correct = true;
                        var correctAnwers = answers.Where(r => r.IsCorrect);
                        if (responseSelects.Count() == correctAnwers.Count())
                        {
                            foreach (var answer in correctAnwers)
                            {
                                var selection = responseSelects.Where(r => r.UserAnswer.ToUpper() == answer.ChoiceDescription.ToUpper()).FirstOrDefault();
                                correct = selection != null;

                                if (!correct) break;
                            }
                        }
                        else
                        {
                            correct = false;
                        }

                        response.MarkAsComplete(correct);
                    }
                }
            }
            var correctAnswersCount = classScheduleRoster.Responses.Where(x => x.IsCorrect.GetValueOrDefault()).Count();
            var totalQuestions = classScheduleRoster.Test.Test_Item_Links.Count();
            float actualScore = totalQuestions == 0 ? 0 : ((float)correctAnswersCount / totalQuestions) * 100;
            int score = ((int)actualScore);
            var passingScore = classScheduleRoster.TestTypeId == 1 ? testSettings.PreTestScore : Convert.ToInt32(String.IsNullOrEmpty(testSettings.FinalTestPassingScore) ? "0" : testSettings.FinalTestPassingScore);

            if (passingScore == 0)
            {
                var settings = (await _clientUserSettingsGeneralSettingsService.AllAsync()).First();
                passingScore = (int)settings.CompanySpecificCoursePassingScore;
            }

            string passFail = score >= passingScore ? "P" : "F";
            classScheduleRoster.CompleteTest(DateTime.UtcNow, score, passFail);

            if (passFail == "F" && classScheduleRoster.TestTypeId != 1)
                await AutoReleaseTestEnable(classSchedule.ILAID, classSchedule.Id, testId, classScheduleRoster.EmpId);

            if (classScheduleRoster.TestTypeId != 1)
            {
                classScheduleEmployee.CompleteClass(DateTime.UtcNow, passFail, score);
            }

            await _class_empService.UpdateAsync(classScheduleEmployee);
            await _classRosterService.UpdateAsync(classScheduleRoster);

            bool showCorrectIncorrectAnswers =
                    classScheduleRoster.TestTypeId == 1 ? testSettings.ShowCorrectIncorrectPreTestAnswers :
                    classScheduleRoster.TestTypeId == 2 ? testSettings.ShowCorrectIncorrectFinalTestAnswers :
                    classScheduleRoster.TestTypeId == 3 ? testSettings.ShowCorrectIncorrectRetakeTestAnswers : false;

            bool showSubmittedAnswers =
                    classScheduleRoster.TestTypeId == 1 ? testSettings.ShowStudentSubmittedPreTestAnswers :
                    classScheduleRoster.TestTypeId == 2 ? testSettings.ShowStudentSubmittedFinalTestAnswers :
                    classScheduleRoster.TestTypeId == 3 ? testSettings.ShowStudentSubmittedRetakeTestAnswers : false;

            //build model
            List<SubmitTestModel> model = classScheduleRoster.Test.Test_Item_Links.Select(r =>
            {
                var testItemResponse = classScheduleRoster.Responses.Where(s => s.TestItemId == r.TestItemId).FirstOrDefault();
                return new SubmitTestModel()
                {
                    BlankIndexWithAnswer = r.TestItem.TestItemTypeId == 2 && testItemResponse != null ? testItemResponse.Selections.Select(s => new FillintheBlank()
                    {
                        CorrectIndex = s.CorrectIndex.GetValueOrDefault(),
                        UserValue = s.UserAnswer
                    }).ToList() : new List<FillintheBlank>(),
                    FillInTheBlankkAnswer = showCorrectIncorrectAnswers && r.TestItem.TestItemTypeId == 2 ?
                                             r.TestItem.TestItemFillBlanks.Select(s => s.Correct).ToList() : new List<string>(),

                    MatchValueWithCorrectValue = r.TestItem.TestItemTypeId == 5 && testItemResponse != null ? testItemResponse.Selections.Select(s => new MatchColumns()
                    {
                        MatchValue = s.MatchValue,
                        UserValue = s.UserAnswer,
                        CorrectIndex = s.CorrectIndex??0
                    }).ToList() : new List<MatchColumns>(),
                    MatchValueAnswer = showCorrectIncorrectAnswers && r.TestItem.TestItemTypeId == 5 ? r.TestItem.TestItemMatches.Select(s => s.CorrectValue).ToList() : new List<char?>(),

                    MultipleCorrectAnswer = r.TestItem.TestItemTypeId == 6 && testItemResponse != null ? testItemResponse.Selections.Select(s => s.UserAnswer).ToList() : new List<string>(),
                    MultiCorrectAnswers = showCorrectIncorrectAnswers && r.TestItem.TestItemTypeId == 6 || r.TestItemId == 1 ? r.TestItem.TestItemMCQs.Where(s => s.IsCorrect).Select(s => s.ChoiceDescription).ToList() : new List<string>(),

                    ShortAnswer = showCorrectIncorrectAnswers && r.TestItem.TestItemTypeId == 4 ? (r.TestItem.TestItemShortAnswers.Where(x => x.AcceptableResponses == 1).OrderBy(x => x.Number).Select(x => x.Responses)).ToList() : new List<string>(),
                    TrueFalseAnswer = showCorrectIncorrectAnswers && r.TestItem.TestItemTypeId == 3 ? (r.TestItem.TestItemTrueFalses.Where(x => x.IsCorrect).FirstOrDefault()?.Choices) : "",
                    UserAnswer = testItemResponse != null ? testItemResponse.Selections.FirstOrDefault()?.UserAnswer : "",
                    MCQAnswer = showCorrectIncorrectAnswers && r.TestItem.TestItemTypeId == 1 ? (r.TestItem.TestItemMCQs.Where(x => x.IsCorrect).FirstOrDefault()?.ChoiceDescription) : "",

                    ILANumber = classSchedule.ILA.Number,
                    showCorrectIncorrectAnswers = showCorrectIncorrectAnswers,
                    showSubmittedAnswers = showSubmittedAnswers,
                    ClearDescription = ReplaceCharactersWithNbsp(r.TestItem.Description),
                    CompletionStatus = testItemResponse != null && testItemResponse.IsComplete ? "Complete" : "Incomplete",
                    Correct = testItemResponse != null ? testItemResponse.IsCorrect.GetValueOrDefault() : false,
                    EndDate = classSchedule.EndDateTime,
                    MaximumScore = 100,
                    PassFailStatus = classScheduleRoster.Grade,
                    PassingScore = passingScore,
                    ProviderName = classSchedule.ILA.Provider.Name,
                    StartDate = classSchedule.StartDateTime,
                    testItem = _testApplicationService.TestItemToTestItemVM(r.TestItem,true),
                    TotalScore = score,
                    TotalTestDuration = classScheduleRoster.TotalTestDuration
                };
            }).ToList();

            return model;
        }

        public async System.Threading.Tasks.Task AutoReleaseTestEnable(int? ilaId, int classId, int testId, int employeeId)
        {
            var settings2 = await _classTestReleaseEmpSettingDomainService.FindQueryWithIncludeAsync(x => x.ClassScheduleId == classId, new string[] { "ClassSchedule_TestReleaseEMPSetting_RetakeLinks" }).FirstOrDefaultAsync();
            if (settings2.AutoReleaseRetake == true)
            {
                for (int i = 0; i < settings2.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.Count; i++)
                {
                    var retakeList = await _classRosterService.FindQuery(x => x.TestId == settings2.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.ToList()[i].RetakeTestId && x.ClassScheduleId == classId && x.EmpId == employeeId && x.RetakeOrder == i + 1).FirstOrDefaultAsync();
                    // if no retake order then release test
                    if (retakeList == null)
                    {
                        var retakeOption = new ClassRoasterUpdateOptions();
                        retakeOption.ClassId = classId;
                        retakeOption.TestId = settings2.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.ToList()[i].RetakeTestId;
                        retakeOption.TestType = "Retake";
                        retakeOption.RetakeOrder = i + 1;

                        await ReleaseTestAsync(employeeId, retakeOption);
                        break;
                    }
                    else
                    {

                        //main if condition if the retake is released
                        if (retakeList.IsReleased == true)
                        {
                            if (retakeList.CompletedDate == null && retakeList.Grade == null)
                            {
                                break;
                            }
                        }
                        else if (retakeList.IsReleased == false)
                        {
                            var retakeOption = new ClassRoasterUpdateOptions();
                            retakeOption.ClassId = classId;
                            retakeOption.TestId = settings2.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.ToList()[i].RetakeTestId;
                            retakeOption.TestType = "Retake";
                            retakeOption.RetakeOrder = i + 1;

                            await ReleaseTestAsync(employeeId, retakeOption);
                            break;
                        }
                    }
                }
            }
        }

        public async Task<ClassSchedule_Roster> ExitTestAsync(int rosterId)
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            var empTest = await _classRosterService.GetAsync(rosterId);
            empTest.MarkAsIncomplete(username);
            await _classRosterService.UpdateAsync(empTest);
            return empTest;
        }

        static string ReplaceCharactersWithNbsp(string input)
        {
            // Replace each character with &nbsp; inside <u> tags
            string pattern = "<u>(.*?)</u>";
            string replacedString = System.Text.RegularExpressions.Regex.Replace(input, pattern, match =>
            {
                string contentInsideUTags = match.Groups[1].Value;
                string nbsp = string.Join(" ", contentInsideUTags.Select(c => "&nbsp;"));
                return "<u>" + nbsp + "</u>";
            });

            return replacedString;
        }
    }
}
