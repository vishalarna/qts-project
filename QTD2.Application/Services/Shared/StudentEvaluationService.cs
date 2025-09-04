using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.QuestionBank;
using QTD2.Infrastructure.Model.StudentEvaluation;
using QTD2.Infrastructure.Model.StudentEvaluation_Question_Link;
using IStudentEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluationService;
using IStudenEvaluation_QuestionDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluation_QuestionService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using IClassSchedule_StudentEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_StudentEvaluations_LinkService;
using IcsEvaluationRosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Evaluation_RosterService;
using IcsEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IRatingScaleExpandedDomainService = QTD2.Domain.Interfaces.Service.Core.IRatingScaleExpandedService;
using IStudentEvaluationWithoutEmpDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluationWithoutEmpService;
using Microsoft.EntityFrameworkCore;
using QTD2.Infrastructure.Model.StudentEvaluationHistory;
using QTD2.Infrastructure.Model.ClassSchedule_StudentEvaluation_Link;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.StudentEvaluationForm;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using ILocationDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_Service;
using IInstructorDomainService = QTD2.Domain.Interfaces.Service.Core.IInstructor_Service;
using IILAEvaluationEMPSettingDomainService = QTD2.Domain.Interfaces.Service.Core.IEvaluationReleaseEMPSettingsService;
using IProviderDomainService = QTD2.Domain.Interfaces.Service.Core.IProviderService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using IRatingScaleNDomainService = QTD2.Domain.Interfaces.Service.Core.IRatingScaleNService;
using IClassScheduleEvaluationRoasterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Evaluation_RosterService;
using IILA_StudentEvaluation_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_StudentEvaluation_LinkService;
using QTD2.Infrastructure.Model.EMPStudentEvaluationVM;

namespace QTD2.Application.Services.Shared
{
    public class StudentEvaluationService : IStudentEvaluationService
    {
        private readonly IStudentEvaluationDomainService _studentEvaluationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<StudentEvaluationService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly StudentEvaluation _studentEvaluation;
        private readonly IQuestionBankService _questionBankService;
        private readonly IStudenEvaluation_QuestionDomainService _studentEvaluationQuestionService;
        private readonly StudentEvaluation_Question _studentEval_Question;
        private readonly ClassSchedule_StudentEvaluations_Link _class_StudentEval;
        private readonly IClassScheduleDomainService _classScheduleService;
        private readonly IClassSchedule_StudentEvaluationDomainService _classStudentEvaluationDomainService;
        private readonly IILA_StudentEvaluation_LinkDomainService _student_evaluation_link_Service;

        private readonly IcsEvaluationRosterDomainService _classScheduleEvaluationRosterService;
        private readonly IcsEmployeeDomainService _classScheduleEmployeeService;
        private readonly IEmployeeDomainService _employeeService;
        private readonly IStudentEvaluationWithoutEmpDomainService _studentEvaluationWithoutEmpDomainService;
        private readonly IILADomainService _ilaService;
        private readonly ILocationDomainService _locationService;
        private readonly IInstructorDomainService _instService;
        private readonly IILAEvaluationEMPSettingDomainService _evalReleaseService;
        private readonly IProviderDomainService _provService;
        private readonly IPersonDomainService _personService;
        private readonly IRatingScaleNDomainService _rating_scaleNService;
        private readonly IClassScheduleEvaluationRoasterDomainService _classScheduleEvaluationRoasterDomainService;
        private readonly IEMPReleaseCheckService _empReleaseCheckService;
        private readonly IRatingScaleExpandedDomainService _rating_scale_expandedService;

