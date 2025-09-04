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
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using IEmpTestDomainService = QTD2.Domain.Interfaces.Service.Core.IEmpTestService;
using ITestItemTypeDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemTypeService;
using IEmpTestReleaseSettingsDomainService = QTD2.Domain.Interfaces.Service.Core.ITestReleaseEMPSettingsService;
using ITestDomainService = QTD2.Domain.Interfaces.Service.Core.ITestService;
using ITestItemLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITest_Item_LinkService;
using ITestItemDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemService;
using ITestReleaseEMPSettingsDomainService = QTD2.Domain.Interfaces.Service.Core.ITestReleaseEMPSettingsService;
using IILATraineeEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using IClassScheduleRosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_RosterService;
using IClassScheduleRosterStatusDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Roster_StatusesService;
using IClassSchedule_EmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IInstructorDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_Service;
using ILocationDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_Service;
using IProviderDomainService = QTD2.Domain.Interfaces.Service.Core.IProviderService;
using ITestItemShortAnswerDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemShortAnswerService;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using QTD2.Infrastructure.Model.EmployeeTest;
using QTD2.Infrastructure.Model.TestItem;
using System.Text.RegularExpressions;

namespace QTD2.Application.Services.Shared
{
    //public class EmpTestService : IEmpTestService
    //{
    //    private readonly IClassRosterDomainService _classRosterService;
    //    private readonly Employee _employee;
    //    private readonly Test _test;
    //    private readonly ClassSchedule_Roster _roster;
    //    private readonly IEmployeeDomainService _employeeService;
    //    private readonly IEmpTestDomainService _empTestService;
    //    private readonly ITestTypeDomainService _testtypeService;
    //    private readonly IStringLocalizer<ClassRosterService> _localizer;
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    private readonly IAuthorizationService _authorizationService;
    //    private readonly UserManager<AppUser> _userManager;
    //    private readonly ITestItemTypeDomainService _testItemTypeSercice;
    //    private readonly IPersonDomainService _personService;
    //    private readonly IEmpTestReleaseSettingsDomainService _empTestSettingsIlaService;
    //    private readonly ITestDomainService _testService;
    //    private readonly ITestItemLinkDomainService _testItemLinkService;
    //    private readonly ITestItemDomainService _testItem_Service;
    //    private readonly TestItem _testItem;
    //    private readonly ITestReleaseEMPSettingsDomainService _testReleaseEMPSettingsService;
    //    private readonly IILATraineeEvaluationDomainService _iLATraineeEvaluationDomainService;
    //    private readonly IClassScheduleDomainService _classScheduleDomainService;
    //    private readonly IClassRosterService _classRosterappService;
    //    private readonly IClassScheduleRosterDomainService _classScheduleRosterDomainService;
    //    private readonly IClassScheduleRosterStatusDomainService _classScheduleRosterStatusDomainService;
    //    private readonly IEMPReleaseCheckService _empReleaseCheckService;
    //    private readonly IClassSchedule_EmployeeDomainService _classSchedule_empService;
    //    private readonly IILADomainService _ilaService;
    //    private readonly IInstructorDomainService _instructorService;
    //    private readonly ILocationDomainService _locationService;
    //    private readonly IProviderDomainService _providerService;
    //    private readonly ITestItemShortAnswerDomainService _testItemShortAnswersService;


    //    public EmpTestService(IClassRosterDomainService classRosterService, IStringLocalizer<ClassRosterService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager, ITestTypeDomainService testtypeService, IEmployeeDomainService employeeService, ITestItemTypeDomainService testItemTypeSercice, IPersonDomainService personService, IEmpTestDomainService empTestService, IEmpTestReleaseSettingsDomainService empTestSettingsService, ITestDomainService testService, ITestItemLinkDomainService testItemLinkService, ITestItemDomainService testItemService, ITestReleaseEMPSettingsDomainService testReleaseEMPSettingsService, IILATraineeEvaluationDomainService iLATraineeEvaluationDomainService, IClassScheduleDomainService classScheduleDomainService, IClassRosterService classRosterappService, IClassRosterDomainService classScheduleRosterDomainService, IClassScheduleRosterStatusDomainService classScheduleRosterStatusDomainService, IEMPReleaseCheckService empReleaseCheckService, IClassSchedule_EmployeeDomainService classSchedule_empService, IILADomainService ilaService, IInstructorDomainService instructorService, ILocationDomainService locationService, IProviderDomainService providerService, ITestItemShortAnswerDomainService testItemShortAnswersService)
    //    {
    //        _classRosterService = classRosterService;
    //        _testtypeService = testtypeService;
    //        _employee = new Employee();
    //        _localizer = localizer;
    //        _httpContextAccessor = httpContextAccessor;
    //        _authorizationService = authorizationService;
    //        _userManager = userManager;
    //        _employeeService = employeeService;
    //        _roster = new ClassSchedule_Roster();
    //        _test = new Test();
    //        _testItemTypeSercice = testItemTypeSercice;
    //        _personService = personService;
    //        _empTestService = empTestService;
    //        _empTestSettingsIlaService = empTestSettingsService;
    //        _testService = testService;
    //        _testItemLinkService = testItemLinkService;
    //        _testItem_Service = testItemService;
    //        _testItem = new TestItem();
    //        _testReleaseEMPSettingsService = testReleaseEMPSettingsService;
    //        _iLATraineeEvaluationDomainService = iLATraineeEvaluationDomainService;
    //        _classScheduleDomainService = classScheduleDomainService;
    //        _classRosterappService = classRosterappService;
    //        _classScheduleRosterDomainService = classScheduleRosterDomainService;
    //        _classScheduleRosterStatusDomainService = classScheduleRosterStatusDomainService;
    //        _empReleaseCheckService = empReleaseCheckService;
    //        _classSchedule_empService = classSchedule_empService;
    //        _ilaService = ilaService;
    //        _instructorService = instructorService;
    //        _locationService = locationService;
    //        _providerService = providerService;
    //        _testItemShortAnswersService = testItemShortAnswersService;
    //    }

    //    public async Task<List<EmployeeTestModel>> GetEmployeesTestAsync()
    //    {

    //        var list = new List<EmployeeTestModel>();
    //        var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

    //        var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

    //        if (person != null)
    //        {
    //            var employee = await _employeeService.FindQueryWithIncludeAsync(x => x.PersonId == person.Id, new string[] { "ClassSchedule_Employee.PreTestStatus", "ClassSchedule_Employee.CBTStatus", "ClassSchedule_Employee.TestStatus", "ClassSchedule_Employee.ReTakeStatus" }).FirstOrDefaultAsync();
    //            //await _empReleaseCheckService.CheckTestRelease();

    //            var obj_list = (await _classRosterService.FindAsync(x => x.EmpId == employee.Id)).ToList();

    //            if (obj_list != null && obj_list.Count > 0)
    //            {
    //                for (int i = 0; i < obj_list.Count; i++)
    //                {
    //                    obj_list[i].Test = await _testService.FindQuery(x => x.Id == obj_list[i].TestId).FirstOrDefaultAsync();
    //                    if (obj_list[i].Test != null)
    //                    {
    //                        obj_list[i].Test.ILATraineeEvaluations = await _iLATraineeEvaluationDomainService.FindQuery(x => x.TestId == obj_list[i].TestId).ToListAsync();

    //                        obj_list[i].TestType = await _testtypeService.FindQuery(x => x.Id == obj_list[i].TestTypeId).FirstOrDefaultAsync();
    //                        obj_list[i].ClassSchedule = await _classScheduleDomainService.FindQuery(x => x.Id == obj_list[i].ClassScheduleId).FirstOrDefaultAsync();
    //                        if (obj_list[i].ClassSchedule != null)
    //                        {
    //                            obj_list[i].ClassSchedule.ILA = await _ilaService.FindQuery(x => x.Id == obj_list[i].ClassSchedule.ILAID).FirstOrDefaultAsync();
    //                            obj_list[i].ClassSchedule.Instructor = await _instructorService.FindQuery(x => x.Id == obj_list[i].ClassSchedule.InstructorId).FirstOrDefaultAsync();
    //                            obj_list[i].ClassSchedule.Location = await _locationService.FindQuery(x => x.Id == obj_list[i].ClassSchedule.LocationId).FirstOrDefaultAsync();
    //                            obj_list[i].ClassSchedule.Provider = await _providerService.FindQuery(x => x.Id == obj_list[i].ClassSchedule.ProviderID).FirstOrDefaultAsync();

    //                            //var rosterStatus = _classScheduleRosterDomainService.FindQuery(x => x.TestId == obj.TestId && x.ClassScheduleId == obj.ClassSchedule.Id && x.EmpId == employee.Id && x.RetakeOrder == obj.RetakeOrder).FirstOrDefault().StatusId;
    //                            //string status = _classScheduleRosterStatusDomainService.GetAsync(rosterStatus ?? 0).Result.Name;
    //                            var rosterStatus = await _classScheduleRosterDomainService.FindQuery(x => x.TestId == obj_list[i].TestId && x.ClassScheduleId == obj_list[i].ClassSchedule.Id && x.EmpId == employee.Id && x.RetakeOrder == obj_list[i].RetakeOrder).FirstOrDefaultAsync();
    //                            var rosterStatusId = rosterStatus != null && rosterStatus.StatusId != null ? rosterStatus.StatusId : 0;
    //                            var status = await _classScheduleRosterStatusDomainService.FindQuery(x => x.Id == rosterStatusId).Select(s => s.Name).FirstOrDefaultAsync();

