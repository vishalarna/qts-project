using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.StudentEvaluationQuestion;
using IStudentEvaluationQuestionDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluationQuestionService;
using IStudentEvalution_QuestionDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluation_QuestionService;
using IQuestionBankDomainService = QTD2.Domain.Interfaces.Service.Core.IQuestionBankService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class StudentEvaluationQuestionService : IStudentEvaluationQuestionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<StudentEvaluationQuestionService> _localizer;
        private readonly IStudentEvaluationQuestionDomainService _studentEvaluationQuestionService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStudentEvalution_QuestionDomainService _studentEvaluationLinkService;
        private readonly IQuestionBankDomainService _questionBankService;

        public StudentEvaluationQuestionService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<StudentEvaluationQuestionService> localizer, IStudentEvaluationQuestionDomainService studentEvaluationQuestionService, UserManager<AppUser> userManager, IStudentEvalution_QuestionDomainService studentEvaluationLinkService, IQuestionBankDomainService questionBankService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _studentEvaluationQuestionService = studentEvaluationQuestionService;
            _userManager = userManager;
            _studentEvaluationLinkService = studentEvaluationLinkService;
            _questionBankService = questionBankService;
        }

        public async Task<StudentEvaluationQuestion> GetAsync(int id)
        {
            var obj = await _studentEvaluationQuestionService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);
                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _studentEvaluationQuestionService.UpdateAsync(obj);
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

        public async Task<List<StudentEvaluationQuestion>> GetAsync()
        {
            var obj_list = await _studentEvaluationQuestionService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _studentEvaluationQuestionService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task DeleteAsync(int id)
            {
                var obj = await GetAsync(id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

                if (result.Succeeded)
                {
                    obj.Delete();

                    var validationResult = await _studentEvaluationQuestionService.UpdateAsync(obj);
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

        public async Task<StudentEvaluationQuestion> UpdateAsync(int id, StudentEvaluationQuestionUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
               // obj.Name = options.Name;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;

                var validationResult = await _studentEvaluationQuestionService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<List<QuestionBank>> GetStudentEvalQuestionsForEvalAsync(int id)
        {
            var stlinks = await _studentEvaluationLinkService.FindQuery(x => x.StudentEvaluationId == id).ToListAsync();
            List<QuestionBank> questions = new List<QuestionBank>();
            foreach (var stlink in stlinks)
            {
                var question = await _questionBankService.FindQuery(x => x.Id == stlink.QuestionBankId).FirstOrDefaultAsync();
                if(question != null)
                {
                    questions.Add(question);
                }
            }
            return questions;
        }

        public async Task<StudentEvaluationQuestion> CreateAsync(StudentEvaluationQuestionCreateOptions options)
        {
            /*var obj = (await _studentEvaluationQuestionService.FindAsync(x => x.EvalFormID == options.EvalFormID)).FirstOrDefault();
            if (obj == null)
            {*/
               var obj = new StudentEvaluationQuestion(options.EvalFormID, options.QuestionText, options.QuestionNumber, options.QuestionImage, options.QuestionFiles);
           /* }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }*/

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _studentEvaluationQuestionService.AddAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }
    }
}