        public StudentEvaluationService(
            IStudentEvaluationWithoutEmpDomainService studentEvaluationWithoutEmpDomainService,
            IcsEvaluationRosterDomainService classScheduleEvaluationRosterService,
            IcsEmployeeDomainService classScheduleDomainService,
            IEmployeeDomainService employeeService,
            IStudentEvaluationDomainService studentEvaluationService,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<StudentEvaluationService> localizer,
            UserManager<AppUser> userManager, IQuestionBankService questionBankService,
            IStudenEvaluation_QuestionDomainService studentEvaluationQuestionService,
            IClassScheduleDomainService classScheduleService,
            IClassSchedule_StudentEvaluationDomainService classStudentEvaluationDomainService,
            IRatingScaleExpandedDomainService rating_scale_expandedService,
            IILADomainService ilaService,
            ILocationDomainService locationService,
            IInstructorDomainService instService,
            IILAEvaluationEMPSettingDomainService evalReleaseService,
            IPersonDomainService personService,
            IProviderDomainService provService,
            IILA_StudentEvaluation_LinkDomainService student_evaluation_link_Service,
            IRatingScaleNDomainService rating_scaleNService,
            IClassScheduleEvaluationRoasterDomainService classScheduleEvaluationRoasterDomainService, IEMPReleaseCheckService empReleaseCheckService)
        {
            _studentEvaluationService = studentEvaluationService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _userManager = userManager;
            _studentEvaluation = new StudentEvaluation();
            _questionBankService = questionBankService;
            _studentEvaluationQuestionService = studentEvaluationQuestionService;
            _classScheduleService = classScheduleService;
            _classStudentEvaluationDomainService = classStudentEvaluationDomainService;
            _classScheduleEvaluationRosterService = classScheduleEvaluationRosterService;
            _classScheduleEmployeeService = classScheduleDomainService;
            _employeeService = employeeService;
            _class_StudentEval = new ClassSchedule_StudentEvaluations_Link();
            _rating_scale_expandedService = rating_scale_expandedService;
            _studentEvaluationWithoutEmpDomainService = studentEvaluationWithoutEmpDomainService;
            _ilaService = ilaService;
            _locationService = locationService;
            _instService = instService;
            _evalReleaseService = evalReleaseService;
            _personService = personService;
            _provService = provService;
            _rating_scaleNService = rating_scaleNService;
            _classScheduleEvaluationRoasterDomainService = classScheduleEvaluationRoasterDomainService;
            _empReleaseCheckService = empReleaseCheckService;
            _student_evaluation_link_Service = student_evaluation_link_Service;
        }

        public async System.Threading.Tasks.Task ActivateAsync(int id, StudentEvaluationHistoryCreateOptions options)
        {
            //if (options != null && options.StudentEvaluationIds.Count() > 0)
            //{
            //    foreach (var studentEval in options.StudentEvaluationIds)
            //    {
            var studentEvaluation = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, StudentEvaluationOperations.Update);
            if (result.Succeeded)
            {
                studentEvaluation.Activate();
                var validationResult = await _studentEvaluationService.UpdateAsync(studentEvaluation);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
            //}
            //}
            //else
            //{
            //    throw new Exception("Student Evaluation Ids not found");
            //}
        }