    //                            DateTime? dueDate = null;
    //                            var setting = await _testReleaseEMPSettingsService.FindQuery(x => x.ILAId == obj_list[i].ClassSchedule.ILAID).FirstOrDefaultAsync();
    //                            if (setting != null && obj_list[i].TestType != null)
    //                            {
    //                                if (obj_list[i].TestType.Description.ToUpper() == "PRETEST")
    //                                    dueDate = obj_list[i].ClassSchedule.StartDateTime;

    //                                else
    //                                    dueDate = obj_list[i].ClassSchedule.EndDateTime.AddDays(setting.FinalTestDueDate);

    //                                //var finalTestDays = _empTestSettingsIlaService.FindQuery(x => x.ILAId == obj.ClassSchedule.ILAID)?.FirstOrDefaultAsync().Result?.FinalTestDueDate;
    //                                //if (finalTestDays != null && finalTestDays > 0)
    //                                //{
    //                                //    dueDate = obj.ClassSchedule.EndDateTime.AddDays((double)finalTestDays);
    //                                //}
    //                                var classEndDate = obj_list[i].ClassSchedule.EndDateTime;
    //                                if (rosterStatus != null && rosterStatus.IsReleased == true)
    //                                {

    //                                    list.Add(new EmployeeTestModel()
    //                                    {
    //                                        DueDate = dueDate,
    //                                        Score = obj_list[i].Score,
    //                                        Grade = obj_list[i].Grade,
    //                                        TestId = obj_list[i].TestId,
    //                                        TestType = obj_list[i].TestType.Description,
    //                                        TestTypeId = obj_list[i].TestTypeId,
    //                                        EmpId = obj_list[i].EmpId,
    //                                        ILA = (obj_list[i].ClassSchedule?.ILA?.Number ?? "N/A") + " - " + obj_list[i].ClassSchedule?.ILA?.Name ?? "N/A",
    //                                        Instructpr = obj_list[i].ClassSchedule?.Instructor?.InstructorName ?? "N/A",
    //                                        Location = obj_list[i].ClassSchedule?.Location?.LocName ?? "N/A",
    //                                        ClassDate = obj_list[i].ClassSchedule.StartDateTime,
    //                                        ClassScheduleId = obj_list[i].ClassSchedule.Id,
    //                                        ILANumber = obj_list[i].ClassSchedule?.ILA?.Number ?? "N/A",
    //                                        TestHours = obj_list[i].Test.ILATraineeEvaluations.Where(x => x.TestId == obj_list[i].TestId).Select(x => x.TestTimeLimitHours).FirstOrDefault(),
    //                                        TestMinutes = obj_list[i].Test.ILATraineeEvaluations.Where(x => x.TestId == obj_list[i].TestId).Select(x => x.TestTimeLimitMinutes).FirstOrDefault(),
    //                                        Status = status,
    //                                        ProviderName = obj_list[i].ClassSchedule?.ILA?.Name ?? "N/A",
    //                                        CompletedDate = obj_list[i].CompletedDate,
    //                                        Instructions = obj_list[i].Test.ILATraineeEvaluations.Where(x => x.TestId == obj_list[i].TestId).Select(s => s.TestInstruction).FirstOrDefault(),
    //                                        RosterId = obj_list[i].Id,
    //                                        TestSubmittedAnswers = setting.ShowStudentSubmittedFinalTestAnswers,
    //                                        TestCorrectIncorrectAnswers = setting.ShowCorrectIncorrectFinalTestAnswers,
    //                                        PretestSubmittedAnswers = setting.ShowStudentSubmittedPreTestAnswers,
    //                                        PretestCorrectIncorrectAnswers = setting.ShowCorrectIncorrectPreTestAnswers,
    //                                        TestRetakeSubmittedAnswers = setting.ShowStudentSubmittedRetakeTestAnswers,
    //                                        TestRetakeCorrectIncorrectAnswers = setting.ShowCorrectIncorrectRetakeTestAnswers
    //                                    });
    //                                }
    //                            }
    //                        }
    //                    }                       
    //                }
    //            }
    //        }


    //        return list;

    //    }

    //    public async Task<ClassSchedule_Roster_Response_Selection> CreateAsync(EmpTestCreateOptions options)
    //    {

    //        try
    //        {
    //            var roster = await _classRosterService.FindQuery(x => x.Id == options.RosterId).FirstOrDefaultAsync();
    //            if (roster == null)
    //            {
    //                throw new BadHttpRequestException(message: _localizer["RosterNotFoundException"]);
    //            }
    //            var testItemType = _testItemTypeSercice.GetAsync(options.TestItemTypeId).Result.Description;
    //            var obj = (await _empTestService.FindAsync(x => x.RosterId == roster.Id && x.QuestionId == options.QuestionId)).FirstOrDefault();
    //            if (obj == null)
    //            {

    //                if (testItemType.Trim().ToLower() == "multiple correct answers" || testItemType.Trim().ToLower() == "match the column" || testItemType.Trim().ToLower() == "fill in the blank" || testItemType.Trim().ToLower() == "short answers")
    //                {
    //                    //For multiple correct questions, fill in the blanks and match the columns

    //                    //For multiple correct question
    //                    if (testItemType.Trim().ToLower() == "multiple correct answers")
    //                    {


    //                        await CreateMultipleCreateAnswers(options);
    //                    }

    //                    else if (testItemType.Trim().ToLower() == "fill in the blank")
    //                    {
    //                        //For fill in the blank question


    //                        await CreateFillInTheBlankWithAnswers(options);
    //                    }
    //                    else if (testItemType.Trim().ToLower() == "match the column")
    //                    {
    //                        //For match the column question

    //                        await CreateMatchTheColumnAnswers(options);
    //                    }
    //                    else if (testItemType.Trim().ToLower() == "short answers")
    //                    {
    //                        await CreateShortAnswerAsync(options);
    //                    }

    //                    else
    //                    {
    //                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
    //                    }

    //                }
    //                else
    //                {
    //                    obj = new ClassSchedule_Roster_Response_Selection(roster.Id, options.UserAnswer.FirstOrDefault(), options.TestItemTypeId, null, null, options.QuestionId);
    //                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmpTestOperations.Create);
    //                    if (result.Succeeded)
    //                    {
    //                        obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //                        obj.CreatedDate = DateTime.Now;

    //                        var validationResult = await _empTestService.AddAsync(obj);
    //                        if (!validationResult.IsValid)
    //                        {
    //                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
    //                        }
    //                    }
    //                    else
    //                    {
    //                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
    //                    }

    //                }

    //            }
    //            else
    //            {
    //                //Update case
    //                var existing = _empTestService.GetAsync(obj.Id).Result;

    //                testItemType = _testItemTypeSercice.GetAsync(existing.TestItemTypeId).Result.Description;
    //                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, existing, EmpTestOperations.Update);

    //                if (result.Succeeded)
    //                {

    //                    if (testItemType.Trim().ToLower() == "multiple correct answers" || testItemType.Trim().ToLower() == "match the column" || testItemType.Trim().ToLower() == "fill in the blank" || testItemType.Trim().ToLower() == "short answers")
    //                    {
    //                        //For multiple correct questions, fill in the blanks and match the columns

    //                        //For multiple correct question
    //                        if (testItemType.Trim().ToLower() == "multiple correct answers")
    //                        {
    //                            var questionAns = _empTestService.FindQuery(x => x.QuestionId == options.QuestionId && x.RosterId == roster.Id).ToList();

    //                            if (questionAns != null && questionAns.Count > 0)
    //                            {
    //                                foreach (var item in questionAns)
    //                                {
    //                                    await _empTestService.DeleteAsync(item);
    //                                }
    //                                await CreateMultipleCreateAnswers(options);
    //                            }
    //                            else
    //                            {
    //                                await CreateMultipleCreateAnswers(options);
    //                            }
    //                        }

    //                        else if (testItemType.Trim().ToLower() == "fill in the blank")
    //                        {
    //                            var questionAns = _empTestService.FindQuery(x => x.QuestionId == options.QuestionId && x.RosterId == roster.Id).ToList();

    //                            if (questionAns != null && questionAns.Count > 0)
    //                            {
    //                                foreach (var item in questionAns)
    //                                {
    //                                    await _empTestService.DeleteAsync(item);
    //                                }
    //                                await CreateFillInTheBlankWithAnswers(options);
    //                            }
    //                            else
    //                            {
    //                                await CreateFillInTheBlankWithAnswers(options);
    //                            }
    //                        }
    //                        else if (testItemType.Trim().ToLower() == "match the column")
    //                        {
    //                            var questionAns = _empTestService.FindQuery(x => x.QuestionId == options.QuestionId && x.RosterId == roster.Id).ToList();

    //                            if (questionAns != null && questionAns.Count > 0)
    //                            {
    //                                foreach (var item in questionAns)
    //                                {
    //                                    await _empTestService.DeleteAsync(item);
    //                                }
    //                                await CreateMatchTheColumnAnswers(options);
    //                            }
    //                            else
    //                            {
    //                                await CreateMatchTheColumnAnswers(options);
    //                            }
    //                        }
    //                        else if (testItemType.Trim().ToLower() == "short answers")
    //                        {
    //                            var questionAns = _empTestService.FindQuery(x => x.QuestionId == options.QuestionId && x.RosterId == roster.Id).ToList();

