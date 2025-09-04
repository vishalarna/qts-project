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
using IStudentEvalutionWithoutEmpDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluationWithoutEmpService;
using Microsoft.EntityFrameworkCore;
using QTD2.Infrastructure.Model.StudentEvaluationWithoutEmp;
using IClassSchedule_Eval_RosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Evaluation_RosterService;
using QTD2.Infrastructure.Model.StudentEvaluationWithEMP;
using IClassSchedule_EMP_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using IClassSchedule_Eval_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_StudentEvaluations_LinkService;
using IStudentEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluationService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using IILAStudentEvalLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_StudentEvaluation_LinkService;
using IStudentEvalAudienceDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluationAudienceService;
using QTD2.Infrastructure.Model.StudentEvaluation;
using Microsoft.Extensions.Options;

namespace QTD2.Application.Services.Shared
{
    public class StudentEvaluationWithoutEmpService : IStudentEvaluationWithoutEmpService
    {
        private readonly IStudentEvalutionWithoutEmpDomainService _studentEvalutionWithoutEmpService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<StudentEvaluationService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IClassSchedule_Eval_RosterDomainService _cs_eval_rosterService;
        private readonly IClassSchedule_EMP_LinkDomainService _classScheduleEmployeeLinkService;
        private readonly IClassSchedule_Eval_LinkDomainService _classSchedule_Eval_LinkService;
        private readonly IStudentEvaluationDomainService _evalService;
        private readonly IClassScheduleDomainService _classScheduleService;
        private readonly IILAStudentEvalLinkDomainService _ila_evalLinkService;
        private readonly IStudentEvalAudienceDomainService _evalAudienceService;
        private readonly StudentEvaluation _studentEvaluation;

        public StudentEvaluationWithoutEmpService(IStudentEvalutionWithoutEmpDomainService studentEvalutionWithoutEmpService,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<StudentEvaluationService> localizer,
            UserManager<AppUser> userManager,
            IClassSchedule_Eval_RosterDomainService cs_eval_rosterService,
            IClassSchedule_EMP_LinkDomainService classScheduleEmployeeLinkService,
            IClassSchedule_Eval_LinkDomainService classSchedule_Eval_LinkService,
            IStudentEvaluationDomainService evalService,
            IClassScheduleDomainService classScheduleService,
            IILAStudentEvalLinkDomainService ila_evalLinkService,
            IStudentEvalAudienceDomainService evalAudienceService)
        {
            _studentEvalutionWithoutEmpService = studentEvalutionWithoutEmpService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _userManager = userManager;
            _cs_eval_rosterService = cs_eval_rosterService;
            _classScheduleEmployeeLinkService = classScheduleEmployeeLinkService;
            _classSchedule_Eval_LinkService = classSchedule_Eval_LinkService;
            _evalService = evalService;
            _classScheduleService = classScheduleService;
            _ila_evalLinkService = ila_evalLinkService;
            _evalAudienceService = evalAudienceService;
            _studentEvaluation = new StudentEvaluation();
        }