        public async Task<StudentEvaluation> CreateAync(StudentEvaluationCreateOptions options)
        {
            var studentEvaluation = new StudentEvaluation();
            if (options.Mode == "Copy")
            {

                var existingEval = await _studentEvaluationService.GetWithIncludeAsync(options.stdEvalId ?? 0, new string[] { nameof(_studentEvaluation.StudentEvaluationQuestions)});
                studentEvaluation = new StudentEvaluation(options.RatingScaleId, options.Title, options.Instructions, null, existingEval.IsAvailableForAllILAs, existingEval.IsIncludeCommentSections, existingEval.IsAllowNAOption, existingEval.IsAvailableForSelectedILAs);
                studentEvaluation.RatingScaleN = await _rating_scaleNService.GetAsync(options.RatingScaleId);
            }
            else
            {
                studentEvaluation = new StudentEvaluation(options.RatingScaleId, options.Title, options.Instructions, true, options.IsAvailableForAllILAs, options.IsIncludeCommentSections, options.IsAllowNAOption, options.IsAvailableForSelectedILAs);
                studentEvaluation.RatingScaleN = await _rating_scaleNService.GetAsync(options.RatingScaleId);
            }

            var studentEvaluationExists = (await _studentEvaluationService.FindAsync(r => r.Title == options.Title)).FirstOrDefault() != null;
            if (studentEvaluationExists)
            {
                throw new BadHttpRequestException(message: _localizer["studentEvaluationExists"]);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, StudentEvaluationOperations.Create);

            if (result.Succeeded)
            {
                studentEvaluation.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                studentEvaluation.CreatedDate = DateTime.Now;
                var validationResult = await _studentEvaluationService.AddAsync(studentEvaluation);

                if (validationResult.IsValid)
                {
                    if (options.Mode == "Copy")
                    {
                        studentEvaluation = (await _studentEvaluationService.FindAsync(r => r.Title == options.Title)).FirstOrDefault();
                        var existingEval = await _studentEvaluationService.GetWithIncludeAsync(options.stdEvalId ?? 0, new string[] { nameof(_studentEvaluation.StudentEvaluationQuestions) });
                        var listQuesIds = new List<int>();
                        var linkQues = new StudentEvaluation_Question_LinkCreateOptions();
                        if (existingEval.StudentEvaluationQuestions != null && existingEval.StudentEvaluationQuestions.Count() > 0)
                        {
                            foreach (var quesId in existingEval.StudentEvaluationQuestions)
                            {
                                listQuesIds.Add(quesId.QuestionBankId);
                            }
                            linkQues.QuestionIds = listQuesIds.ToArray();
                            await LinkQuestions(studentEvaluation.Id, linkQues);
                        }
                    }

                    return studentEvaluation;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeactivateAsync(int id, StudentEvaluationHistoryCreateOptions options)
        {

            //if (options != null && options.StudentEvaluationIds.Count() > 0)
            //{
            //    foreach (var studentEval in options.StudentEvaluationIds)
            //    {
            var studentEvaluation = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, StudentEvaluationOperations.Update);
            if (result.Succeeded)
            {
                studentEvaluation.Deactivate();
                var validationResult = await _studentEvaluationService.UpdateAsync(studentEvaluation);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }

            //    }

            //}
            //else
            //{
            //    throw new Exception("Student Evaluation Ids not found");
            //}

        }

        public async System.Threading.Tasks.Task DeleteAsync(int id, StudentEvaluationHistoryCreateOptions options)
        {
            //if (options != null && options.StudentEvaluationIds.Count() > 0)
            //{
            //    foreach (var studentEval in options.StudentEvaluationIds)
            //    {
            var studentEvaluation = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, StudentEvaluationOperations.Delete);
            if (result.Succeeded)
            {
                studentEvaluation.Delete();
                var validationResult = await _studentEvaluationService.UpdateAsync(studentEvaluation);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
            //    }

            //}
            //else
            //{
            //    throw new Exception("Student Evaluation Ids not found");
            //}
        }

        public async Task<List<StudentEvaluationVM>> GetAsync()
        {
            var studentEvallist = new List<StudentEvaluationVM>();
            var studentEvaluation = await _studentEvaluationService.AllWithIncludeAsync(new string[] { "ClassSchedule_Evaluation_Rosters" });
            //  var studentEvaluation = await _studentEvaluationService.AllAsync();
            studentEvaluation = studentEvaluation.Where(p => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, p, StudentEvaluationOperations.Read).Result.Succeeded);
            if (studentEvaluation.Count() > 0)
            {
                foreach (var stdeval in studentEvaluation)
                {
                    var isReleased = await _classScheduleEvaluationRoasterDomainService.FindAsync(x => x.StudentEvaluationId == stdeval.Id);


                    studentEvallist.Add(new StudentEvaluationVM()
                    {
                        Id = stdeval.Id,
                        StudentEvaluationId = "QTD_0" + stdeval.Id,
                        Title = stdeval.Title,
                        IsPublished = stdeval.IsPublished ?? false,
                        Active = stdeval.Active,
                        questionsNum = await _studentEvaluationQuestionService.GetCount(x => x.StudentEvaluationId == stdeval.Id),
                        ClassRoaster = stdeval.ClassSchedule_Evaluation_Rosters.Count(),
                        ClassRoasterIsReleased = isReleased.Count() > 0 ? true : false
                    });
                }
            }

            return studentEvallist.ToList();
        }

        public async Task<List<StudentEvaluationVM>> GetPublishedEvalsAsync()
        {
            var studentEvallist = new List<StudentEvaluationVM>();
            var evals = await _studentEvaluationService.FindQuery(x => x.IsPublished == true, true).ToListAsync();
            evals = evals.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, StudentEvaluationOperations.Read).Result.Succeeded).ToList();
            if (evals.Count() > 0)
            {
                foreach (var stdeval in evals)
                {
                    studentEvallist.Add(new StudentEvaluationVM()
                    {
                        Id = stdeval.Id,
                        StudentEvaluationId = "QTD_0" + stdeval.Id,
                        Title = stdeval.Title,
                        IsPublished = stdeval.IsPublished,
                        Active = stdeval.Active,
                        questionsNum = await _studentEvaluationQuestionService.GetCount(x => x.StudentEvaluationId == stdeval.Id)
                    });
                }
            }

            return studentEvallist.ToList();
        }