    //                            if (questionAns != null && questionAns.Count > 0)
    //                            {
    //                                foreach (var item in questionAns)
    //                                {
    //                                    await _empTestService.DeleteAsync(item);
    //                                }
    //                                await CreateShortAnswerAsync(options);
    //                            }
    //                            else
    //                            {
    //                                await CreateShortAnswerAsync(options);
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        existing.UserAnswer = options.UserAnswer.FirstOrDefault();
    //                        //existing.TestId = options.TestId;
    //                        existing.QuestionId = options.QuestionId;
    //                        //existing.TestTypeId = options.TestTypeId;
    //                        //existing.EmployeeId = options.EmployeeId;
    //                        //existing.ClassScheduleId = options.ClassScheduleId;
    //                        existing.RosterId = roster.Id;
    //                        existing.MatchValue = null;
    //                        existing.CorrectIndex = null;
    //                        obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //                        obj.ModifiedDate = DateTime.Now;
    //                        var validationResult = await _empTestService.UpdateAsync(obj);
    //                        if (!validationResult.IsValid)
    //                        {
    //                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
    //                        }

    //                    }

    //                }
    //            }

    //            return obj;
    //        }
    //        catch (Exception ex)
    //        {

    //            throw;
    //        }

    //    }
    //    public async Task<bool> CreateMultipleCreateAnswers(EmpTestCreateOptions options)
    //    {
    //        try
    //        {
    //            var roster = await _classRosterService.FindQuery(x => x.Id == options.RosterId).FirstOrDefaultAsync();
    //            if (roster == null)
    //            {
    //                throw new BadHttpRequestException(message: _localizer["RosterNotFoundException"]);
    //            }
    //            if (options.UserAnswer.Count == 0)
    //            {
    //                var obj = new ClassSchedule_Roster_Response_Selection(roster.Id, null, options.TestItemTypeId, null, null, options.QuestionId);
    //                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmpTestOperations.Create);
    //                if (result.Succeeded)
    //                {
    //                    obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //                    obj.CreatedDate = DateTime.Now;

    //                    var validationResult = await _empTestService.AddAsync(obj);
    //                    if (!validationResult.IsValid)
    //                    {
    //                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
    //                    }
    //                }
    //                else
    //                {
    //                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
    //                }
    //            }
    //            else
    //            {
    //                foreach (var ans in options.UserAnswer)
    //                {

    //                    var obj = new ClassSchedule_Roster_Response_Selection(roster.Id, ans, options.TestItemTypeId, null, null, options.QuestionId);
    //                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmpTestOperations.Create);
    //                    if (result.Succeeded)
    //                    {
    //                        obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //                        obj.CreatedDate = DateTime.Now;

    //                        var validationResult = await _empTestService.AddAsync(obj);
    //                        if (!validationResult.IsValid)
    //                        {
    //                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
    //                        }
    //                    }
    //                    else
    //                    {
    //                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
    //                    }


    //                }

    //            }

    //            return true;
    //        }
    //        catch (Exception ex)
    //        {

    //            throw;
    //        }

    //    }

    //    public async Task<bool> CreateFillInTheBlankWithAnswers(EmpTestCreateOptions options)
    //    {
    //        var roster = await _classRosterService.FindQuery(x => x.Id == options.RosterId).FirstOrDefaultAsync();
    //        if (roster == null)
    //        {
    //            throw new BadHttpRequestException(message: _localizer["RosterNotFoundException"]);
    //        }
    //        if (options.BlankIndexWithAnwer.Count == 0)
    //        {
    //            var obj = new ClassSchedule_Roster_Response_Selection(roster.Id, null, options.TestItemTypeId, null, null, options.QuestionId);
    //            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmpTestOperations.Create);
    //            if (result.Succeeded)
    //            {
    //                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //                obj.CreatedDate = DateTime.Now;

    //                var validationResult = await _empTestService.AddAsync(obj);
    //                if (!validationResult.IsValid)
    //                {
    //                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
    //                }
    //            }
    //            else
    //            {
    //                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
    //            }

    //        }
    //        else
    //        {
    //            foreach (var blank in options.BlankIndexWithAnwer)
    //            {

    //                var obj = new ClassSchedule_Roster_Response_Selection(roster.Id, blank.UserValue, options.TestItemTypeId, null, blank.CorrectIndex, options.QuestionId);
    //                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmpTestOperations.Create);
    //                if (result.Succeeded)
    //                {
    //                    obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //                    obj.CreatedDate = DateTime.Now;

    //                    var validationResult = await _empTestService.AddAsync(obj);
    //                    if (!validationResult.IsValid)
    //                    {
    //                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
    //                    }
    //                }
    //                else
    //                {
    //                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
    //                }
    //            }

    //        }

    //        return true;
    //    }

    //    public async Task<bool> CreateShortAnswerAsync(EmpTestCreateOptions options)
    //    {
    //        var roster = await _classRosterService.FindQuery(x => x.Id == options.RosterId).FirstOrDefaultAsync();
    //        if (roster == null)
    //        {
    //            throw new BadHttpRequestException(message: _localizer["RosterNotFoundException"]);
    //        }
    //        var testItemType = _testItemTypeSercice.GetAsync(options.TestItemTypeId).Result.Description;
    //        var obj = (await _empTestService.FindAsync(x => x.RosterId == roster.Id && x.QuestionId == options.QuestionId)).FirstOrDefault();
    //        obj = new ClassSchedule_Roster_Response_Selection(roster.Id, options.ShortAnswerDescription, options.TestItemTypeId, null, null, options.QuestionId);
    //        var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmpTestOperations.Create);
    //        if (result.Succeeded)
    //        {
    //            obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //            obj.CreatedDate = DateTime.Now;

    //            var validationResult = await _empTestService.AddAsync(obj);
    //            if (!validationResult.IsValid)
    //            {
    //                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
    //            }
    //            else
    //            {
    //                return true;
    //            }
    //        }
    //        else
    //        {
    //            throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
    //        }
    //    }

    //    public async Task<bool> CreateMatchTheColumnAnswers(EmpTestCreateOptions options)
    //    {
    //        var roster = await _classRosterService.FindQuery(x => x.Id == options.RosterId).FirstOrDefaultAsync();
    //        if (roster == null)
    //        {
    //            throw new BadHttpRequestException(message: _localizer["RosterNotFoundException"]);
    //        }
    //        if (options.MatchValueWithCorrectValue.Count == 0)
    //        {
    //            var obj = new ClassSchedule_Roster_Response_Selection(roster.Id, null, options.TestItemTypeId, null, null, options.QuestionId);
    //            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmpTestOperations.Create);
    //            if (result.Succeeded)
    //            {
    //                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //                obj.CreatedDate = DateTime.Now;

    //                var validationResult = await _empTestService.AddAsync(obj);
    //                if (!validationResult.IsValid)
    //                {
    //                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
    //                }
    //            }
    //            else
    //            {
    //                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
    //            }

    //        }
    //        else
    //        {
    //            foreach (var match in options.MatchValueWithCorrectValue)
    //            {

    //                var obj = new ClassSchedule_Roster_Response_Selection(roster.Id, match.UserValue, options.TestItemTypeId, match.MatchValue, null, options.QuestionId);
    //                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmpTestOperations.Create);
    //                if (result.Succeeded)
    //                {
    //                    obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //                    obj.CreatedDate = DateTime.Now;

    //                    var validationResult = await _empTestService.AddAsync(obj);
    //                    if (!validationResult.IsValid)
    //                    {
    //                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
    //                    }
    //                }
    //                else
    //                {
    //                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
    //                }


    //            }
    //        }
    //        return true;
    //    }

    //    public async Task<EmployeeAnswerModel> GetTestAnswersAsync(int classId, int testId, int questionId, int rosterId)
    //    {
    //        try
    //        {

    //            var model = new EmployeeAnswerModel();
    //            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

    //            var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

    //            if (person != null)
    //            {
    //                var employee = await _employeeService.FindQuery(x => x.PersonId == person.Id).FirstOrDefaultAsync();
    //                if (employee == null)
    //                {
    //                    throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
    //                }
    //                var roster = await _classRosterService.FindQuery(x => x.Id == rosterId).FirstOrDefaultAsync();
    //                if (roster == null)
    //                {
    //                    throw new BadHttpRequestException(message: _localizer["RosterNotFoundException"]);
    //                }
    //                var testItemTypeId = await _testItem_Service.FindQuery(x => x.Id == questionId).Select(s => s.TestItemTypeId).FirstOrDefaultAsync();
    //                if (testItemTypeId == 0)
    //                {
    //                    throw new BadHttpRequestException(_localizer["TestItemTypeNotFound"]);
    //                }
    //                var emptest = await _empTestService.FindQuery(x => x.RosterId == roster.Id && x.QuestionId == questionId).FirstOrDefaultAsync();
    //                if (emptest == null)
    //                {
    //                    return new EmployeeAnswerModel
    //                    {
    //                        TestId = testId,
    //                        QuestionId = questionId,
    //                        TestItemTypeId = testItemTypeId,
    //                        UserAnswer = null,
    //                        EmployeeId = roster.EmpId,
    //                    };
    //                }
    //                var testItemType = await _testItemTypeSercice.FindQuery(x => x.Id == testItemTypeId).Select(s => s.Description).FirstOrDefaultAsync();
    //                if (testItemType.Trim().ToLower() == "multiple correct answers" || testItemType.Trim().ToLower() == "match the column" || testItemType.Trim().ToLower() == "fill in the blank")
    //                {
    //                    //For multiple correct questions, fill in the blanks and match the columns

