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
using QTD2.Infrastructure.Model.ILATraineeEvaluation;
using IILATraineeEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService;
using ITraineeEvaluationTypeDomainService = QTD2.Domain.Interfaces.Service.Core.ITraineeEvaluationTypeService;
using ITestDomainService = QTD2.Domain.Interfaces.Service.Core.ITestService;
using ITest_TestItemDomainService = QTD2.Domain.Interfaces.Service.Core.ITest_Item_LinkService;
using IDiscussionQuestionDomainService = QTD2.Domain.Interfaces.Service.Core.IDiscussionQuestionService;
using IIILA_PerformTraineeEvalDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_PerformTraineeEvalService;
using IILATaskObjectiveLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService;
using QTD2.Infrastructure.Model.EvalutionType;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class ILATraineeEvaluationService : IILATraineeEvaluationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ILATraineeEvaluation> _localizer;
        private readonly IILATraineeEvaluationDomainService _iLATraineeEvaluationService;
        private readonly IIILA_PerformTraineeEvalDomainService _iILA_PerformTraineeEvalDomainService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITraineeEvaluationTypeDomainService _tr_typeService;
        private readonly ITestDomainService _test_Service;
        private readonly ITest_TestItemDomainService _test_ItemLinkService;
        private readonly IDiscussionQuestionDomainService _discuss_QuestionService;
        private readonly IILATaskObjectiveLinkDomainService _ilaTaskObjectiveLinkService;

        public ILATraineeEvaluationService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<ILATraineeEvaluation> localizer,
            IILATraineeEvaluationDomainService iLATraineeEvaluationService,
            UserManager<AppUser> userManager,
            ITraineeEvaluationTypeDomainService tr_typeService,
            ITestDomainService test_Service,
            ITest_TestItemDomainService testItemService,
            IDiscussionQuestionDomainService discuss_QuestionService,
            IIILA_PerformTraineeEvalDomainService iILA_PerformTraineeEvalDomainService, IILATaskObjectiveLinkDomainService ilaTaskObjectiveLinkService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _iLATraineeEvaluationService = iLATraineeEvaluationService;
            _userManager = userManager;
            _tr_typeService = tr_typeService;
            _test_Service = test_Service;
            _test_ItemLinkService = testItemService;
            _discuss_QuestionService = discuss_QuestionService;
            _iILA_PerformTraineeEvalDomainService = iILA_PerformTraineeEvalDomainService;
            _ilaTaskObjectiveLinkService = ilaTaskObjectiveLinkService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _iLATraineeEvaluationService.UpdateAsync(obj);
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

        public async Task<ILATraineeEvaluation> CreateAsync(ILATraineeEvaluationCreateOptions options)
        {
            var obj = (await _iLATraineeEvaluationService.FindAsync(x => x.TestId == options.TestId && x.ILAId == options.ILAId && x.EvaluationTypeId == options.EvaluationTypeId)).FirstOrDefault();
            if (obj == null)
            {
                if(options.EvaluationTypeId == null)
                {
                    options.EvaluationTypeId = await _tr_typeService.FindQuery(x=>x.Name == "Written").Select(x=>x.Id).FirstOrDefaultAsync();
                }
                obj = new ILATraineeEvaluation(options.TestId, options.ILAId, (int)options.EvaluationTypeId, options.TestTitle, options.TestInstruction, options.TestTimeLimitHours, options.TestTimeLimitMinutes, options.TrainingEvaluationMethod, options.TestTypeId);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
                if (result.Succeeded)
                {
                    obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    obj.CreatedDate = DateTime.Now;
                    var validationResult = await _iLATraineeEvaluationService.AddAsync(obj);
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
            else
            {
                obj.TestTitle = options.TestTitle;
                obj.TestInstruction = options.TestInstruction;
                obj.TestTimeLimitHours = options.TestTimeLimitHours;
                obj.TestTimeLimitMinutes = options.TestTimeLimitMinutes;
                obj.TestTypeId = options.TestTypeId;
                obj.TrainingEvaluationMethod = options.TrainingEvaluationMethod;
                obj.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);
                var validationResult = await _iLATraineeEvaluationService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",",validationResult.Errors)]);
                }
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _iLATraineeEvaluationService.UpdateAsync(obj);
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

        public async Task<List<ILATraineeEvaluation>> GetAsync()
        {
            var obj_list = await _iLATraineeEvaluationService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }
        public async Task<bool> RemoveTraineeTypeIlaIdAsync(int ilaId)
        {
            var performTraineeEval = (await _iILA_PerformTraineeEvalDomainService.FindAsync(x => x.ILAId == ilaId)).FirstOrDefault();
            if (performTraineeEval!=null)
            {
                var links = await _ilaTaskObjectiveLinkService.FindQuery(x => x.ILAId == ilaId).ToListAsync();
                foreach(var link in links)
                {
                    link.UseForTQ = false;
                }
                await _ilaTaskObjectiveLinkService.BulkUpdateAsync(links);
                var result=await _iILA_PerformTraineeEvalDomainService.DeleteAsync(performTraineeEval);
                return result.IsValid;
            }
            return false;

        }
        
        public async Task<bool> ChangeTraineeEvaluationStatus(TraineeEvaluationStatusVM data)
        {
            if (data.Type=="Perform")
            {
                var performTraineeEval = (await _iILA_PerformTraineeEvalDomainService.FindAsync(x => x.Id == data.EvaluationId)).FirstOrDefault();
                if (performTraineeEval != null)
                {
                    performTraineeEval.Active = data.Status;
                    var result = await _iILA_PerformTraineeEvalDomainService.UpdateAsync(performTraineeEval);
                    return result.IsValid;
                }
            }
            else
            {
                var evals = (await _iLATraineeEvaluationService.FindAsync(x => x.Id == data.EvaluationId)).FirstOrDefault();
                if (evals != null)
                {
                    evals.Active = data.Status;
                    var result = await _iLATraineeEvaluationService.UpdateAsync(evals);
                    return result.IsValid;
                }
            }
            return false;

        }

        public async Task<List<IlaTraineeEvaluationListVm>> GetTraineeEvaluationByILAAsync(int id)
        {
            var toReturnList = new List<IlaTraineeEvaluationListVm>();
            var performTraineeEval=( await _iILA_PerformTraineeEvalDomainService.FindAsync(x => x.ILAId == id)).FirstOrDefault();
            var evals = await _iLATraineeEvaluationService.FindQueryWithIncludeAsync(x => x.ILAId == id,new string[] { "TraineeEvaluationType" },true).ToListAsync();
            evals = evals.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User,x,AuthorizationOperations.Read).Result.Succeeded).ToList();
            foreach (var item in evals)
            {
                var method = new IlaTraineeEvaluationListVm();
                method.EvaluationId = item.Id;
                method.EvaluationType = "regular";
                method.EvaluationMethodType = item.TraineeEvaluationType.Name;
                method.EvaluationDescription = item.TestTitle;
                method.IsActive = item.Active;
                method.data = item;
                toReturnList.Add(method);
            }
            if (performTraineeEval!=null)
            {
                var method = new IlaTraineeEvaluationListVm();
                method.EvaluationId = performTraineeEval.Id;
                method.EvaluationType = "Perform";
                method.EvaluationMethodType = "Perform";
                method.EvaluationDescription = performTraineeEval.Title;
                method.IsActive = performTraineeEval.Active;
                method.data = performTraineeEval;
                toReturnList.Add(method);
            }
            return toReturnList;
        }

        public async Task<ILATraineeEvaluation> GetAsync(int id)
        {
            var obj = await _iLATraineeEvaluationService.GetAsync(id);
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

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _iLATraineeEvaluationService.UpdateAsync(obj);
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

        public async Task<ILATraineeEvaluation> UpdateAsync(int id, ILATraineeEvaluationCreateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                if (options.EvaluationTypeId == null)
                {
                    options.EvaluationTypeId = await _tr_typeService.FindQuery(x => x.Name == "Written").Select(x => x.Id).FirstOrDefaultAsync();

                }
                obj.TestId = options.TestId;
                obj.ILAId = options.ILAId;
                obj.TestTypeId = options.TestTypeId;
                obj.TestInstruction = options.TestInstruction;
                obj.TestTimeLimitMinutes = options.TestTimeLimitMinutes;
                obj.TestTimeLimitHours = options.TestTimeLimitHours;
                obj.TestTitle = options.TestTitle;
                obj.EvaluationTypeId = (int)options.EvaluationTypeId;
                obj.TrainingEvaluationMethod = options.TrainingEvaluationMethod;
                obj.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);
                var validationResult = await _iLATraineeEvaluationService.UpdateAsync(obj);
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

        public async Task<ILATraineeEvaluation> CopyTraineeEvaluationAsync(int id)
        {
            var ilaTr = await _iLATraineeEvaluationService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if(ilaTr == null)
            {
                throw new BadHttpRequestException(message: _localizer["TraineeEvaluationDoesNotExistException"]);
            }
            else
            {
                var test = await _test_Service.FindQuery(x => x.Id == ilaTr.TestId).FirstOrDefaultAsync();
                if(test == null)
                {
                    throw new BadHttpRequestException(message: _localizer["TestNotFoundException"]);
                }
                else
                {
                    Test copyTest = new Test(test.TestStatusId, test.TestTitle + " - Copy");
                    await _test_Service.AddAsync(copyTest);
                    ILATraineeEvaluation copy = new ILATraineeEvaluation(copyTest.Id, ilaTr.ILAId, ilaTr.EvaluationTypeId, ilaTr.TestTitle + " - Copy", ilaTr.TestInstruction, ilaTr.TestTimeLimitHours, ilaTr.TestTimeLimitMinutes, ilaTr.TrainingEvaluationMethod, ilaTr.TestTypeId);
                    var validationResult = await _iLATraineeEvaluationService.AddAsync(copy);
                    if (validationResult.IsValid)
                    {
                        var type = await _tr_typeService.FindQuery(x => x.Id == copy.EvaluationTypeId).FirstOrDefaultAsync();
                        switch (type.Name.Trim().ToLower())
                        {
                            case "written":
                                var questions = await _test_ItemLinkService.FindQuery(x => x.TestId == test.Id).ToListAsync();
                                foreach(var question in questions)
                                {
                                    Test_Item_Link link = new Test_Item_Link();
                                    link.Sequence = question.Sequence;
                                    link.TestId = copyTest.Id;
                                    link.TestItemId = question.TestItemId;
                                    await _test_ItemLinkService.AddAsync(link);
                                }
                                break;
                            case "discuss":
                                var discussQuestions = await _discuss_QuestionService.FindQuery(x => x.ILATraineeEvaluationId == ilaTr.Id).ToListAsync();
                                foreach(var que in discussQuestions)
                                {
                                    DiscussionQuestion copyQue = new DiscussionQuestion(copy.Id,que.QuestionText,que.QuestionFileUpload,que.QuestionImageUpload,que.QuestionLinksUpload,que.AnswerKeywords,que.AnswerImageUpload,que.AnswerFileUpload);
                                    await _discuss_QuestionService.AddAsync(copyQue);
                                }
                                break;
                        }

                        return copy;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                    }
                }
            }
        }
    }
}