        public async Task<List<EmpStudentEvaluation_VM>> GetEvaluationsAsync()
        {
            var person = await _personService.FindQuery(x => x.Username == _httpContextAccessor.HttpContext.User.Identity.Name).FirstOrDefaultAsync();
            if (person == null)
            {
                throw new BadHttpRequestException(message: _localizer["Person Information Not Found"]);
            }
            var employee = await _employeeService.FindQuery(x => x.PersonId == person.Id).FirstOrDefaultAsync();
            if (employee == null)
            {
                throw new BadHttpRequestException(message: _localizer["Employee Information Not Found"]);
            }
            employee.Person = person;
            var csEvaluationRosters = (await _classScheduleEvaluationRosterService.GetEvaluationsByEmpIdAsync(employee.Id)).ToList();

            var toReturnList = new List<EmpStudentEvaluation_VM>();
            var index = 0;
            foreach (var emp in csEvaluationRosters)
            {
                var csInfo = await _classScheduleService.FindQuery(x => x.Id == emp.ClassScheduleId).FirstOrDefaultAsync();
                if (csInfo != null)
                {
                    var ila = await _ilaService.FindQuery(x => x.Id == csInfo.ILAID).FirstOrDefaultAsync();
                    if (ila != null)
                    {
                        var stdEval = await _studentEvaluationService.FindQuery(x => x.Id == emp.StudentEvaluationId).FirstOrDefaultAsync();
                        if (stdEval != null)
                        {
                            var provider = await _provService.FindQuery(x => x.Id == ila.ProviderId).FirstOrDefaultAsync();
                            if (provider != null)
                            {
                                var location = await _locationService.FindQuery(x => x.Id == csInfo.LocationId).FirstOrDefaultAsync();
                                var instructor = await _instService.FindQuery(x => x.Id == csInfo.InstructorId).FirstOrDefaultAsync();
                                var evalSettings = await _evalReleaseService.FindQuery(x => x.ILAId == ila.Id).FirstOrDefaultAsync();
                                toReturnList.Add(new EmpStudentEvaluation_VM
                                {
                                    number = ++index,
                                    ilaTitle = ila.Number + " " + ila.Name,
                                    dueDate = evalSettings != null ? evalSettings.GetDueDate(csInfo.EndDateTime) : csInfo.EndDateTime.AddDays(10),
                                    isCompleted = emp.IsCompleted,
                                    status = emp.getStatus(),
                                    classSchedule_Evaluation_RosterId = emp.Id,
                                    isStarted = emp.IsStarted,
                                    isAllowed = emp.IsAllowed,
                                    providerName = provider.Name,
                                    ilaTitleOnly = ila.Name,
                                    instructorName = instructor?.InstructorName,
                                    ilaNumber = ila.Number,
                                    locationName = location?.LocName,
                                    completionDate = emp.CompletedDate,
                                    classScheduleId = emp.ClassScheduleId,
                                    evaluationId = emp.StudentEvaluationId,
                                    employeeId = employee.Id,
                                    evaluationTitle = stdEval.Title,
                                    classStartDate = csInfo.StartDateTime,
                                    classEndDate = csInfo.EndDateTime
                                });
                            }
                        }
                    }
                }

            }
            return toReturnList;
        }