    //                    //For multiple correct question
    //                    if (testItemType.Trim().ToLower() == "multiple correct answers")
    //                    {
    //                        var multipleAnswers = new List<string>();
    //                        var list = await _empTestService.FindQuery(x => x.QuestionId == questionId && x.RosterId == roster.Id).ToListAsync();
    //                        if (list != null && list.Count > 0)
    //                        {
    //                            foreach (var item in list)
    //                            {
    //                                multipleAnswers.Add(item.UserAnswer);
    //                            }

    //                        }
    //                        model.TestId = roster.TestId;
    //                        model.QuestionId = emptest.QuestionId;
    //                        model.TestItemTypeId = emptest.TestItemTypeId;
    //                        model.UserAnswer = null;
    //                        model.EmployeeId = roster.EmpId;
    //                        model.TestItemTypeId = roster.TestTypeId;
    //                        model.MultipleCorrectAnswer = multipleAnswers;
    //                    }
    //                    else if (testItemType.Trim().ToLower() == "match the column")
    //                    {
    //                        var matchColumList = new List<MatchColumns>();
    //                        //var matchColumn = new List<Tuple<string, string>>();
    //                        var list = await _empTestService.FindQuery(x => x.QuestionId == questionId && x.RosterId == roster.Id).ToListAsync();
    //                        if (list != null && list.Count > 0)
    //                        {
    //                            foreach (var item in list)
    //                            {
    //                                matchColumList.Add(new MatchColumns()
    //                                {
    //                                    MatchValue = item.MatchValue,
    //                                    UserValue = item.UserAnswer

    //                                });
    //                                //matchColumn.Add(new Tuple<string, string>(item.MatchValue, item.UserAnswer));
    //                            }

    //                        }
    //                        model.TestId = roster.TestId;
    //                        model.QuestionId = emptest.QuestionId;
    //                        model.TestItemTypeId = emptest.TestItemTypeId;
    //                        model.EmployeeId = roster.EmpId;
    //                        model.TestItemTypeId = roster.TestTypeId;
    //                        model.MatchValueWithCorrectValue = matchColumList;

    //                    }
    //                    else if (testItemType.Trim().ToLower() == "fill in the blank")
    //                    {
    //                        var fillinTheblankList = new List<FillintheBlank>();
    //                        var list = await _empTestService.FindQuery(x => x.QuestionId == questionId && x.RosterId == roster.Id).ToListAsync();
    //                        if (list != null && list.Count > 0)
    //                        {
    //                            foreach (var item in list)
    //                            {
    //                                fillinTheblankList.Add(new FillintheBlank()
    //                                {
    //                                    CorrectIndex = item.CorrectIndex ?? 0,
    //                                    UserValue = item.UserAnswer

    //                                });
    //                                //matchColumn.Add(new Tuple<string, string>(item.MatchValue, item.UserAnswer));
    //                            }

    //                        }
    //                        model.TestId = roster.TestId;
    //                        model.QuestionId = emptest.QuestionId;
    //                        model.TestItemTypeId = emptest.TestItemTypeId;
    //                        model.EmployeeId = roster.EmpId;
    //                        model.TestItemTypeId = roster.TestTypeId;
    //                        model.BlankIndexWithAnwer = fillinTheblankList;

    //                    }
    //                }

    //                else
    //                {
    //                    model.TestId = roster.TestId;
    //                    model.QuestionId = questionId;
    //                    model.TestItemTypeId = emptest.TestItemTypeId;
    //                    model.UserAnswer = emptest.UserAnswer;
    //                    model.EmployeeId = roster.EmpId;
    //                    model.TestItemTypeId = roster.TestTypeId;
    //                }
    //            }

    //            return model;

    //        }
    //        catch (Exception)
    //        {

    //            throw;
    //        }
    //    }

    //    public async Task<List<ReviewTestModel>> ReviewtestAsync(int rosterId)
    //    {
    //        var reviewList = new List<ReviewTestModel>();
    //        var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

    //        var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

    //        if (person != null)
    //        {
    //            var employee = await _employeeService.FindQuery(x => x.PersonId == person.Id).FirstOrDefaultAsync();
    //            var roster = await _classRosterService.FindQuery(x => x.Id == rosterId).FirstOrDefaultAsync();
    //            if (roster == null)
    //            {
    //                throw new BadHttpRequestException(message: _localizer["RosterNotFoundException"]);
    //            }
    //            var testItemlinks = (await _testItemLinkService.FindQuery(x => x.TestId == roster.TestId).OrderBy(o => o.Sequence).ToListAsync());
    //            List<TestItem> testItems = new List<TestItem>();
    //            var allTestItems = testItemlinks.Select(x => x.TestItem);
    //            var testItemIds = testItemlinks.Select(s => s.TestItemId).ToList();
    //            var random = await _testService.FindQuery(x => x.Id == roster.TestId).Select(s => s.RandomizeDistractors).FirstOrDefaultAsync();
    //            foreach (var tId in testItemIds)
    //            {
    //                var testItem = await _testItem_Service.FindQueryWithIncludeAsync(x => x.Id == tId,
    //                                new string[] { nameof(_testItem.TaxonomyLevel),
    //                                           nameof(_testItem.TestItemType),
    //                                           nameof(_testItem.TestItemMCQs),
    //                                           nameof(_testItem.TestItemFillBlanks),
    //                                           nameof(_testItem.TestItemMatches),
    //                                           nameof(_testItem.TestItemShortAnswers),
    //                                           nameof(_testItem.TestItemTrueFalses) }, true).FirstOrDefaultAsync();

    //                testItem.TestItemMCQs = testItem.TestItemMCQs.OrderBy(x => x.Number).ToList();
    //                testItem.TestItemMatches = testItem.TestItemMatches.OrderBy(x => x.Number).ToList();

    //                testItem.TestItemShortAnswers = testItem.TestItemShortAnswers.OrderBy(x => x.Number).ToList();
    //                testItem.TestItemFillBlanks = testItem.TestItemFillBlanks.OrderBy(x => x.CorrectIndex).ToList();
    //                testItems.Add(testItem);



    //            }

    //            testItems = testItems.Where(t => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, t, TestItemOperations.Read).Result.Succeeded).ToList();
    //            if (testItemlinks.Any(a => a.Sequence == 0))
    //            {
    //                //testItems = testItems.OrderBy(o => Guid.NewGuid()).ToList();
    //                testItems = testItems.OrderBy(x => x.Id).ToList();
    //            }


    //            if (testItems != null && testItems.Count > 0)
    //            {
    //                foreach (var testItem in testItems)
    //                {
    //                    var trueflseItem = await _empTestService.FindQuery(x => x.RosterId == roster.Id && x.QuestionId == testItem.Id).FirstOrDefaultAsync();
    //                    //foreach (var tf in trueflseItem)
    //                    //{
    //                    if (trueflseItem != null)
    //                    {



    //                        var testItemtypeId = testItem.TestItemTypeId;
    //                        var testItemType = _testItemTypeSercice.FindQuery(x => x.Id == testItemtypeId).FirstOrDefault().Description;
    //                        if (testItemType.Trim().ToLower() == "multiple correct answers" || testItemType.Trim().ToLower() == "match the column" || testItemType.Trim().ToLower() == "fill in the blank")
    //                        {
    //                            //For multiple correct questions, fill in the blanks and match the columns

    //                            //For multiple correct question
    //                            if (testItemType.Trim().ToLower() == "multiple correct answers")
    //                            {
    //                                var multipleAnswers = new List<string>();
    //                                var list = await _empTestService.FindQuery(x => x.QuestionId == testItem.Id && x.RosterId == roster.Id).ToListAsync();
    //                                if (list != null && list.Count > 0)
    //                                {
    //                                    foreach (var item in list)
    //                                    {
    //                                        if (item.UserAnswer != null)
    //                                        {
    //                                            multipleAnswers.Add(item.UserAnswer);
    //                                        }

    //                                    }
    //                                    if (multipleAnswers.Count > 0)
    //                                    {

    //                                        reviewList.Add(new ReviewTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            Status = "Complete",
    //                                            MultipleCorrectAnswer = multipleAnswers
    //                                        });
    //                                    }
    //                                    else
    //                                    {
    //                                        reviewList.Add(new ReviewTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            Status = "Incomplete",
    //                                            MultipleCorrectAnswer = new List<string>()

    //                                        });
    //                                    }
    //                                }
    //                            }
    //                            else if (testItemType.Trim().ToLower() == "match the column")
    //                            {
    //                                var matchColumList = new List<MatchColumns>();
    //                                //var matchColumn = new List<Tuple<string, string>>();
    //                                var list = await _empTestService.FindQuery(x => x.QuestionId == testItem.Id && x.RosterId == roster.Id).ToListAsync();
    //                                if (list != null && list.Count > 0)
    //                                {
    //                                    foreach (var item in list)
    //                                    {
    //                                        matchColumList.Add(new MatchColumns()
    //                                        {
    //                                            MatchValue = item.MatchValue,
    //                                            UserValue = item.UserAnswer