        public async System.Threading.Tasks.Task MarkAsCompleted(int empId, int classId, int evalId)
        {
            var roster = await _cs_eval_rosterService.FindQuery(x => x.EmployeeId == empId && x.ClassScheduleId == classId && x.StudentEvaluationId == evalId).FirstOrDefaultAsync();
            if (roster != null)
            {
                roster.CompletedDate = DateTime.Now;
                roster.IsCompleted = true;
                roster.IsStarted = true;
                var validationResult = await _cs_eval_rosterService.UpdateAsync(roster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
            else
            {
                roster = new ClassSchedule_Evaluation_Roster(null, DateTime.Now, classId, empId, true, false, true, evalId);
                var validationResult = await _cs_eval_rosterService.AddAsync(roster);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
        }

        public async Task<List<StudentEvaluationWithEMPVM>> GetDataForEvalWithEMPAsync(int evalId, int classId)
        {
            List<StudentEvaluationWithEMPVM> evals = new List<StudentEvaluationWithEMPVM>();
            var emps = await _classScheduleEmployeeLinkService.FindQueryWithIncludeAsync(x => x.ClassScheduleId == classId && x.IsEnrolled == true, new string[] { "Employee.Person" }).ToListAsync();
            emps = emps.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, ClassScheduleEmployeeOperations.Read).Result.Succeeded).ToList();
            foreach (var emp in emps)
            {
                var eval = new StudentEvaluationWithEMPVM();
                eval.EmpId = emp.Employee.Id;
                eval.EmpName = emp.Employee.Person.FirstName + " " + emp.Employee.Person.LastName;
                eval.EmpEmail = emp.Employee.Person.Username;
                eval.EvaluationId = evalId;
                eval.EmpImage = emp.Employee.Person.Image;
                var roster = await _cs_eval_rosterService.FindQuery(x => x.EmployeeId == eval.EmpId && x.StudentEvaluationId == eval.EvaluationId && x.ClassScheduleId == classId).FirstOrDefaultAsync();
                if (roster == null)
                {
                    roster = new ClassSchedule_Evaluation_Roster();
                }
                var rosterAuth = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, roster, ClassSchedule_Evaluation_RosterOperations.Read);
                if (rosterAuth.Succeeded)
                {
                    if (roster.Id == 0)
                    {
                        eval.CompletedDate = null;
                        eval.ReleaseDate = null;
                        eval.IsReleased = false;
                        eval.HasAggregateData = false;
                        var questions = await _evalService.FindQueryWithIncludeAsync(x => x.Id == eval.EvaluationId, new string[] { "StudentEvaluationQuestions" }).Select(s => s.StudentEvaluationQuestions).FirstOrDefaultAsync();
                        foreach (var question in questions)
                        {
                            var rating = new RatingList();
                            rating.QuestionId = question.Id;
                            rating.Rating = null;
                            eval.Rating.Add(rating);
                        }
                        if (questions.Count < 1)
                        {
                            var rating = new RatingList();
                            rating.QuestionId = null;
                            rating.Rating = null;
                            eval.Rating.Add(rating);
                        }
                    }
                    else
                    {                       
                        eval.CompletedDate = roster.CompletedDate;
                        eval.ReleaseDate = roster.ReleaseDate;
                        eval.IsReleased = roster.IsReleased;
                        if (roster.IsCompleted)
                        {
                            eval.HasAggregateData = await _studentEvalutionWithoutEmpService.FindQuery(x => x.ClassScheduleId == classId && x.StudentEvaluationId == roster.StudentEvaluationId && x.EmployeeId == roster.EmployeeId && x.DataMode == "Aggregate").AnyAsync();
                        }

                        var questions = await _evalService.FindQueryWithIncludeAsync(x => x.Id == eval.EvaluationId, new string[] { "StudentEvaluationQuestions.QuestionBank" }).Select(s => s.StudentEvaluationQuestions).FirstOrDefaultAsync();
                        foreach (var question in questions)
                        {
                            var solvedQuestion = await _studentEvalutionWithoutEmpService.FindQuery(x => x.EmployeeId == emp.Employee.Id && x.QuestionId == question.QuestionBankId && x.ClassScheduleId == classId && x.StudentEvaluationId == evalId).FirstOrDefaultAsync();
                            var rating = new RatingList();
                            if (solvedQuestion == null)
                            {
                                rating.QuestionId = question.QuestionBankId;
                                rating.Rating = null;
                            }
                            else
                            {
                                rating.QuestionId = solvedQuestion.QuestionId;
                                rating.Rating = solvedQuestion.RatingScale;
                            }
                            eval.Rating.Add(rating);
                        }
                        if (questions.Count < 1)
                        {
                            var rating = new RatingList();
                            rating.QuestionId = null;
                            rating.Rating = null;
                            eval.Rating.Add(rating);
                        }
                    }
                    evals.Add(eval);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                }
            }
            return evals;
        }