        public async Task<object> CompleteEvaluationAsync(ClassSchedule_Evaluation_RosterOptions options)
        {
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new ClassSchedule_Evaluation_Roster(), ClassSchedule_Evaluation_RosterOperations.Read);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }

            var classScheduleEvaluationRosterItem = (await _classScheduleEvaluationRosterService.FindAsync(x => x.EmployeeId == options.employeeId && x.ClassScheduleId == options.classId && x.StudentEvaluationId == options.evaluationId))?.FirstOrDefault();
            if (classScheduleEvaluationRosterItem == null)
            {
                classScheduleEvaluationRosterItem = new ClassSchedule_Evaluation_Roster
                {
                    EmployeeId = options.employeeId,
                    ClassScheduleId = options.classId,
                    StudentEvaluationId = options.evaluationId,
                    IsAllowed = false,
                    CompletedDate = DateTime.Now,
                };
                var validationResult = await _classScheduleEvaluationRosterService.AddAsync(classScheduleEvaluationRosterItem);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                classScheduleEvaluationRosterItem.CompletedDate = DateTime.Now;
                classScheduleEvaluationRosterItem.IsAllowed = false;
                var validationResult = await _classScheduleEvaluationRosterService.UpdateAsync(classScheduleEvaluationRosterItem);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }

            return new { message = "Evaluation Completed Successfully" };
        }

        public async Task<object> StartEvaluationAsync(int evaluationId)
        {
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new ClassSchedule_Evaluation_Roster(), ClassSchedule_Evaluation_RosterOperations.Read);
            if (result.Succeeded)
            {
                var classScheduleEvaluationRosterItem = await _classScheduleEvaluationRosterService.GetAsync(evaluationId);
                classScheduleEvaluationRosterItem.IsStarted = true;
                classScheduleEvaluationRosterItem.IsAllowed = false;
                var validationResult = await _classScheduleEvaluationRosterService.UpdateAsync(classScheduleEvaluationRosterItem);

                if (validationResult.IsValid)
                {
                    return new { message = "evaluation started successfully!" };
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }

        }
        public async Task<StudentEvaluation> GetAsync(int id)
        {
            var studentEvaluation = await _studentEvaluationService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { nameof(_studentEvaluation.RatingScaleN) }).FirstOrDefaultAsync();
            if (studentEvaluation != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, StudentEvaluationOperations.Read);
                if (result.Succeeded)
                {
                    return studentEvaluation;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return studentEvaluation;
        }

        public async Task<StudentEvaluation> GetWithRatingScale(int id)
        {
            var studentEvaluation = await _studentEvaluationService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (studentEvaluation != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, StudentEvaluationOperations.Read);
                if (result.Succeeded)
                {
                    studentEvaluation.RatingScaleN = await _rating_scaleNService.FindQuery(x => x.Id == studentEvaluation.RatingScaleId).FirstOrDefaultAsync();
                    if (studentEvaluation.RatingScaleN != null)
                    {
                        studentEvaluation.RatingScaleN.RatingScaleExpanded = await _rating_scale_expandedService.FindQuery(x => x.RatingScaleNId == studentEvaluation.RatingScaleN.Id).ToListAsync();
                    }
                    return studentEvaluation;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return studentEvaluation;
        }

        public async Task<StudentEvaluation> UpdateAsync(int id, StudentEvaluationCreateOptions option)
        {
            var studentEvaluation = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, StudentEvaluationOperations.Update);
            if (result.Succeeded)
            {
                if (option.Mode == "Edit")
                {
                    studentEvaluation.Title = option.Title;
                    studentEvaluation.Instructions = option.Instructions;
                    studentEvaluation.RatingScaleId = option.RatingScaleId;
                    if (option.AnotherMode == "editPublish")
                    {
                        studentEvaluation.IsPublished = true;
                    }
                    studentEvaluation.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    studentEvaluation.ModifiedDate = DateTime.Now;
                }

                else
                {
                    studentEvaluation.Title = option.Title;
                    studentEvaluation.Instructions = option.Instructions;
                    studentEvaluation.RatingScaleId = option.RatingScaleId;
                    studentEvaluation.IsAvailableForAllILAs = option.IsAvailableForAllILAs;
                    studentEvaluation.IsAvailableForSelectedILAs = option.IsAvailableForSelectedILAs;
                    studentEvaluation.IsAllowNAOption = option.IsAllowNAOption;
                    studentEvaluation.IsIncludeCommentSections = option.IsIncludeCommentSections;
                    studentEvaluation.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    studentEvaluation.ModifiedDate = DateTime.Now;

                }

                var validationResult = await _studentEvaluationService.UpdateAsync(studentEvaluation);

                if (validationResult.IsValid)
                {
                    return studentEvaluation;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }
        public async Task<StudentEvaluation> LinkQuestions(int studentEvaluationId, StudentEvaluation_Question_LinkCreateOptions options)
        {
            var studentEvaluation = await _studentEvaluationService.GetWithIncludeAsync(studentEvaluationId, new string[] { nameof(_studentEvaluation.StudentEvaluationQuestions) });
            if (options.isReordered && options.QuestionIds.Count() > 0)
            {
                //Unlink all
                foreach (var id in options.QuestionIds)
                {
                    var question = await _questionBankService.GetAsync(id);
                    studentEvaluation.UnlinkQuestion(question);
                    await _studentEvaluationService.UpdateAsync(studentEvaluation);
                }
                //Link all
                foreach (var id in options.QuestionIds)
                {
                    var question = await _questionBankService.GetAsync(id);
                    studentEvaluation.LinkQuestion(question);
                    var validationResult = await _studentEvaluationService.UpdateAsync(studentEvaluation);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }

            else
            {
                foreach (var id in options.QuestionIds)
                {
                    var question = await _questionBankService.GetAsync(id);

                    var studentEvaluationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, StudentEvaluationOperations.Update);
                    var questionBankResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, QuestionBankOperations.Read);
                    if (studentEvaluationResult.Succeeded && questionBankResult.Succeeded)
                    {
                        studentEvaluation.LinkQuestion(question);
                        var validationResult = await _studentEvaluationService.UpdateAsync(studentEvaluation);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
            }

            return studentEvaluation;
        }

        public async Task<StudentEvaluation> UnLinkQuestions(int studentEvaluationId, StudentEvaluation_Question_LinkCreateOptions options)
        {
            var studentEvaluation = await _studentEvaluationService.GetWithIncludeAsync(studentEvaluationId, new string[] { nameof(_studentEvaluation.StudentEvaluationQuestions) });
            if (options.QuestionIds.Count() > 0)
            {
                //Unlink all
                foreach (var id in options.QuestionIds)
                {
                    var question = await _questionBankService.GetAsync(id);
                    studentEvaluation.UnlinkQuestion(question);
                    await _studentEvaluationService.UpdateAsync(studentEvaluation);
                }
            }
            return studentEvaluation;
        }





        public async Task<List<QuestionsWithCountOptions>> GetLinkedQuestions(int id)
        {
            var links = await _studentEvaluationQuestionService.FindWithIncludeAsync(x => x.StudentEvaluationId == id, new string[] { nameof(_studentEval_Question.QuestionBank) });
            List<Domain.Entities.Core.QuestionBank> questionList = new List<Domain.Entities.Core.QuestionBank>();
            questionList.AddRange(links.Select(x => x.QuestionBank));

            List<QuestionsWithCountOptions> questiuonsWithCount = new List<QuestionsWithCountOptions>();
            foreach (var ques in questionList)
            {

                questiuonsWithCount.Add(new QuestionsWithCountOptions(ques.Stem, ques.Id, "QTS_0" + ques.Id, ques.Active));
            }

            return questiuonsWithCount;
        }
        public async Task<StudentEvaluationStatsVM> GetStatsCount()
        {

            var stats = new StudentEvaluationStatsVM()
            {
                StudentEvaluationsPublished = await _studentEvaluationService.GetCount(x => x.IsPublished == true),
                StudentEvaluationsInActive = await _studentEvaluationService.GetCount(x => x.Active == false),
                StudentEvaluationsInDevelopment = await _studentEvaluationService.GetCount(x => x.IsPublished == false || x.IsPublished == null),

            };

            return stats;
        }


        public async Task<List<StudentEvaluation>> GetList(string option)
        {
            var rrList = new List<StudentEvaluation>();

            switch (option.ToLower().Trim())
            {
                case "active":
                    rrList = await _studentEvaluationService.FindQuery(x => x.Active == false).ToListAsync();
                    break;
                case "publish":
                    rrList = await _studentEvaluationService.FindQuery(x => x.IsPublished == true).ToListAsync();
                    break;

                case "development":
                    rrList = await _studentEvaluationService.FindQuery(x => x.IsPublished == false || x.IsPublished == null).ToListAsync();
                    break;
            }

            return rrList;
        }






        public async Task<StudentEvaluation> PublishEvaluation(int id)
        {

            var studentEvaluation = await GetAsync(id);
            studentEvaluation.IsPublished = true;
            studentEvaluation.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            studentEvaluation.ModifiedDate = DateTime.Now;
            var validationResult = await _studentEvaluationService.UpdateAsync(studentEvaluation);

            if (validationResult.IsValid)
            {
                return studentEvaluation;
            }
            else
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }

        public async Task<StudentEvaluation> LinkClass(int evalId, ClassSchedule_StudentEvaluation_LinkCreateOptions options)
        {
            switch (options.selection)
            {
                case "first":
                default:
                    var firstClass = (await _classScheduleService.FindAsync(x => x.ILAID == options.ILAId)).First().Id;
                    options.classScheduleIds.Add(firstClass);
                    break;
                case "multiple":
                    options.classScheduleIds = options.classScheduleIds;
                    break;
                case "all":
                    var allClass = (await _classScheduleService.FindAsync(x => x.ILAID == options.ILAId)).Select(x => x.Id).ToList<int>();
                    options.classScheduleIds = allClass;
                    break;
            }
            var studentEvaluation = await _studentEvaluationService.GetWithIncludeAsync(evalId, new string[] { nameof(_studentEvaluation.ClassSchedule_StudentEvaluations_Links) });
            foreach (var id in options.classScheduleIds)
            {
                var classSchedule = await _classScheduleService.GetAsync(id);

                var classScheduleResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classSchedule, ClassScheduleOperations.Update);
                var studentEvaluationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, StudentEvaluationOperations.Read);
                if (classScheduleResult.Succeeded && studentEvaluationResult.Succeeded)
                {
                    studentEvaluation.LinkClassSchedule(classSchedule);
                    var validationResult = await _studentEvaluationService.UpdateAsync(studentEvaluation);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return studentEvaluation;
        }

        public async Task<StudentEvaluation> UpdateLinkClassData(ClassSchedule_StudentEvaluation_LinkUpdateOptions options)
        {
            var evaluation = await _student_evaluation_link_Service.GetAsync(options.evalId);
            evaluation.studentEvalAudienceID = options.audienceId;
            await _student_evaluation_link_Service.UpdateAsync(evaluation);
            var studentEvaluation = await _studentEvaluationService.GetWithIncludeAsync(options.evalId, new string[] { nameof(_studentEvaluation.ClassSchedule_StudentEvaluations_Links) });

            return studentEvaluation;
        }

        public async Task<List<ClassScheduleWithCountOptions>> GetLinkedClassesToEvaluation(int id)
        {
            var links = await _classStudentEvaluationDomainService.FindWithIncludeAsync(x => x.StudentEvaluationId == id, new string[] { nameof(_class_StudentEval.ClassSchedule), nameof(_class_StudentEval.StudentEvaluation) });
            //List<Domain.Entities.Core.ClassSchedule> scheduleList = new List<Domain.Entities.Core.ClassSchedule>();
            //scheduleList.AddRange(links.Select(x => x.ClassSchedule));

            List<ClassScheduleWithCountOptions> schedulesWithCount = new List<ClassScheduleWithCountOptions>();
            foreach (var schedule in links)
            {

                schedulesWithCount.Add(new ClassScheduleWithCountOptions(schedule.ClassSchedule.Id, schedule.ClassSchedule.StartDateTime, schedule.StudentEvaluation.Title));
            }

            return schedulesWithCount;
        }
        public async Task<object> SaveQuestion(StudentEvaluation_SaveQuestion options)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new StudentEvaluationWithoutEmp(), StudentEvaluationWithoutEmpOperations.Create);
            if (!authorizationResult.Succeeded)
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
            var studentEvaluationwithoutEmp = (await _studentEvaluationWithoutEmpDomainService.FindAsync(x => x.ClassScheduleId == options.ClassId && x.StudentEvaluationId == options.EvaluationId && x.EmployeeId == options.EmployeeId && x.QuestionId == options.QuestionId))?.FirstOrDefault();

            var studentEvaluation = await _studentEvaluationService.GetWithRatingScalesAsync(options.EvaluationId);

            if (studentEvaluationwithoutEmp == null)
            {
                studentEvaluationwithoutEmp = new StudentEvaluationWithoutEmp
                {
                    StudentEvaluationId = options.EvaluationId,
                    ClassScheduleId = options.ClassId,
                    QuestionId = options.QuestionId,
                    EmployeeId = options.EmployeeId,
                    RatingScale = studentEvaluation.RatingScaleN.RatingScaleExpanded.Where(r => r.Ratings == options.Rating).FirstOrDefault()?.Id,
                    Notes = options.Notes
                };
                var validationResult = await _studentEvaluationWithoutEmpDomainService.AddAsync(studentEvaluationwithoutEmp);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                studentEvaluationwithoutEmp.RatingScale = studentEvaluation.RatingScaleN.RatingScaleExpanded.Where(r => r.Ratings == options.Rating).FirstOrDefault()?.Id;
                studentEvaluationwithoutEmp.Notes = options.Notes;
                var validationResult = await _studentEvaluationWithoutEmpDomainService.UpdateAsync(studentEvaluationwithoutEmp);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

            }
            return new { message = "Question Saved Successfully" };
        }

        public async Task<List<StudentEvaluationWithoutEmp>> GetSavedQuestionsDataAsync(int evalId, int classId, int empId)
        {
            var evalData = await _studentEvaluationWithoutEmpDomainService.FindQueryWithIncludeAsync((x => x.StudentEvaluationId == evalId && x.ClassScheduleId == classId && x.EmployeeId == empId), new string[] { "RatingScaleExpanded" }, true).ToListAsync();
            evalData = evalData.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, StudentEvaluationWithoutEmpOperations.Read).Result.Succeeded).ToList();
            return evalData;
        }

        public async Task<object> GetEvaluationsByIdAsync(int employeeId)
        {
            if (employeeId != null)
            {
                // await _empReleaseCheckService.CheckEvaluationRelease();
                var csEvaluationRosters = await _classScheduleEvaluationRosterService.GetEvaluationsByIdAsync(employeeId);
                var toReturnList = new List<object>();
                var index = 0;

                foreach (var emp in csEvaluationRosters)
                {
                    var csInfo = await _classScheduleService.GetClassScheduleByIdAsync(emp.ClassScheduleId.Value);
                    if (csInfo != null)
                    {
                        var ila = await _ilaService.GetLocationByIdAsync(csInfo.ILAID);
                        if (ila != null)
                        {
                            var location = await _locationService.GetLocationByIdAsync(csInfo.LocationId);
                            if (location != null)
                            {
                                var instructor = await _instService.GetInstructorByIdAsync(csInfo.InstructorId);
                                if (instructor != null)
                                {
                                    var stdEval = await _studentEvaluationService.GetStudentEvaluationByIdAsync(emp.StudentEvaluationId);
                                    if (stdEval != null)
                                    {
                                        var provider = await _provService.GetProviderByIdAsync(ila.ProviderId);
                                        if (provider != null)
                                        {
                                            var evalSettings = await _evalReleaseService.GetEvaluationSettingsByIdAsync(ila.Id);
                                            //TOdo refactor
                                            toReturnList.Add(new
                                            {
                                                number = ++index,
                                                ilaTitle = ila.Number + " " + ila.Name,
                                                dueDate = evalSettings != null ? evalSettings.GetDueDate(csInfo.EndDateTime) : csInfo.EndDateTime.AddDays(10),
                                                isCompleted = emp.IsCompleted,
                                                status = emp.getStatus(),
                                                classSchedule_Evaluation_RosterId = emp.Id,
                                                isStarted = emp.IsStarted,
                                                isAllowed = emp.IsAllowed,
                                                providerName = provider.Name,
                                                ilaTitleOnly = ila.Name,
                                                instructorName = instructor.InstructorName,
                                                ilaNumber = ila.Number,
                                                locationName = location.LocName,
                                                completionDate = emp.CompletedDate,
                                                classScheduleId = emp.ClassScheduleId,
                                                evaluationId = emp.StudentEvaluationId,
                                                employeeId = employeeId,
                                                evaluationTitle = stdEval.Title,
                                                classStartDate = csInfo.StartDateTime,
                                                classEndDate = csInfo.EndDateTime
                                            });
                                        }
                                        else
                                        {
                                            throw new BadHttpRequestException(message: _localizer["ProviderNotFoundException"]);
                                        }
                                    }
                                    {
                                        throw new BadHttpRequestException(message: _localizer["StudentEvaluationNotFoundException"]);
                                    }
                                }
                                else
                                {
                                    throw new BadHttpRequestException(message: _localizer["InstructorNotFoundException"]);
                                }
                            }
                            else
                            {
                                throw new BadHttpRequestException(message: _localizer["LocationNotFoundException"]);
                            }
                        }
                        else
                        {
                            throw new BadHttpRequestException(message: _localizer["ILANotFoundException"]);
                        }
                    }
                    else
                    {
                        throw new BadHttpRequestException(message: _localizer["ClassScheduleNotFoundException"]);
                    }
                }
                return toReturnList;
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
        }


        public async Task<object> StartEvaluationAsyncByIdAsync(int evaluationId, int employeeId)
        {
            if (employeeId != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new ClassSchedule_Evaluation_Roster(), ClassSchedule_Evaluation_RosterOperations.Read);
                if (result.Succeeded)
                {
                    var classScheduleEvaluationRosterItem = await _classScheduleEvaluationRosterService.GetAsync(evaluationId);
                    classScheduleEvaluationRosterItem.SetIsStarted(true);
                    classScheduleEvaluationRosterItem.SetIsAllowed(false);
                    var validationResult = await _classScheduleEvaluationRosterService.UpdateAsync(classScheduleEvaluationRosterItem);

                    if (validationResult.IsValid)
                    {
                        return new { message = "evaluation started successfully!" };
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
        }
        public async Task<object> CompleteEvaluationAsyncByIdAsync(ClassSchedule_Evaluation_RosterOptions options, int employeeId)
        {
            if (employeeId != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, new ClassSchedule_Evaluation_Roster(), ClassSchedule_Evaluation_RosterOperations.Read);
                if (!result.Succeeded)
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
                var classScheduleEvaluationRosterItem = await _classScheduleEvaluationRosterService.GetClassScheduleRosterByIdAsync(employeeId, options.classId, options.evaluationId);
                if (classScheduleEvaluationRosterItem == null)
                {
                    classScheduleEvaluationRosterItem = new ClassSchedule_Evaluation_Roster(options.classId, options.employeeId, options.evaluationId, false);
                    classScheduleEvaluationRosterItem.SetCompletedDate(DateTime.Now);
                    var validationResult = await _classScheduleEvaluationRosterService.AddAsync(classScheduleEvaluationRosterItem);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    classScheduleEvaluationRosterItem.SetCompletedDate(DateTime.Now);
                    classScheduleEvaluationRosterItem.SetIsAllowed(false);
                    var validationResult = await _classScheduleEvaluationRosterService.UpdateAsync(classScheduleEvaluationRosterItem);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }

                return new { message = "Evaluation Completed Successfully" };
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
        }
    }
}