    //                                        });
    //                                        //matchColumn.Add(new Tuple<string, string>(item.MatchValue, item.UserAnswer));
    //                                    }
    //                                    if (matchColumList.Count > 0)
    //                                    {
    //                                        reviewList.Add(new ReviewTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            Status = "Complete",
    //                                            MatchValueWithCorrectValue = matchColumList
    //                                        });
    //                                    }
    //                                    else
    //                                    {
    //                                        reviewList.Add(new ReviewTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            Status = "Incomplete",
    //                                            MatchValueWithCorrectValue = new List<MatchColumns>()

    //                                        });
    //                                    }
    //                                }


    //                            }
    //                            else if (testItemType.Trim().ToLower() == "fill in the blank")
    //                            {
    //                                var fillinTheblankList = new List<FillintheBlank>();
    //                                var list = await _empTestService.FindQuery(x => x.QuestionId == testItem.Id && x.RosterId == roster.Id).ToListAsync();
    //                                if (list != null && list.Count > 0)
    //                                {
    //                                    foreach (var item in list)
    //                                    {
    //                                        fillinTheblankList.Add(new FillintheBlank()
    //                                        {
    //                                            CorrectIndex = item.CorrectIndex ?? 0,
    //                                            UserValue = item.UserAnswer

    //                                        });
    //                                        //matchColumn.Add(new Tuple<string, string>(item.MatchValue, item.UserAnswer));
    //                                    }

    //                                    if (fillinTheblankList.Count > 0)
    //                                    {
    //                                        reviewList.Add(new ReviewTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            Status = "Complete",
    //                                            BlankIndexWithAnwer = fillinTheblankList
    //                                        });
    //                                    }
    //                                    else
    //                                    {
    //                                        reviewList.Add(new ReviewTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            Status = "Incomplete",
    //                                            BlankIndexWithAnwer = new List<FillintheBlank>()

    //                                        });
    //                                    }

    //                                }


    //                            }
    //                        }
    //                        else if (testItemType.Trim().ToLower() == "true / false")
    //                        {  //For short answers, true false and multiple choice questions

    //                            var userAnswer = trueflseItem.UserAnswer;
    //                            if (userAnswer == null)
    //                            {
    //                                reviewList.Add(new ReviewTestModel()
    //                                {
    //                                    testItem = testItem,
    //                                    Status = "Incomplete",
    //                                    UserAnswer = userAnswer

    //                                });

    //                            }
    //                            else
    //                            {
    //                                reviewList.Add(new ReviewTestModel()
    //                                {
    //                                    testItem = testItem,
    //                                    Status = "Complete",
    //                                    UserAnswer = userAnswer
    //                                });
    //                            }


    //                        }
    //                        else if (testItemType.Trim().ToLower() == "short answers")
    //                        {
    //                            var shAnswers = await _testItemShortAnswersService.FindQuery(x => x.TestItemId == testItem.Id && x.AcceptableResponses == 1).ToListAsync();
    //                            var shList = new List<ShortAnswers>();
    //                            var list = await _empTestService.FindQuery(x => x.QuestionId == testItem.Id && x.RosterId == roster.Id).ToListAsync();
    //                            foreach (var item in list)
    //                            {
    //                                var shortAnswer = new ShortAnswers();
    //                                shortAnswer.UserAnswer = item.UserAnswer;
    //                                foreach (var shAns in shAnswers)
    //                                {
    //                                    shortAnswer.CorrectAnswers.Add(shAns.Responses);
    //                                }

    //                                shList.Add(shortAnswer);
    //                            }
    //                            if (shList.Count > 0)
    //                            {
    //                                reviewList.Add(new ReviewTestModel()
    //                                {
    //                                    testItem = testItem,
    //                                    Status = "Complete",
    //                                    ShortAnswerWithCorrects = shList
    //                                });
    //                            }
    //                            else
    //                            {
    //                                reviewList.Add(new ReviewTestModel()
    //                                {
    //                                    testItem = testItem,
    //                                    Status = "Incomplete",
    //                                    ShortAnswerWithCorrects = new List<ShortAnswers>()
    //                                });
    //                            }
    //                        }
    //                        else if (testItemType.Trim().ToLower() == "multiple choice questions")
    //                        {
    //                            var userAnswer = trueflseItem.UserAnswer;
    //                            if (userAnswer == null)
    //                            {
    //                                reviewList.Add(new ReviewTestModel()
    //                                {
    //                                    testItem = testItem,
    //                                    Status = "Incomplete",
    //                                    UserAnswer = userAnswer

    //                                });
    //                            }
    //                            else
    //                            {
    //                                reviewList.Add(new ReviewTestModel()
    //                                {
    //                                    testItem = testItem,
    //                                    Status = "Complete",
    //                                    UserAnswer = userAnswer
    //                                });
    //                            }
    //                        }
    //                    }


    //                }
    //            }

    //        }
    //        return reviewList.ToList();
    //    }

    //    public async Task<List<SubmitTestModel>> SubmiTestAsync(int classId, int testId, int rosterId, DateTime completionDate)
    //    {
    //        var reviewList = new List<SubmitTestModel>();
    //        var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;

    //        var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

    //        if (person != null)
    //        {
    //            var employee = await _employeeService.FindQuery(x => x.PersonId == person.Id).FirstOrDefaultAsync();
    //            var roster = await _classRosterService.FindQuery(x => x.Id == rosterId).FirstOrDefaultAsync();
    //            if (roster == null)
    //            {
    //                throw new BadHttpRequestException(message: _localizer["RosterNotFoundException"]);
    //            }
    //            var testItemlinks = (await _testItemLinkService.FindQuery(x => x.TestId == testId).OrderBy(o => o.Sequence).ToListAsync());
    //            List<TestItem> testItems = new List<TestItem>();
    //            var allTestItems = testItemlinks.Select(x => x.TestItem);
    //            var testItemIds = testItemlinks.Select(s => s.TestItemId).ToList();
    //            var random = await _testService.FindQuery(x => x.Id == testId).Select(s => s.RandomizeDistractors).FirstOrDefaultAsync();
    //            foreach (var tId in testItemIds)
    //            {
    //                var testItem = await _testItem_Service.FindQueryWithIncludeAsync(x => x.Id == tId,
    //                                new string[] { nameof(_testItem.TaxonomyLevel),
    //                                           nameof(_testItem.TestItemType),
    //                                           nameof(_testItem.TestItemMCQs),
    //                                           nameof(_testItem.TestItemFillBlanks),
    //                                           nameof(_testItem.TestItemMatches),
    //                                           nameof(_testItem.TestItemShortAnswers),
    //                                           nameof(_testItem.TestItemTrueFalses) }, true).FirstOrDefaultAsync();

    //                testItem.TestItemMCQs = testItem.TestItemMCQs.OrderBy(x => x.Number).ToList();
    //                testItem.TestItemMatches = testItem.TestItemMatches.OrderBy(x => x.Number).ToList();

    //                testItem.TestItemShortAnswers = testItem.TestItemShortAnswers.OrderBy(x => x.Number).ToList();
    //                testItem.TestItemFillBlanks = testItem.TestItemFillBlanks.OrderBy(x => x.CorrectIndex).ToList();
    //                testItems.Add(testItem);



    //            }

    //            testItems = testItems.Where(t => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, t, TestItemOperations.Read).Result.Succeeded).ToList();
    //            if (testItemlinks.Any(a => a.Sequence == 0))
    //            {
    //                testItems = testItems.OrderBy(o => Guid.NewGuid()).ToList();
    //            }


    //            if (testItems != null && testItems.Count > 0)
    //            {
    //                foreach (var testItem in testItems)
    //                {
    //                    var trueflseItem = await _empTestService.FindQuery(x => x.QuestionId == testItem.Id && x.RosterId == roster.Id).FirstOrDefaultAsync();
    //                    //foreach (var tf in trueflseItem)
    //                    //{
    //                    if (trueflseItem != null)
    //                    {
    //                        var testItemType = _testItemTypeSercice.GetAsync(trueflseItem.TestItemTypeId).Result.Description;
    //                        if (testItemType.Trim().ToLower() == "multiple correct answers" || testItemType.Trim().ToLower() == "match the column" || testItemType.Trim().ToLower() == "fill in the blank")
    //                        {
    //                            //For multiple correct questions, fill in the blanks and match the columns

    //                            //For multiple correct question
    //                            if (testItemType.Trim().ToLower() == "multiple correct answers")
    //                            {
    //                                var multipleAnswers = new List<string>();
    //                                bool correctAnswer = false;
    //                                var testItemmcq = testItem.TestItemMCQs.Where(x => x.IsCorrect == true).Select(x => x.ChoiceDescription).OrderBy(r => r).ToList();
    //                                var list = await _empTestService.FindQuery(x => x.QuestionId == testItem.Id && x.RosterId == roster.Id).Select(x => x.UserAnswer).OrderBy(r => r).ToListAsync();
    //                                if (list != null && list.Count > 0)
    //                                {
    //                                    if (testItemmcq.SequenceEqual(list))
    //                                    {
    //                                        correctAnswer = true;
    //                                        reviewList.Add(new SubmitTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            CompletionStatus = "Complete",
    //                                            Correct = correctAnswer,
    //                                            MultipleCorrectAnswer = list,
    //                                            MultiCorrectAnswers = testItemmcq
    //                                        });
    //                                    }
    //                                    else
    //                                    {
    //                                        reviewList.Add(new SubmitTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            CompletionStatus = "In Complete",
    //                                            Correct = correctAnswer,
    //                                            MultipleCorrectAnswer = list,
    //                                            MultiCorrectAnswers = testItemmcq