        public async System.Threading.Tasks.Task CreateAsync(StudentEvaluationWithoutEmpCreateOptions options)
        {

            if (options.studentEvalData != null && options.studentEvalData.Count > 0)
            {
                if (options.DataMode == "Aggregate")
                {
                    var aggRecords = await _studentEvalutionWithoutEmpService.FindQuery(x => x.StudentEvaluationId == options.StudentEvaluationId && x.ClassScheduleId == options.ClassScheduleId && x.EmployeeId == options.EmpId && x.DataMode == "Individual").ToListAsync();
                    foreach (var aggRecord in aggRecords)
                    {
                        aggRecord.Delete();
                        await _studentEvalutionWithoutEmpService.UpdateAsync(aggRecord);
                    }
                    foreach (var quesAns in options.studentEvalData)
                    {
                        var record = await _studentEvalutionWithoutEmpService.FindQuery(x => x.ClassScheduleId == options.ClassScheduleId && x.StudentEvaluationId == options.StudentEvaluationId && x.EmployeeId == options.EmpId && x.QuestionId == quesAns.QuestionId && x.DataMode == "Aggregate").FirstOrDefaultAsync();
                        if (record == null)
                        {
                            var studentEvalWithoutEmp = new StudentEvaluationWithoutEmp(options.StudentEvaluationId, options.ClassScheduleId, quesAns.QuestionId, options.DataMode, null, quesAns.High, quesAns.Average, quesAns.Low, quesAns.Notes, options.AdditionalComments, true, options.EmpId);
                            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvalWithoutEmp, StudentEvaluationWithoutEmpOperations.Create);
                            if (result.Succeeded)
                            {
                                studentEvalWithoutEmp.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                                studentEvalWithoutEmp.CreatedDate = DateTime.Now;
                                var validationResult = await _studentEvalutionWithoutEmpService.AddAsync(studentEvalWithoutEmp);
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
                        else
                        {
                            record.High = quesAns.High;
                            record.Low = quesAns.Low;
                            record.Average = quesAns.Average;
                            record.Notes = quesAns.Notes;
                            record.AdditionalComments = options.AdditionalComments;
                            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, record, StudentEvaluationWithoutEmpOperations.Update);
                            if (result.Succeeded)
                            {
                                //record.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                                //record.CreatedDate = DateTime.Now;
                                var validationResult = await _studentEvalutionWithoutEmpService.UpdateAsync(record);
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
                }
                else
                {
                    var aggRecords = await _studentEvalutionWithoutEmpService.FindQuery(x => x.StudentEvaluationId == options.StudentEvaluationId && x.ClassScheduleId == options.ClassScheduleId && x.EmployeeId == options.EmpId && x.DataMode == "Aggregate").ToListAsync();
                    foreach (var aggRecord in aggRecords)
                    {
                        aggRecord.Delete();
                        await _studentEvalutionWithoutEmpService.UpdateAsync(aggRecord);
                    }

                    foreach (var quesAns in options.studentEvalData)
                    {
                        var record = await _studentEvalutionWithoutEmpService.FindQuery(x => x.ClassScheduleId == options.ClassScheduleId && x.StudentEvaluationId == options.StudentEvaluationId && x.EmployeeId == options.EmpId && x.QuestionId == quesAns.QuestionId && x.DataMode == "Individual").FirstOrDefaultAsync();
                        if (record == null)
                        {
                            var studentEvalWithoutEmp = new StudentEvaluationWithoutEmp(options.StudentEvaluationId, options.ClassScheduleId, quesAns.QuestionId, options.DataMode, quesAns.RatingScale, 0.0, 0.0, 0.0, quesAns.Notes, options.AdditionalComments, true, options.EmpId);
                            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvalWithoutEmp, StudentEvaluationWithoutEmpOperations.Create);
                            if (result.Succeeded)
                            {
                                studentEvalWithoutEmp.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                                studentEvalWithoutEmp.CreatedDate = DateTime.Now;
                                var validationResult = await _studentEvalutionWithoutEmpService.AddAsync(studentEvalWithoutEmp);
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
                        else
                        {
                            record.RatingScale = quesAns.RatingScale;
                            record.Notes = quesAns.Notes;
                            record.AdditionalComments = options.AdditionalComments;
                            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, record, StudentEvaluationWithoutEmpOperations.Update);
                            if (result.Succeeded)
                            {
                                //record.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                                //record.CreatedDate = DateTime.Now;
                                var validationResult = await _studentEvalutionWithoutEmpService.UpdateAsync(record);
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
                }
                await MarkAsCompleted((int)options.EmpId, options.ClassScheduleId, options.StudentEvaluationId);
            }
        }

        public async Task<List<StudentEvaluation>> GetEvaluationsForClassAsync(int classId)
        {
            //commenting this now as it'll be remove later

            //await CheckEvaluations(classId);

            var evals = await _classSchedule_Eval_LinkService.FindQueryWithIncludeAsync(x => x.ClassScheduleId == classId, new string[] { "StudentEvaluation" }).Select(s => s.StudentEvaluation).ToListAsync();
            evals = evals.Where(w => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, w, StudentEvaluationOperations.Read).Result.Succeeded).ToList();
            return evals;
        }

        //public async System.Threading.Tasks.Task CheckEvaluations(int classId)
        //{
        //    var ilaId = await _classScheduleService.FindQuery(x => x.Id == classId).Select(s => s.ILAID).FirstOrDefaultAsync();
        //    if (ilaId.HasValue)
        //    {
        //        var evalLinks = await _ila_evalLinkService.FindQuery(x => x.ILAId == ilaId).ToListAsync();
        //        foreach (var link in evalLinks)
        //        {
        //            var audience = await _evalAudienceService.FindQuery(x => x.Id == link.studentEvalAudienceID).FirstOrDefaultAsync();
        //            if (audience != null)
        //            {
        //                switch (audience.Name.Trim().ToLower())
        //                {
        //                    case "all enrolled employees":
        //                        var hasLink = await _classSchedule_Eval_LinkService.FindQuery(x => x.ClassScheduleId == classId && x.StudentEvaluationId == link.studentEvalFormID).AnyAsync();
        //                        if (!hasLink)
        //                        {
        //                            var allClass = await _classScheduleService.FindQuery(x => x.Id == classId).FirstOrDefaultAsync();
        //                            if (allClass != null && allClass.Id == classId)
        //                            {
        //                                var eval = await _evalService.FindQueryWithIncludeAsync(x => x.Id == link.studentEvalFormID, new string[] { nameof(_studentEvaluation.ClassSchedule_StudentEvaluations_Links) }).FirstOrDefaultAsync();
        //                                if (eval != null)
        //                                {
        //                                    eval.LinkClassSchedule(allClass);
        //                                    await _evalService.UpdateAsync(eval);
        //                                }
        //                            }
        //                        }
        //                        break;
        //                    case "first class only (pilot class)":
        //                        var firstClass = await _classScheduleService.AllQuery().OrderBy(o => o.StartDateTime).FirstOrDefaultAsync();
        //                        if (firstClass != null && firstClass.Id == classId)
        //                        {
        //                            var eval = await _evalService.FindQueryWithIncludeAsync(x => x.Id == link.studentEvalFormID, new string[] { nameof(_studentEvaluation.ClassSchedule_StudentEvaluations_Links) }).FirstOrDefaultAsync();
        //                            if (eval != null)
        //                            {
        //                                eval.LinkClassSchedule(firstClass);
        //                            }
        //                        }
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //}

        public async Task<List<StudentEvaluationWithEMPVM>> GetAllEvaluationDataForClass(int classId)
        {
            var evals = await _classSchedule_Eval_LinkService.FindQueryWithIncludeAsync(x => x.ClassScheduleId == classId, new string[] { "StudentEvaluation" }).Select(s => s.StudentEvaluation).ToListAsync();
            evals = evals.Where(w => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, w, StudentEvaluationOperations.Read).Result.Succeeded).ToList();
            List<StudentEvaluationWithEMPVM> withEMPList = new List<StudentEvaluationWithEMPVM>();
            foreach (var eval in evals)
            {
                var evalsWithEMP = await GetDataForEvalWithEMPAsync(eval.Id, classId);
                if (evalsWithEMP.Count > 0)
                {
                    withEMPList.AddRange(evalsWithEMP);
                }
            }
            return withEMPList;
        }

        public async System.Threading.Tasks.Task ReleaseOrRecallEvalAsync(EvalReleaseOptions option)
        {
            var shouldRelease = false;
            var isRecalled = false;
            switch (option.Action.Trim().ToLower())
            {
                case "recalled":
                    shouldRelease = false;
                    isRecalled = true;
                    break;
                case "released":
                    shouldRelease = true;
                    break;
            }

            var roster = await _cs_eval_rosterService.FindQuery(x => x.StudentEvaluationId == option.EvalId && x.EmployeeId == option.EmpId && x.ClassScheduleId == option.ClassId).FirstOrDefaultAsync();
            if (roster == null)
            {
                roster = new ClassSchedule_Evaluation_Roster();
                var auth = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, roster, ClassSchedule_Evaluation_RosterOperations.Create);
                if (auth.Succeeded)
                {
                    roster.StudentEvaluationId = option.EvalId;
                    roster.ClassScheduleId = option.ClassId;
                    roster.EmployeeId = option.EmpId;
                    roster.IsReleased = shouldRelease;
                    roster.IsRecalled = isRecalled;
                    if (shouldRelease)
                    {
                        roster.Release(DateTime.Now);
                    }
                    var validationResult = await _cs_eval_rosterService.AddAsync(roster);
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
                var auth = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, roster, ClassSchedule_Evaluation_RosterOperations.Update);
                if (auth.Succeeded)
                {
                    roster.IsReleased = shouldRelease;
                    roster.IsRecalled = isRecalled;
                    if (shouldRelease)
                    {
                        roster.Release(DateTime.Now);
                    }
                    else
                    {
                        roster.ReleaseDate = null;
                    }
                    var validationResult = await _cs_eval_rosterService.UpdateAsync(roster);
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
    }
}