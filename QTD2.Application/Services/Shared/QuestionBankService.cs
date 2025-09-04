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
using QTD2.Infrastructure.Model.QuestionBankHistory;
using QTD2.Infrastructure.Model.StudentEvaluation_Question_Link;
using QTD2.Infrastructure.Model.StudentEvaluationHistory;
using IQuestionBankDomainService = QTD2.Domain.Interfaces.Service.Core.IQuestionBankService;
using IStudentEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluationService;
using IQuestionBankHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IQuestionBankHistoryService;
using IIStudentEvaluationHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluationHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class QuestionBankService : IQuestionBankService
    {
        private readonly IQuestionBankDomainService _questionBankService;
        private readonly IStudentEvaluationDomainService _studentEvaluationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<QuestionBank> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IQuestionBankHistoryDomainService _questionBankHistoryService;
        private readonly IIStudentEvaluationHistoryDomainService _studentEvaluationHistoryService;
        private readonly StudentEvaluation _studentEvaluation;

        public QuestionBankService(
            IQuestionBankDomainService questionBankService,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<QuestionBank> localizer,
            UserManager<AppUser> userManager,
            IStudentEvaluationDomainService studentEvaluationService,
            IQuestionBankHistoryDomainService questionBankHistoryService,
            IIStudentEvaluationHistoryDomainService studentEvaluationHistoryService)
        {
            _questionBankService = questionBankService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _userManager = userManager;
            _studentEvaluationService = studentEvaluationService;
            _questionBankHistoryService = questionBankHistoryService;
            _studentEvaluationHistoryService = studentEvaluationHistoryService;
            _studentEvaluation = new StudentEvaluation();
        }

        public async System.Threading.Tasks.Task ActivateAsync(int id)
        {
            var question = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, QuestionBankOperations.Update);
            if (result.Succeeded)
            {
                question.Activate();
                var validationResult = await _questionBankService.UpdateAsync(question);

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

        public async Task<QuestionBank> CreateAync(QuestionBankCreateOptions options)
        {
            var question = new QuestionBank(options.Stem);
            var questionExists = (await _questionBankService.FindAsync(r => r.Stem == options.Stem)).FirstOrDefault() != null;
            if (questionExists)
            {
                throw new BadHttpRequestException(message: _localizer["Question Already Exists"]);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, QuestionBankOperations.Create);

            if (result.Succeeded)
            {
                question.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                question.CreatedDate = DateTime.Now;
                var validationResult = await _questionBankService.AddAsync(question);

                if (validationResult.IsValid)
                {
                    question = (await _questionBankService.FindAsync(r => r.Stem == options.Stem)).FirstOrDefault();
                    return question;
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

        public async System.Threading.Tasks.Task DeactivateAsync(int id)
        {
            var question = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, QuestionBankOperations.Update);
            if (result.Succeeded)
            {
                question.Deactivate();
                var validationResult = await _questionBankService.UpdateAsync(question);

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

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var studentEvaluation = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, QuestionBankOperations.Delete);
            if (result.Succeeded)
            {
                studentEvaluation.Delete();
                var validationResult = await _questionBankService.UpdateAsync(studentEvaluation);

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

        public async Task<List<QuestionBankCustomModel>> GetAsync()
        {
            var list = new List<QuestionBankCustomModel>();
            var question = await _questionBankService.AllAsync();
            question = question.Where(p => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, p, QuestionBankOperations.Read).Result.Succeeded);
            if (question.Count() > 0)
            {
                foreach (var ques in question)
                {
                    list.Add(new QuestionBankCustomModel()
                    {
                        Id = ques.Id,
                        Stem = ques.Stem,
                        questionId = "QTD_0" + ques.Id,
                        Active = ques.Active
                    });
                }
            }
            return list.ToList();
        }

        public async Task<QuestionBank> GetAsync(int id)
        {
            var question = await _questionBankService.GetAsync(id);
            if (question != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, QuestionBankOperations.Read);
                if (result.Succeeded)
                {
                    return question;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return question;
        }

        public async Task<QuestionBank> UpdateAsync(int id, QuestionBankCreateOptions option)
        {
            var question = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, QuestionBankOperations.Update);
            if (result.Succeeded)
            {
                question.Stem = option.Stem;
                question.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                question.ModifiedDate = DateTime.Now;
                var validationResult = await _questionBankService.UpdateAsync(question);

                if (validationResult.IsValid)
                {
                    return question;
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
        public async Task<List<int>> CreateEvalutaionAndLinkQUestionAync(QuestionBankCreateOptions options)
        {
            var questionIdsArray = new List<int>();
            if(options.stemArray != null && options.stemArray.Count > 0)
            {
                foreach (var ques in options.stemArray)
                {
                    var question = new QuestionBank(ques);
                    var questionExists = (await _questionBankService.FindAsync(r => r.Stem == options.Stem)).FirstOrDefault() != null;
                    if (!questionExists)
                    {
                        var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, QuestionBankOperations.Create);
                        if (result.Succeeded)
                        {
                            question.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                            question.CreatedDate = DateTime.Now;
                            var validationResult = await _questionBankService.AddAsync(question);
                            var histOptions = new QuestionBankHistoryCreateOptions();
                            histOptions.EffectiveDate = DateTime.UtcNow;
                            histOptions.QuestionBankNotes = "Question Created : " + question.Stem;
                            histOptions.QuestionBankId = question.Id;
                            await _questionBankHistoryService.AddAsync(new QuestionBankHistory()
                            { 
                            EffectiveDate = DateTime.UtcNow,
                            QuestionBankNotes = "Question Created : " + question.Stem,
                            QuestionBankId = question.Id,
                            });
                            questionIdsArray.Add(question.Id);
                            if (validationResult.IsValid)
                            {
                                var quesoptions = new StudentEvaluation_Question_LinkCreateOptions();
                                quesoptions.QuestionIds = questionIdsArray.ToArray();

                                foreach (var id in quesoptions.QuestionIds)
                                {
                                    var studentEvaluation = await _studentEvaluationService.GetWithIncludeAsync(options.studentEvaluationId, new string[] { nameof(_studentEvaluation.StudentEvaluationQuestions) });
                                    var questionToLink = await _questionBankService.GetAsync(id);

                                    var studentEvaluationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, studentEvaluation, StudentEvaluationOperations.Update);
                                    var questionBankResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, QuestionBankOperations.Read);
                                    if (studentEvaluationResult.Succeeded && questionBankResult.Succeeded)
                                    {
                                        studentEvaluation.LinkQuestion(question);
                                        var stdEvalvalidationResult = await _studentEvaluationService.UpdateAsync(studentEvaluation);
                                        if (!stdEvalvalidationResult.IsValid)
                                        {
                                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                                        }
                                        await _studentEvaluationHistoryService.AddAsync(new StudentEvaluationHistory()
                                        {
                                            StudentEvaluationNotes = "Student Evaluation Linked to Question  Id => " + question.Id,
                                            EffectiveDate = DateTime.Now,
                                            StudentEvaluationId = options.studentEvaluationId,
                                        });
                                    }
                                    else
                                    {
                                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                                    }
                                }
                                //await _studentEvaluationService.LinkQuestions(options.studentEvaluationId, quesoptions);
                                
                            }

                        }

                        else
                        {
                            throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                        }
                    }
                }
            }
            return questionIdsArray;
        }
    }
}