    //                                        });

    //                                    }

    //                                }
    //                                else
    //                                {
    //                                    reviewList.Add(new SubmitTestModel()
    //                                    {
    //                                        testItem = testItem,
    //                                        CompletionStatus = "In Complete",
    //                                        Correct = correctAnswer,
    //                                        MultipleCorrectAnswer = new List<string>(),
    //                                        MultiCorrectAnswers = testItemmcq
    //                                    });

    //                                }
    //                            }
    //                            else if (testItemType.Trim().ToLower() == "match the column")
    //                            {
    //                                //IDictionary<string, string> d1 = new Dictionary<string, string>();
    //                                //IDictionary<string, string> d2 = new Dictionary<string, string>();
    //                                List<MatchingDictionariesVM> d1 = new List<MatchingDictionariesVM>();
    //                                List<MatchingDictionariesVM> d2 = new List<MatchingDictionariesVM>();
    //                                var matchColumList = new List<MatchColumns>();
    //                                //var matchColumn = new List<Tuple<string, string>>();
    //                                var list = await _empTestService.FindQuery(x => x.QuestionId == testItem.Id && x.RosterId == roster.Id).ToListAsync();
    //                                var testItemMatchCorrectColumn = testItem.TestItemMatches.Select(x => x.CorrectValue).ToList();
    //                                var testItemMatchColumn = testItem.TestItemMatches.ToList();
                          
    //                                foreach (var item in testItemMatchColumn)
    //                                {
    //                                    d2.Add(new MatchingDictionariesVM { Match = item.MatchValue.ToString().Replace("\0", ""), Correct = item.CorrectValue.ToString().Replace("\0", "") });
    //                                }
    //                                if (list != null && list.Count > 0)
    //                                {
    //                                    foreach (var item in list)
    //                                    {
    //                                        if (item.MatchValue == null && item.UserAnswer == null)
    //                                        {
    //                                            d1.Add(new MatchingDictionariesVM { Match = "", Correct = "" });
    //                                        }
    //                                        else
    //                                        {
    //                                            d1.Add(new MatchingDictionariesVM { Match = item.MatchValue.ToString(), Correct = item.UserAnswer });
    //                                        }

    //                                        matchColumList.Add(new MatchColumns()
    //                                        {
    //                                            MatchValue = item.MatchValue,
    //                                            UserValue = item.UserAnswer

    //                                        });
    //                                        //matchColumn.Add(new Tuple<string, string>(item.MatchValue, item.UserAnswer));
    //                                    }
    //                                    if (d1.Count == d2.Count)
    //                                    {
    //                                        var isAdded = false;
    //                                        foreach (var key in d1)
    //                                        {
    //                                            if (d2.Find((x) => x.Match == key.Match) != null)
    //                                            {
    //                                                var foundValue = d2.Find((x) => x.Match == key.Match).Correct;
    //                                                if (foundValue.Trim().ToLower() != key.Correct.Trim().ToLower() && !isAdded)
    //                                                {
    //                                                    isAdded = true;
    //                                                    reviewList.Add(new SubmitTestModel()
    //                                                    {
    //                                                        testItem = testItem,
    //                                                        CompletionStatus = "Complete",
    //                                                        Correct = false,
    //                                                        MatchValueWithCorrectValue = matchColumList,
    //                                                        MatchValueAnswer = testItemMatchCorrectColumn
    //                                                    });
    //                                                }
    //                                            }
    //                                        }
    //                                        if (!isAdded)
    //                                        {
    //                                            reviewList.Add(new SubmitTestModel()
    //                                            {
    //                                                testItem = testItem,
    //                                                CompletionStatus = "Complete",
    //                                                Correct = true,
    //                                                MatchValueWithCorrectValue = matchColumList,
    //                                                MatchValueAnswer = testItemMatchCorrectColumn
    //                                            });
    //                                        }

    //                                    }
    //                                    //else
    //                                    //{
    //                                    //    reviewList.Add(new SubmitTestModel()
    //                                    //    {
    //                                    //        testItem = testItem,
    //                                    //        Correct = false,
    //                                    //        CompletionStatus = "Incomplete",
    //                                    //        MatchValueWithCorrectValue = new List<MatchColumns>(),
    //                                    //        MatchValueAnswer = testItemMatchCorrectColumn

    //                                    //    });
    //                                    //}
    //                                }


    //                            }
    //                            else if (testItemType.Trim().ToLower() == "fill in the blank")
    //                            {
    //                                //IDictionary<int, string> d1 = new Dictionary<int, string>();
    //                                //IDictionary<int, string> d2 = new Dictionary<int, string>();
    //                                List<FIBMatchingVM> d1 = new List<FIBMatchingVM>();
    //                                List<FIBMatchingVM> d2 = new List<FIBMatchingVM>();
    //                                var fillinTheblankList = new List<FillintheBlank>();
    //                                var list = await _empTestService.FindQuery(x => x.QuestionId == testItem.Id && x.RosterId == roster.Id).ToListAsync();
    //                                var fillintheBlanks = testItem.TestItemFillBlanks.ToList();
    //                                string clearDescription = ReplaceCharactersWithNbsp(testItem.Description);
    //                                foreach (var item in fillintheBlanks)
    //                                {

    //                                    d2.Add(new FIBMatchingVM { Match = item.CorrectIndex, Correct = item.Correct });

    //                                }


    //                                if (list != null && list.Count > 0)
    //                                {
    //                                    foreach (var item in list)
    //                                    {
    //                                        d1.Add(new FIBMatchingVM { Match = (item.CorrectIndex ?? 0), Correct = item.UserAnswer });
    //                                        fillinTheblankList.Add(new FillintheBlank()
    //                                        {
    //                                            CorrectIndex = item.CorrectIndex ?? 0,
    //                                            UserValue = item.UserAnswer

    //                                        });
    //                                        //matchColumn.Add(new Tuple<string, string>(item.MatchValue, item.UserAnswer));
    //                                    }

    //                                    if (d1.Count == d2.Count)
    //                                    {
    //                                        //if (d1.Equals(d2))
    //                                        //{
    //                                        //    reviewList.Add(new SubmitTestModel()
    //                                        //    {
    //                                        //        testItem = testItem,
    //                                        //        CompletionStatus = "Complete",
    //                                        //        Correct = true,
    //                                        //        BlankIndexWithAnwer = fillinTheblankList
    //                                        //    });

    //                                        //}
    //                                        //else
    //                                        //{

    //                                        //    reviewList.Add(new SubmitTestModel()
    //                                        //    {
    //                                        //        testItem = testItem,
    //                                        //        CompletionStatus = "Complete",
    //                                        //        Correct = false,
    //                                        //        BlankIndexWithAnwer = fillinTheblankList
    //                                        //    });
    //                                        //}
    //                                        var isAdded = false;
    //                                        foreach (var data in d1)
    //                                        {
    //                                            if (d2.Find(x => x.Match == data.Match) != null)
    //                                            {
    //                                                var foundValue = d2.Find(x => x.Match == data.Match);
    //                                                if (foundValue.Correct.Trim().ToLower() != data.Correct.Trim().ToLower() && !isAdded)
    //                                                {
    //                                                    isAdded = true;
    //                                                    reviewList.Add(new SubmitTestModel()
    //                                                    {
    //                                                        testItem = testItem,
    //                                                        CompletionStatus = "Complete",
    //                                                        Correct = false,
    //                                                        BlankIndexWithAnwer = fillinTheblankList,
    //                                                        FillInTheBlankkAnswer = fillintheBlanks.Select(x => x.Correct).ToList(),
    //                                                        ClearDescription = clearDescription
    //                                                    });
    //                                                }
    //                                            }
    //                                        }
    //                                        if (!isAdded)
    //                                        {
    //                                            reviewList.Add(new SubmitTestModel()
    //                                            {
    //                                                testItem = testItem,
    //                                                CompletionStatus = "Complete",
    //                                                Correct = true,
    //                                                BlankIndexWithAnwer = fillinTheblankList,
    //                                                FillInTheBlankkAnswer = fillintheBlanks.Select(x => x.Correct).ToList(),
    //                                                ClearDescription = clearDescription
    //                                            });
    //                                           //_= fillinTheblankList[0].UserValue == null ? reviewList[0].Correct = false : reviewList[0].Correct;
    //                                        }

    //                                    }
    //                                    else
    //                                    {
    //                                        reviewList.Add(new SubmitTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            CompletionStatus = "Incomplete",
    //                                            Correct = false,
    //                                            BlankIndexWithAnwer = new List<FillintheBlank>(),
    //                                            FillInTheBlankkAnswer = fillintheBlanks.Select(x => x.Correct).ToList(),
    //                                            ClearDescription = clearDescription

    //                                        });
    //                                    }

    //                                }

