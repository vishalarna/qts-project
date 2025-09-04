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
using QTD2.Infrastructure.Model.DiscussionQuestion;
using IDiscussionQuestionDomainService = QTD2.Domain.Interfaces.Service.Core.IDiscussionQuestionService;

namespace QTD2.Application.Services.Shared
{
    public class DiscussionQuestionService : IDiscussionQuestionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<DiscussionQuestionService> _localizer;
        private readonly IDiscussionQuestionDomainService _discussionQuestionService;
        private readonly UserManager<AppUser> _userManager;

        public DiscussionQuestionService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<DiscussionQuestionService> localizer, IDiscussionQuestionDomainService discussionQuestionService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _discussionQuestionService = discussionQuestionService;
            _userManager = userManager;
        }

        public async Task<DiscussionQuestion> CreateAsync(DiscussionQuestionCreateOptions options)
        {
            var obj = (await _discussionQuestionService.FindAsync(x => x.ILATraineeEvaluationId == options.ILATraineeEvaluationId)).FirstOrDefault();
            if (obj == null)
            {
                obj = new DiscussionQuestion(options.ILATraineeEvaluationId, options.QuestionText, options.QuestionFileUpload, options.QuestionImageUpload, options.QuestionLinksUpload, options.AnswerKeywords, options.AnswerImageUpload, options.AnswerFileUpload);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _discussionQuestionService.AddAsync(obj);
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

        public async Task<List<DiscussionQuestion>> GetDiscussionQuestionsAsync(int id)
        {
            var questions = await _discussionQuestionService.FindQuery(x => x.ILATraineeEvaluationId == id).ToListAsync();
            questions = questions.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, AuthorizationOperations.Read).Result.Succeeded).ToList();
            return questions;
        }

        public async System.Threading.Tasks.Task RemoveAllQuestions(int id)
        {
            var questions = await _discussionQuestionService.FindQuery(x => x.ILATraineeEvaluationId == id).ToListAsync();
            for(int i = 0; i < questions.Count; i++)
            {
                await _discussionQuestionService.DeleteAsync(questions[i]);
            }
        } 
    }
}