    //                            }
    //                        }
    //                        else
    //                        {  //For short answers, true false and multiple choice questions
    //                            if (testItemType.Trim().ToLower() == "short answers")
    //                            {
    //                                bool correctAnswer = false;
    //                                var testItemSA = testItem.TestItemShortAnswers.Select(x => new TestItemShortAnswer { Id = x.Id, Responses = x.Responses, IsCaseSensitive = x.IsCaseSensitive }).ToList();
    //                                if (trueflseItem.UserAnswer != null && trueflseItem.UserAnswer != "")
    //                                {

    //                                    //if (testItemSA.Contains(trueflseItem.UserAnswer))
    //                                    //{
    //                                    //    correctAnswer = true;
    //                                    //}
    //                                    foreach (var sq in testItemSA)
    //                                    {
    //                                        if (sq.IsCaseSensitive)
    //                                        {
    //                                            if (sq.Responses.Equals(trueflseItem.UserAnswer))
    //                                            {
    //                                                correctAnswer = true;
    //                                            }
    //                                        }
    //                                        else if (sq.Responses.Trim().ToLower().Equals(trueflseItem.UserAnswer.Trim().ToLower()))
    //                                        {
    //                                            correctAnswer = true;
    //                                        }
    //                                    }
    //                                    reviewList.Add(new SubmitTestModel()
    //                                    {
    //                                        testItem = testItem,
    //                                        Correct = correctAnswer,
    //                                        CompletionStatus = "Complete",
    //                                        UserAnswer = trueflseItem.UserAnswer,
    //                                        ShortAnswer = testItemSA.Select(x => x.Responses).ToList()


    //                                    });
    //                                }
    //                                else
    //                                {
    //                                    reviewList.Add(new SubmitTestModel()
    //                                    {
    //                                        testItem = testItem,
    //                                        Correct = correctAnswer,
    //                                        CompletionStatus = "In Complete",
    //                                        UserAnswer = null,
    //                                        ShortAnswer = testItemSA.Select(x => x.Responses).ToList()

    //                                    });

    //                                }



    //                            }
    //                            else if (testItemType.Trim().ToLower() == "true / false")
    //                            {
    //                                bool correctAnswer = false;
    //                                var testItemTf = testItem.TestItemTrueFalses.Where(x => x.IsCorrect == true).Select(x => x.Choices).FirstOrDefault();
    //                                if (testItemTf != null && testItemTf.Count() > 0)
    //                                {



    //                                    if (trueflseItem.UserAnswer != null)
    //                                    {
    //                                        if (trueflseItem.UserAnswer.ToLower() == testItemTf.ToLower())
    //                                        {
    //                                            correctAnswer = true;
    //                                        }
    //                                        reviewList.Add(new SubmitTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            Correct = correctAnswer,
    //                                            CompletionStatus = "Complete",
    //                                            UserAnswer = trueflseItem.UserAnswer,
    //                                            TrueFalseAnswer = testItemTf

    //                                        });
    //                                    }
    //                                    else
    //                                    {
    //                                        reviewList.Add(new SubmitTestModel()
    //                                        {
    //                                            testItem = testItem,
    //                                            Correct = correctAnswer,
    //                                            CompletionStatus = "In Complete",
    //                                            UserAnswer = null,
    //                                            TrueFalseAnswer = testItemTf

    //                                        });

    //                                    }
    //                                }



    //                            }
    //                            else if (testItemType.Trim().ToLower() == "multiple choice questions")
    //                            {
    //                                bool correctAnswer = false;
    //                                var testItemmcq = testItem.TestItemMCQs.Where(x => x.IsCorrect == true).Select(x => x.ChoiceDescription).FirstOrDefault();
    //                                if (trueflseItem.UserAnswer != null)
    //                                {
    //                                    if (trueflseItem.UserAnswer.ToLower() == testItemmcq.ToLower())
    //                                    {
    //                                        correctAnswer = true;
    //                                    }
    //                                    reviewList.Add(new SubmitTestModel()
    //                                    {
    //                                        testItem = testItem,
    //                                        Correct = correctAnswer,
    //                                        CompletionStatus = "Complete",
    //                                        UserAnswer = trueflseItem.UserAnswer,
    //                                        MCQAnswer = testItemmcq

    //                                    });
    //                                }
    //                                else
    //                                {
    //                                    reviewList.Add(new SubmitTestModel()
    //                                    {
    //                                        testItem = testItem,
    //                                        Correct = correctAnswer,
    //                                        CompletionStatus = "In Complete",
    //                                        UserAnswer = null,
    //                                        MCQAnswer = testItemmcq

    //                                    });

    //                                }



    //                            }


    //                        }

    //                    }


    //                }
    //            }
    //            ClassRoasterUpdateOptions options = new ClassRoasterUpdateOptions();
    //            var ilaId = _iLATraineeEvaluationDomainService.FindQuery(x => x.TestId == testId)?.Select(x => x.ILAId).FirstOrDefault();
    //            var testtypeId = _iLATraineeEvaluationDomainService.FindQuery(x => x.TestId == testId).FirstOrDefault().TestTypeId;
    //            var testType = _testtypeService.GetAsync(testtypeId ?? 0).Result.Description;
    //            var testpassingPercentage = "50%";
    //            options.RetakeOrder = null;
    //            bool showSubmittedAnswers = false;
    //            bool showCorrectIncorrectAnswers = false;
    //            var settings = await _testReleaseEMPSettingsService.FindQuery(x => x.ILAId == ilaId).FirstOrDefaultAsync();
    //            if (testType.ToLower() == "pretest")
    //            {
    //                testpassingPercentage = _testReleaseEMPSettingsService.FindQuery(x => x.ILAId == ilaId)?.Select(x => x.PreTestScore)?.FirstOrDefault().ToString();
    //                options.TestType = "Pretest";

    //                if (settings != null)
    //                {
    //                    showSubmittedAnswers = settings.ShowStudentSubmittedPreTestAnswers;
    //                    showCorrectIncorrectAnswers = settings.ShowCorrectIncorrectPreTestAnswers;
    //                }

    //            }
    //            else if (testType.ToLower() == "final test")
    //            {
    //                testpassingPercentage = _testReleaseEMPSettingsService.FindQuery(x => x.ILAId == ilaId)?.Select(x => x.FinalTestPassingScore)?.FirstOrDefault();
    //                options.TestType = "Test";
    //                if (settings != null)
    //                {
    //                    showSubmittedAnswers = settings.ShowStudentSubmittedFinalTestAnswers;
    //                    showCorrectIncorrectAnswers = settings.ShowCorrectIncorrectFinalTestAnswers;
    //                }
    //            }
    //            else if (testType.ToLower() == "retake")
    //            {
    //                testpassingPercentage = _testReleaseEMPSettingsService.FindQuery(x => x.ILAId == ilaId)?.Select(x => x.FinalTestPassingScore)?.FirstOrDefault();
    //                options.TestType = "Retake";
    //                if (settings != null)
    //                {
    //                    showSubmittedAnswers = settings.ShowStudentSubmittedRetakeTestAnswers;
    //                    showCorrectIncorrectAnswers = settings.ShowCorrectIncorrectRetakeTestAnswers;
    //                    options.RetakeOrder = await _classRosterService.FindQuery(x => x.Id == rosterId).Select(s => s.RetakeOrder).FirstOrDefaultAsync();
    //                }
    //            }
    //            else if (testType.ToLower() == "cbt")
    //            {
    //                options.TestType = "CBT";
    //            }
    //            var totalquestions = testItems.Count;
    //            var correctAnswersCount = reviewList.Where(x => x.Correct == true).Count();
    //            var wrongAnswersCount = totalquestions - correctAnswersCount;
    //            var score = correctAnswersCount / (double)totalquestions;
    //            var percentage = score * 100;

    //            var unAttemptedAnswers = reviewList.Where(x => x.UserAnswer == null && x.BlankIndexWithAnwer == null && x.MultipleCorrectAnswer == null && x.MatchValueWithCorrectValue == null).Count();
    //            //var unattemptedFillInTheBlanks = reviewList.Where(x => x.BlankIndexWithAnwer == null);
    //            //var unattemptedmultipleChoiceQuestions = reviewList.Where(x => x.MultipleCorrectAnswer == null);
    //            //var unattemptedmultipleChoiceQuestions = reviewList.Where(x => x.MatchValueWithCorrectValue == null);
    //            var studentPassFailStatus = "Fail";
    //            int requiredPercentage = 0;


    //            if (testpassingPercentage != null)
    //            {
    //                testpassingPercentage = testpassingPercentage.Replace("%", "");

    //            }
    //            else
    //            {
    //                testpassingPercentage = "50";
    //            }
    //            requiredPercentage = int.Parse(testpassingPercentage);
    //            if (percentage >= requiredPercentage)
    //            {
    //                studentPassFailStatus = "Pass";
    //                options.Grade = "P";
    //            }
    //            else
    //            {
    //                options.Grade = "F";
    //                //auto release test if grade is F
    //                // await _classRosterappService.ReleaseTestAsync(employee.Id, options);
    //            }
    //            var classSchedule = await _classScheduleDomainService.FindWithIncludeAsync(x => x.Id == classId, new string[] { "ILA", "Provider" });
    //            reviewList[^1].ProviderName = classSchedule.Select(x => x?.Provider?.Name ?? "N/A").FirstOrDefault();
    //            reviewList[^1].StartDate = classSchedule.Select(x => x.StartDateTime).FirstOrDefault();
    //            reviewList[^1].EndDate = classSchedule.Select(x => x.EndDateTime).FirstOrDefault();
    //            reviewList[^1].ILANumber = classSchedule.Select(x => x?.ILA?.Number ?? "N/A").FirstOrDefault();
    //            reviewList[^1].MaximumScore = 100;
    //            reviewList[^1].PassingScore = requiredPercentage;
    //            reviewList[^1].TotalScore = (int)percentage;
    //            reviewList[^1].PassFailStatus = studentPassFailStatus;
    //            reviewList[^1].showSubmittedAnswers = showSubmittedAnswers;
    //            reviewList[^1].showCorrectIncorrectAnswers = showCorrectIncorrectAnswers;

    //            options.TestId = testId;
    //            options.ClassId = classId;
    //            options.Score = (int)percentage;

    //            if (testType.Trim().ToLower() == "final test")
    //            {
    //                var schedule = await _classSchedule_empService.FindQuery(x => x.EmployeeId == employee.Id && x.ClassScheduleId == classId).FirstOrDefaultAsync();
    //                if (schedule != null)
    //                {
    //                    schedule.CompleteClass(completionDate, options.Grade, options.Score);
    //                    if (schedule.FinalGrade == "F")
    //                    {
    //                        await AutoReleaseTestEnable(ilaId, classId, testId, employee.Id, options);
    //                    }
    //                    var finalCompletedStatusId = await _classScheduleRosterStatusDomainService.FindQuery(x => x.Name.Trim().ToLower() == "completed").Select(s => s.Id).FirstOrDefaultAsync();
    //                    if (finalCompletedStatusId != 0)
    //                    {
    //                        schedule.TestStatusId = finalCompletedStatusId;
    //                        await _classSchedule_empService.UpdateAsync(schedule);
    //                    }
    //                }
    //            }
    //            else if (testType.Trim().ToLower() == "pretest")
    //            {
    //                var schedule = await _classSchedule_empService.FindQuery(x => x.EmployeeId == employee.Id && x.ClassScheduleId == classId).FirstOrDefaultAsync();
    //                if (schedule != null)
    //                {
    //                    var completedStatusId = await _classScheduleRosterStatusDomainService.FindQuery(x => x.Name.Trim().ToLower() == "completed").Select(s => s.Id).FirstOrDefaultAsync();
    //                    if (completedStatusId != 0)
    //                    {
    //                        schedule.PreTestStatusId = completedStatusId;
    //                        await _classSchedule_empService.UpdateAsync(schedule);
    //                    }
    //                }
    //            }
    //            else if (testType.Trim().ToLower() == "retake")
    //            {
    //                var schedule = await _classSchedule_empService.FindQuery(x => x.EmployeeId == employee.Id && x.ClassScheduleId == classId).FirstOrDefaultAsync();
    //                if (schedule != null)
    //                {
    //                    schedule.CompleteClass(completionDate, options.Grade, options.Score);
    //                    var completedStatusId = await _classScheduleRosterStatusDomainService.FindQuery(x => x.Name.Trim().ToLower() == "completed").Select(s => s.Id).FirstOrDefaultAsync();
    //                    if (completedStatusId != 0)
    //                    {
    //                        schedule.PreTestStatusId = completedStatusId;
    //                        await _classSchedule_empService.UpdateAsync(schedule);
    //                    }


    //                }
    //            }




    //            var res = await _classRosterappService.UpdateScoreAsync(employee.Id, options);
    //            //if(res.Grade == null)
    //            //{

    //            var test = await _classRosterappService.UpdateGradeAsync(employee.Id, options);

    //            //}
    //            var completeStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "Completed").FirstOrDefault().Id;
    //            var incompleteStatusId = _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "In Progress").FirstOrDefault().Id;
    //            //Get Roster 
    //            var empTest = _classScheduleRosterDomainService.FindQuery(x => x.TestId == testId && x.ClassScheduleId == classId && x.EmpId == employee.Id && x.RetakeOrder == options.RetakeOrder).FirstOrDefault();
    //            if (empTest != null && completeStatusId != 0)
    //            {

    //                empTest.StatusId = completeStatusId;

    //                empTest.CompleteTest(completionDate);
    //                empTest.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //                empTest.ModifiedDate = DateTime.Now;
    //                await _classScheduleRosterDomainService.UpdateAsync(empTest);

    //                if (testType.Trim().ToLower() == "retake")
    //                {
    //                    if (options.Grade == "F")
    //                    {
    //                        await AutoReleaseTestEnable(ilaId, classId, testId, employee.Id, options);

    //                    }
    //                }
    //            }

    //            for (int i = 0; i < reviewList.Count; i++)
    //            {
    //                reviewList[i].showCorrectIncorrectAnswers = showCorrectIncorrectAnswers;
    //                reviewList[i].showSubmittedAnswers = showSubmittedAnswers;
    //            }

    //        }
    //        return reviewList.Distinct().ToList();
    //    }

    //    public async System.Threading.Tasks.Task AutoReleaseTestEnable(int? ilaId, int classId, int testId, int employeeId, ClassRoasterUpdateOptions options)
    //    {
    //        //   var settings = await _testReleaseEMPSettingsService.FindQuery(x => x.ILAId == ilaId).FirstOrDefaultAsync();
    //        //if autorelease bit true


    //        var settings2 = await _testReleaseEMPSettingsService.FindQueryWithIncludeAsync(x => x.ILAId == ilaId, new string[] { "TestReleaseEMPSetting_Retake_Links" }).FirstOrDefaultAsync();
    //        if (settings2.AutoReleaseRetake == true)
    //        {
    //            for (int i = 0; i < settings2.TestReleaseEMPSetting_Retake_Links.Count; i++)
    //            {
    //                var retakeList = await _classRosterService.FindQuery(x => x.TestId == settings2.TestReleaseEMPSetting_Retake_Links.ToList()[i].RetakeTestId && x.ClassScheduleId == classId && x.EmpId == employeeId && x.RetakeOrder == i + 1).FirstOrDefaultAsync();
    //                // if no retake order then release test
    //                if (retakeList == null)
    //                {
    //                    var retakeOption = new ClassRoasterUpdateOptions();
    //                    retakeOption.ClassId = classId;
    //                    retakeOption.TestId = settings2.TestReleaseEMPSetting_Retake_Links.ToList()[i].RetakeTestId;
    //                    retakeOption.TestType = "Retake";
    //                    retakeOption.RetakeOrder = i + 1;

    //                    await _classRosterappService.ReleaseTestAsync(employeeId, retakeOption);
    //                    break;
    //                }
    //                else
    //                {

    //                    //main if condition if the retake is released
    //                    if (retakeList.IsReleased == true)
    //                    {
    //                        //if completed date is not null and grade is F the auto release
    //                        //if (retakeList.CompletedDate != null && retakeList.Grade == "F")
    //                        //{
    //                        //    await _classRosterappService.ReleaseTestAsync(employeeId, options);
    //                        //}
    //                        //if completed date is null and grade is also null then auto release
    //                        if (retakeList.CompletedDate == null && retakeList.Grade == null)
    //                        {
    //                            break;
    //                            //await _classRosterappService.ReleaseTestAsync(employeeId, options);
    //                        }
    //                    }
    //                    else if (retakeList.IsReleased == false)
    //                    {
    //                        var retakeOption = new ClassRoasterUpdateOptions();
    //                        retakeOption.ClassId = classId;
    //                        retakeOption.TestId = settings2.TestReleaseEMPSetting_Retake_Links.ToList()[i].RetakeTestId;
    //                        retakeOption.TestType = "Retake";
    //                        retakeOption.RetakeOrder = i + 1;

    //                        await _classRosterappService.ReleaseTestAsync(employeeId, retakeOption);
    //                        break;
    //                    }
    //                    //skip isReleased == false

    //                }
    //            }



    //            //if there are retake orders

    //        }
    //    }

    //    public async Task<bool> ExitTestAsync(int rosterId)
    //    {
    //        var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Email;
    //        var person = await _personService.FindQuery(x => x.Username == userName).FirstOrDefaultAsync();

    //        if (person != null)
    //        {
    //            var employee = await _employeeService.FindQuery(x => x.PersonId == person.Id).FirstOrDefaultAsync();
    //            var incompleteStatusId = await _classScheduleRosterStatusDomainService.FindQuery(x => x.Name == "In Progress").Select(s => s.Id).FirstOrDefaultAsync();
    //            //Get Roster 
    //            var empTest = _classScheduleRosterDomainService.FindQuery(x => x.Id == rosterId).FirstOrDefault();
    //            if (empTest != null)
    //            {
    //                empTest.StatusId = incompleteStatusId;
    //                empTest.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
    //                empTest.ModifiedDate = DateTime.Now;
    //                await _classScheduleRosterDomainService.UpdateAsync(empTest);
    //            }
    //        }
    //        return true;
    //    }

    //    static string ReplaceCharactersWithNbsp(string input)
    //    {
    //        // Replace each character with &nbsp; inside <u> tags
    //        string pattern = "<u>(.*?)</u>";
    //        string replacedString = Regex.Replace(input, pattern, match =>
    //        {
    //            string contentInsideUTags = match.Groups[1].Value;
    //            string nbsp = string.Join(" ", contentInsideUTags.Select(c => "&nbsp;"));
    //            return "<u>" + nbsp + "</u>";
    //        });

    //        return replacedString;
    //    }
    //}
}

