using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjective_Category;
using QTD2.Infrastructure.Model.EnablingObjective_Procedure_Link;
using QTD2.Infrastructure.Model.EnablingObjective_SaftyHazard_Link;
using QTD2.Infrastructure.Model.EnablingObjective_SubCategory;
using QTD2.Infrastructure.Model.EnablingObjective_Topic;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.Task;
using ITask_DomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using ITask_EO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_EnablingObjective_LinkService;
using IProcedure_EO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_EnablingObjective_LinkService;
using ISafetyHazard_EO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_EO_LinkService;
using IRR_DomainService = QTD2.Domain.Interfaces.Service.Core.IRegulatoryRequirementService;
using IRR_EO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IRegRequirement_EO_LinkService;
using IILA_DomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IILA_EO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_EnablingObjective_LinkService;
using ITestItemDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemService;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Application.Utils;
using System.Collections;
using QTD2.Infrastructure.Hashing.Interfaces;
using QTD2.Infrastructure.Model.EnablingObjective_MetaEO_Link;
using IEO_MetaEO_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_MetaEO_LinkService;
using QTD2.Infrastructure.Model.TestItem;
using ITestItem_Test_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITest_Item_LinkService;
using QTD2.Infrastructure.Model.EnablingObjective_Step;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using IPosition_SQLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_SQService;
using IEnablingObjective_Employee_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_Employee_LinkService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IEnablingObjective_SuggestionDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_SuggestionService;
using IEnablingObjective_ToolsDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_ToolService;
using ITask_TrainingGroupDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_TrainingGroupService;
using ITest_DomainService = QTD2.Domain.Interfaces.Service.Core.ITestService;
using IClassSchedule_Roster_DomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_RosterService;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.EnablingObjective_Question;
using QTD2.Infrastructure.Model.EnablingObjective_Suggestion;
using QTD2.Infrastructure.Model.Tool;
using QTD2.Infrastructure.Model.TreeDataVMs;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class EnablingObjectiveService : Interfaces.Services.Shared.IEnablingObjectiveService
    {
        private readonly Domain.Interfaces.Service.Core.IEnablingObjectiveService _enablingObjectiveService;
        private readonly IEnablingObjective_CategoryService _enablingObjective_CategoryService;
        private readonly IEnablingObjective_SubCategoryService _enablingObjective_SubCategoryService;
        private readonly IEnablingObjective_TopicService _enablingObjective_TopicService;
        private readonly Domain.Interfaces.Service.Core.ISaftyHazardService _saftyHazardService;
        private readonly Domain.Interfaces.Service.Core.IProcedureService _procedureService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<EnablingObjectiveService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly EnablingObjective_Category _eoCat;
        private readonly EnablingObjective_SubCategory _eoSub;
        private readonly EnablingObjective_Topic _eoTopic;
        private readonly EnablingObjective _eo;
        private readonly Domain.Interfaces.Service.Core.IILA_TopicService _topicService;
        private readonly ITask_DomainService _taskService;
        private readonly ITask_EO_LinkDomainService _task_eo_linkService;
        private readonly Task_EnablingObjective_Link _task_eo_link;
        private readonly IProcedure_EO_LinkDomainService _proc_eo_linkService;
        private readonly Procedure_EnablingObjective_Link _proc_eo_link;
        private readonly ISafetyHazard_EO_LinkDomainService _sh_eo_linkService;
        private readonly SafetyHazard_EO_Link _sh_eo_link;
        private readonly IRR_DomainService _rr_Service;
        private readonly IRR_EO_LinkDomainService _rr_eo_linkService;
        private readonly RegRequirement_EO_Link _rr_eo_link;
        private readonly IILA_DomainService _ilaService;
        private readonly IILA_EO_LinkDomainService _ila_eo_linkService;
        private readonly ILA_EnablingObjective_Link _ila_eo_link;
        private readonly ITestItemDomainService _testItemService;
        private readonly TestItem _testItem;
        private readonly IHasher _hasher;
        private readonly IEO_MetaEO_LinkDomainService _eo_metaEO_linkService;
        private readonly EnablingObjective_MetaEO_Link _eo_metaEO_link;
        private readonly ITestItem_Test_LinkDomainService _test_item_linkService;
        private readonly Test_Item_Link _test_item_link;
        private readonly IEnablingObjective_StepService _eo_StepService;
        private readonly EnablingObjective_Step _eo_step;
        private readonly IPositionDomainService _posService;
        private readonly IPosition_SQLinkDomainService _pos_sq_service;
        private readonly Positions_SQ _pos_sq;
        private readonly IEnablingObjective_Employee_LinkDomainService _eo_emp_linkService;
        private readonly IEmployeeDomainService _eo_empService;
        private readonly EnablingObjective_Employee_Link _eo_emp;
        private readonly Employee _emp;
        private readonly IEnablingObjective_QuestionService _eoquestionService;
        private readonly IEnablingObjective_SuggestionDomainService _eo_suggestionService;
        private readonly IEnablingObjective_ToolsDomainService _eo_toolService;
        private readonly Domain.Interfaces.Service.Core.IToolService _toolService;
        private readonly ITask_TrainingGroupDomainService _taskTrainingGroupService;
        private readonly ITest_DomainService _testService;
        private readonly IClassSchedule_Roster_DomainService _classScheduleRosterService;
        private readonly Domain.Entities.Core.Task _task;
        private readonly Interfaces.Services.Shared.IVersion_TaskService _versionTaskService;

        public EnablingObjectiveService(
           IHttpContextAccessor httpContextAccessor,
           IAuthorizationService authorizationService,
           Domain.Interfaces.Service.Core.IEnablingObjectiveService enablingObjectiveService,
           IEnablingObjective_CategoryService enablingObjective_CategoryService,
           IEnablingObjective_SubCategoryService enablingObjective_SubCategoryService,
           IEnablingObjective_TopicService enablingObjective_TopicService,
           Domain.Interfaces.Service.Core.ISaftyHazardService saftyHazardService,
           Domain.Interfaces.Service.Core.IProcedureService procedureService,
           Domain.Interfaces.Service.Core.IILA_TopicService topicService,
           IStringLocalizer<EnablingObjectiveService> localizer,
           UserManager<AppUser> userManager,
           ITask_DomainService taskService,
           ITask_EO_LinkDomainService task_eo_linkService,
           IProcedure_EO_LinkDomainService eo_proc_linkService,
           ISafetyHazard_EO_LinkDomainService eo_sh_linkService,
           IRR_DomainService rr_Service,
           IRR_EO_LinkDomainService rr_eo_linkService,
           IILA_DomainService ilaService,
           IILA_EO_LinkDomainService ila_eo_linkService,
           ITestItemDomainService testItemService,
           IHasher hasher,
           IEO_MetaEO_LinkDomainService eo_metaEO_linkService,
           ITestItem_Test_LinkDomainService test_item_linkService, IEnablingObjective_StepService eo_StepService,
           IPositionDomainService posService,
           IPosition_SQLinkDomainService pos_sq_service,
           IEnablingObjective_Employee_LinkDomainService eo_emp_linkService,
           IEmployeeDomainService eo_empService,
           IEnablingObjective_QuestionService eoquestionService,
           IEnablingObjective_SuggestionDomainService eo_suggestionService,
           IEnablingObjective_ToolsDomainService eo_toolService,
           Interfaces.Services.Shared.IVersion_TaskService versionTaskService,
           Domain.Interfaces.Service.Core.IToolService toolService, ITest_DomainService testService, IClassSchedule_Roster_DomainService classScheduleRosterService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _enablingObjectiveService = enablingObjectiveService;
            _enablingObjective_CategoryService = enablingObjective_CategoryService;
            _enablingObjective_SubCategoryService = enablingObjective_SubCategoryService;
            _enablingObjective_TopicService = enablingObjective_TopicService;
            _saftyHazardService = saftyHazardService;
            _procedureService = procedureService;
            _topicService = topicService;
            _localizer = localizer;
            _userManager = userManager;
            _eoSub = new EnablingObjective_SubCategory();
            _eoTopic = new EnablingObjective_Topic();
            _eoCat = new EnablingObjective_Category();
            _taskService = taskService;
            _task_eo_linkService = task_eo_linkService;
            _task_eo_link = new Task_EnablingObjective_Link();
            _proc_eo_linkService = eo_proc_linkService;
            _proc_eo_link = new Procedure_EnablingObjective_Link();
            _sh_eo_linkService = eo_sh_linkService;
            _sh_eo_link = new SafetyHazard_EO_Link();
            _rr_Service = rr_Service;
            _rr_eo_linkService = rr_eo_linkService;
            _rr_eo_link = new RegRequirement_EO_Link();
            _ilaService = ilaService;
            _ila_eo_linkService = ila_eo_linkService;
            _ila_eo_link = new ILA_EnablingObjective_Link();
            _testItemService = testItemService;
            _testItem = new TestItem();
            _hasher = hasher;
            _eo_metaEO_linkService = eo_metaEO_linkService;
            _eo = new EnablingObjective();
            _eo_metaEO_link = new EnablingObjective_MetaEO_Link();
            _test_item_linkService = test_item_linkService;
            _test_item_link = new Test_Item_Link();
            _posService = posService;
            _pos_sq_service = pos_sq_service;
            _pos_sq = new Positions_SQ();
            _eo_emp_linkService = eo_emp_linkService;
            _eo_empService = eo_empService;
            _eo_emp = new EnablingObjective_Employee_Link();
            _emp = new Employee();
            _eo_StepService = eo_StepService;
            _eoquestionService = eoquestionService;
            _eo_suggestionService = eo_suggestionService;
            _eo_toolService = eo_toolService;
            _toolService = toolService;
            _task = new Domain.Entities.Core.Task();
            _testService = testService;
            _classScheduleRosterService = classScheduleRosterService;
            _versionTaskService = versionTaskService;
        }

        public async Task<EnablingObjective> CreateAsync(EnablingObjectiveCreateOptions options)
        {
            // As Custom objective doesn't have an option for Number, if they add the field we'll get rid of assigning it from here
            //options.Number = (await _enablingObjectiveService.FindAsync(r => r.TopicId == options.TopicId)).LastOrDefault()?.Number + 1 ?? 1;
            var eolist = (await _enablingObjectiveService.FindWithIncludeAsync(r => r.CategoryId == options.CategoryId
            && r.SubCategoryId == options.SubCategoryId, new[] { "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category" })).ToList();

            if (options.TopicId != null)
            {
                var validateOptions = eolist.Where(r => r.Number == options.Number && r.TopicId == options.TopicId).FirstOrDefault();
                if (validateOptions != null)
                {
                    throw new BadHttpRequestException(message: _localizer["EnablingObjective Number Already In Use"]);
                }
            }
            else
            {
                var validateOptions = eolist.Where(r => r.Number == options.Number && r.TopicId.GetValueOrDefault() == 0).FirstOrDefault();
                if (validateOptions != null)
                {
                    throw new BadHttpRequestException(message: _localizer["EnablingObjective Number Already In Use"]);
                }
            }
            //options.Number = await GetEONumber(options.CategoryId, options.SubCategoryId, options.TopicId, options.Number);
            var eo = new EnablingObjective(options.CategoryId, options.SubCategoryId, options.TopicId, options.Number, options.Statement, options.isMetaEO, options.IsSkill, options.References, options.Criteria, options.Conditions, options.EffectiveDate);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Create);
            if (result.Succeeded)
            {
                eo.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                eo.CreatedDate = DateTime.Now;
                var validationResult = await _enablingObjectiveService.AddAsync(eo);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return new EnablingObjective
                    {
                        Id = eo.Id,
                    };
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<EnablingObjective> CreateFromILAAsync(EnablingObjectiveCreateOptions options)
        {
            var eoExists = (await _enablingObjectiveService.FindQueryWithDeleted(r => r.Number == options.Number).FirstOrDefaultAsync()) != null;
            if (eoExists)
            {
                throw new BadHttpRequestException(message: _localizer["EnablingObjective Number Already In Use"]);
            }

            options.Number = await GetEONumber(options.CategoryId, options.SubCategoryId, options.TopicId, "");
            var eo = new EnablingObjective(options.CategoryId, options.SubCategoryId, options.TopicId, options.Number, options.Statement, options.isMetaEO, options.IsSkill, options.References, options.Criteria, options.Conditions, options.EffectiveDate);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Create);
            if (result.Succeeded)
            {
                eo.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                eo.CreatedDate = DateTime.Now;
                var validationResult = await _enablingObjectiveService.AddAsync(eo);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return eo;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<EnablingObjective_Category> CreateCategoryAsync(EnablingObjective_CategoryOptions options)
        {
            // int eoNumber = (await _enablingObjective_CategoryService.AllQueryWithIncludeAndDeleted(new string[] { nameof(_eo.EnablingObjective_Category) }).ToListAsync()).LastOrDefault()?.Number ?? 0;
            // var eoNumber = await GetCategoryNumberAsync();
            // options.Number = eoNumber;
            var checkCat = await _enablingObjective_CategoryService.FindQuery(x => x.Number == options.Number).FirstOrDefaultAsync();
            if (checkCat == null)
            {
                var eoCat = new EnablingObjective_Category(options.Description, options.Number, options.Title, options.EffectiveDate);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eoCat, EnablingObjective_CategoryOperations.Create);
                if (result.Succeeded)
                {
                    eoCat.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    eoCat.CreatedDate = DateTime.Now;
                    var validationResult = await _enablingObjective_CategoryService.AddAsync(eoCat);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return eoCat;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Number Already In Use"]);
            }
        }

        public async Task<EnablingObjective_Category> UpdateCategoryAsync(int id, EnablingObjective_CategoryOptions options)
        {
            var eoCat = await _enablingObjective_CategoryService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (eoCat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFoundException"]);
            }
            else
            {
                eoCat.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                eoCat.ModifiedDate = DateTime.Now;
                eoCat.Description = options.Description;
                eoCat.Number = options.Number;
                eoCat.Title = options.Title;
                eoCat.EffectiveDate = options.EffectiveDate;
                var numValidation = await _enablingObjective_CategoryService.FindQuery(x => x.Id != eoCat.Id && x.Number == eoCat.Number).AnyAsync();
                if (numValidation == false)
                {
                    var validationResult = await _enablingObjective_CategoryService.UpdateAsync(eoCat);
                    if (validationResult.IsValid)
                    {
                        return eoCat;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["Enabling Objective Category Number in Use"]);
                }
            }
        }

        public async Task<int> GetCategoryNumberAsync()
        {
            var catNumber = (await _enablingObjective_CategoryService.FindQueryWithDeleted(x => true).OrderBy(x => x.Number).Select(x => x.Number).ToListAsync())?.LastOrDefault() ?? 0;
            return catNumber + 1;
        }

        public async Task<List<EnablingObjective_SubCategory>> GetAllSubCategories()
        {
            var subCats = await _enablingObjective_SubCategoryService.AllQuery().ToListAsync();
            return subCats;
        }

        public async Task<List<EnablingObjective_Topic>> GetAllTopics()
        {
            var topics = await _enablingObjective_TopicService.AllQuery().ToListAsync();
            return topics;
        }

        public async Task<List<EnablingObjective>> GetAllSqs()
        {
            var sqs = (await _enablingObjectiveService.FindWithIncludeAsync(x => x.IsSkillQualification, new[] { "EnablingObjective_Topic", "EnablingObjective_Category", "EnablingObjective_SubCategory", "Position_SQs" } )).ToList();
            return sqs;
        }

        public async Task<EnablingObjective_SubCategory> CreateSubCategoryAsync(EnablingObjective_SubCategoryOptions options)
        {
            var checkSubCat = await _enablingObjective_SubCategoryService.FindQuery(x => x.CategoryId == options.CategoryId && options.Number == x.Number).AnyAsync();
            if (checkSubCat == false)
            {
                var eoSubCat = new EnablingObjective_SubCategory(options.Description, options.CategoryId, options.Number, options.Title, options.EffectiveDate);
                eoSubCat.CreateHistory(false, true, options.EffectiveDate, options.Reason);

                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eoSubCat, EnablingObjective_SubCategoryOperations.Create);
                if (result.Succeeded)
                {
                    eoSubCat.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    eoSubCat.CreatedDate = DateTime.Now;
                    var validationResult = await _enablingObjective_SubCategoryService.AddAsync(eoSubCat);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        eoSubCat.EnablingObjectives_Category = await _enablingObjective_CategoryService.GetMinimalEOCatDataById(eoSubCat.CategoryId);
                        return eoSubCat;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Number Already In Use"]);
            }
        }

        public async Task<EnablingObjective_SubCategory> UpdateSubCategoryAsync(int subCatId, EnablingObjective_SubCategoryOptions options)
        {
            var eoSubCat = await _enablingObjective_SubCategoryService.FindQuery(x => x.Id == subCatId).FirstOrDefaultAsync();
            if (eoSubCat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOSubCategoryNotFoundException"]);
            }
            else
            {
                eoSubCat.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                eoSubCat.ModifiedDate = DateTime.Now;
                eoSubCat.Number = options.Number;
                eoSubCat.CategoryId = options.CategoryId;
                eoSubCat.Description = options.Description;
                eoSubCat.EffectiveDate = options.EffectiveDate;
                eoSubCat.Title = options.Title;
                var numCheck = await _enablingObjective_SubCategoryService.FindQuery(x => x.Id != eoSubCat.Id && x.CategoryId == eoSubCat.CategoryId && x.Number == eoSubCat.Number).FirstOrDefaultAsync();
                if (numCheck == null)
                {
                    var validationResult = await _enablingObjective_SubCategoryService.UpdateAsync(eoSubCat);
                    if (validationResult.IsValid)
                    {
                        return eoSubCat;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["Enabling Objective Sub Category Number in Use!"]);
                }
            }
        }

        public async Task<int> GetSubCategoryNumberAsync(int catId)
        {
            var num = (await _enablingObjective_SubCategoryService.FindQueryWithDeleted(x => x.CategoryId == catId).ToListAsync()).LastOrDefault()?.Number ?? 0;
            return num + 1;
        }

        public async Task<EnablingObjective_Topic> CreateTopicAsync(int subcategoryId, EnablingObjective_TopicOptions options)
        {
            var subCat = (await _enablingObjective_SubCategoryService.FindAsync(r => r.Id == subcategoryId)).FirstOrDefault();

            //int eoTopicNumber = (await _enablingObjective_TopicService.FindQueryWithDeleted(r => r.SubCategoryId == subCat.Id).ToListAsync()).LastOrDefault()?.Number ?? 0;
            //options.Number = eoTopicNumber + 1;
            var checkTopic = await _enablingObjective_TopicService.FindQuery(x => x.SubCategoryId == subcategoryId && x.Number == options.Number).FirstOrDefaultAsync();
            if (checkTopic == null)
            {
                var eoTopic = new EnablingObjective_Topic(options.Description, subCat.Id, options.Number, options.Title, options.EffectiveDate);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eoTopic, EnablingObjective_TopicOperations.Create);
                if (result.Succeeded)
                {
                    eoTopic.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    eoTopic.CreatedDate = DateTime.Now;
                    var validationResult = await _enablingObjective_TopicService.AddAsync(eoTopic);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        var sub = await _enablingObjective_SubCategoryService.FindQuery(x => x.Id == eoTopic.SubCategoryId).FirstOrDefaultAsync();
                        var cat = await _enablingObjective_CategoryService.FindQuery(x => x.Id == sub.CategoryId).FirstOrDefaultAsync();
                        eoTopic.EnablingObjectives_SubCategory = sub;
                        eoTopic.EnablingObjectives_SubCategory.EnablingObjectives_Category = cat;
                        return eoTopic;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Number Already In Use"]);
            }
        }

        public async Task<EnablingObjective_Topic> UpdateTopicAsync(int topicId, EnablingObjective_TopicOptions options)
        {
            var topic = await _enablingObjective_TopicService.FindQuery(x => x.Id == topicId).FirstOrDefaultAsync();
            if (topic == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOTopicNotFoundException"]);
            }
            else
            {
                topic.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                topic.ModifiedDate = DateTime.Now;
                topic.Number = options.Number;
                topic.Title = options.Title;
                topic.Description = options.Description;
                topic.EffectiveDate = options.EffectiveDate;
                var numValidation = await _enablingObjective_TopicService.FindQuery(x => x.Id != topic.Id && x.SubCategoryId == topic.SubCategoryId && x.Number == topic.Number).FirstOrDefaultAsync();
                if (numValidation == null)
                {
                    var validationResult = await _enablingObjective_TopicService.UpdateAsync(topic);
                    if (validationResult.IsValid)
                    {
                        return topic;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["Enabling Objective Topic Number In Use"]);
                }
            }
        }

        public async Task<EnablingObjective> DeleteAsync(int eoId)
        {
            var eo = await GetAsync(eoId);
            if (eo != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Delete);
                if (result.Succeeded)
                {
                    // eo.Number = null;
                    eo.Delete();
                    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return eo;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["EnablingObjectiveNotFound"]);
            }
        }

        public async Task<EnablingObjective> DeactivateAsync(int eoId)
        {
            var eo = await _enablingObjectiveService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
            //  var eo = await GetAsync(eoId);
            if (eo != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Delete);
                if (result.Succeeded)
                {
                    eo.Deactivate();
                    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        //await CheckCatActive(eo);
                        return eo;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["EnablingObjectiveNotFound"]);
            }
        }

        public async System.Threading.Tasks.Task CheckTopicActive(EnablingObjective_SubCategory subCat)
        {
            var topics = await _enablingObjective_TopicService.FindQuery(x => x.SubCategoryId == subCat.Id).ToListAsync();
            foreach (var topic in topics)
            {
                var eos = await _enablingObjectiveService.FindQuery(x => x.TopicId == topic.Id).ToListAsync();
                if (!eos.Any(x => x.Active))
                {
                    topic.Deactivate();
                    await _enablingObjective_TopicService.UpdateAsync(topic);
                }
            }
        }

        public async System.Threading.Tasks.Task CheckSubCatActive(EnablingObjective_Category cat)
        {
            var subCats = await _enablingObjective_SubCategoryService.FindQuery(x => x.CategoryId == cat.Id).ToListAsync();
            foreach (var subCat in subCats)
            {
                var eos = await _enablingObjectiveService.FindQuery(x => x.SubCategoryId == subCat.Id).ToListAsync();
                if (!eos.Any(x => x.Active))
                {
                    subCat.Deactivate();
                    await _enablingObjective_SubCategoryService.UpdateAsync(subCat);
                    await CheckTopicActive(subCat);
                }
            }
        }

        public async System.Threading.Tasks.Task CheckCatActive(EnablingObjective eo)
        {
            var cats = await _enablingObjective_CategoryService.FindQuery(x => x.Id == eo.CategoryId).ToListAsync();
            foreach (var cat in cats)
            {
                var eos = await _enablingObjectiveService.FindQuery(x => x.CategoryId == eo.CategoryId).ToListAsync();
                if (!eos.Any(x => x.Active))
                {
                    cat.Deactivate();
                    await _enablingObjective_CategoryService.UpdateAsync(cat);
                    await CheckSubCatActive(cat);
                }
            }
        }

        public async Task<EnablingObjective> ActivateAsync(int eoId)
        {
            var eo = await _enablingObjectiveService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
            //  var eo = await GetAsync(eoId);
            if (eo != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Delete);
                if (result.Succeeded)
                {
                    eo.Activate();
                    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return eo;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["EnablingObjectiveNotFound"]);
            }
        }

        public async Task<List<EnablingObjective_Category>> GetAsync()
        {
            var mycat = await _enablingObjective_CategoryService.GetMinimalEOCatData();
            var mySubCats = await _enablingObjective_SubCategoryService.GetMinimalEOSubCatData();
            var topics = await _enablingObjective_TopicService.GetMinimalEOTopicData();
            var eos = await _enablingObjectiveService.GetCompactedEOData();
            //foreach(var cat in mycat)
            //{
            //    foreach(var subCats in cat.EnablingObjective_SubCategories)
            //    {

            //    }
            //}
            //mycat = mycat.OrderBy(c =>
            //{
            //    c.EnablingObjective_SubCategories = c.EnablingObjective_SubCategories.OrderBy(s =>
            //    {
            //        s.EnablingObjectives = s.EnablingObjectives.OrderBy(eos =>
            //        {
            //            if (eos.Number.Contains("."))
            //            {
            //                return Int64.Parse(eos.Number.Split('.')[3]);
            //            }
            //            else
            //            {
            //                return Int64.Parse(eos.Number);
            //            }
            //        }).ToList();
            //        s.EnablingObjective_Topics = s.EnablingObjective_Topics.OrderBy(t =>
            //        {
            //            t.EnablingObjectives = t.EnablingObjectives.OrderBy(eo =>
            //            {
            //                if (eo.Number.Contains("."))
            //                {
            //                    return Int64.Parse(eo.Number.Split('.')[3]);
            //                }
            //                else
            //                {
            //                    return Int64.Parse(eo.Number);
            //                }
            //            }).ToList();
            //            return t.Number;
            //        }).ToList();
            //        return s.Number;
            //    }).ToList();
            //    return c.Number;
            //}).ToList();
            mycat = mycat.OrderBy(o => o.Number).ToList();
            for (int i = 0; i < mycat.Count; i++)
            {
                mycat[i].EnablingObjective_SubCategories = mySubCats.Where(w => w.CategoryId == mycat[i].Id).OrderBy(o => o.Number).ToList();
                for (int j = 0; j < mycat[i].EnablingObjective_SubCategories.Count; j++)
                {
                    mycat[i].EnablingObjective_SubCategories.ToList()[j].EnablingObjectives = eos.Where(w => w.CategoryId == mycat[i].Id && w.TopicId == null && w.SubCategoryId == mycat[i].EnablingObjective_SubCategories.ToList()[j].Id).OrderBy(eo =>
                    {
                        if (long.TryParse(eo.Number, out long number))
                        {
                            return number;
                        }
                        else if (eo.Number.Contains("."))
                        {
                            string[] parts = eo.Number.Split('.');
                            if (parts.Length > 3 && long.TryParse(parts[3], out long subNumber))
                            {
                                return subNumber;
                            }
                        }

                        // Handle cases where eo.Number is not in a valid numeric format.
                        // You can return a default value, throw an exception, or handle it based on your requirement.
                        return 0;
                    }).ToList();
                    mycat[i].EnablingObjective_SubCategories.ToList()[j].EnablingObjective_Topics = topics.Where(w => w.SubCategoryId == mycat[i].EnablingObjective_SubCategories.ToList()[j].Id).OrderBy(o => o.Number).ToList();
                    for (int k = 0; k < mycat[i].EnablingObjective_SubCategories.ToList()[j].EnablingObjective_Topics.Count; k++)
                    {
                        mycat[i].EnablingObjective_SubCategories.ToList()[j].EnablingObjective_Topics.ToList()[k].EnablingObjectives = eos.Where(w => w.SubCategoryId == mycat[i].EnablingObjective_SubCategories.ToList()[j].Id && w.CategoryId == mycat[i].Id && w.TopicId == mycat[i].EnablingObjective_SubCategories.ToList()[j].EnablingObjective_Topics.ToList()[k].Id).OrderBy(eo =>
                        {
                            if (long.TryParse(eo.Number, out long number))
                            {
                                return number;
                            }
                            else if (eo.Number.Contains("."))
                            {
                                string[] parts = eo.Number.Split('.');
                                if (parts.Length > 3 && long.TryParse(parts[3], out long subNumber))
                                {
                                    return subNumber;
                                }
                            }

                            // Handle cases where eo.Number is not in a valid numeric format.
                            // You can return a default value, throw an exception, or handle it based on your requirement.
                            return 0;
                        }).ToList();
                    }
                }
            }
            mycat = mycat.Where(da => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, da, EnablingObjective_CategoryOperations.Read).Result.Succeeded).ToList();
            return mycat;
        }


        public async Task<List<EOCatTreeVM>> GetMinimalDataForTreeAsync()
        {
            var mycats = (await _enablingObjective_CategoryService.GetMinimalEOCatData()).OrderBy(o => o.Number).ToList();
            var mySubCats = await _enablingObjective_SubCategoryService.GetMinimalEOSubCatData();
            var topics = await _enablingObjective_TopicService.GetMinimalEOTopicData();
            var eos = await _enablingObjectiveService.GetMinimalEOData();

            List<EOCatTreeVM> toReturnVM = new List<EOCatTreeVM>();

            foreach (var mycat in mycats)
            {
                EOCatTreeVM cat = new EOCatTreeVM
                {
                    Active = mycat.Active,
                    Id = mycat.Id,
                    Number = mycat.Number,
                    Title = mycat.Title,
                    EnablingObjective_SubCategories = mySubCats.Where(w => w.CategoryId == mycat.Id).OrderBy(o => o.Number).Select(sub => new EOSubCatTreeVM
                    {
                        Active = sub.Active,
                        Id = sub.Id,
                        Title = sub.Title,
                        Number = sub.Number,
                        EnablingObjectives = eos.Where(w => w.CategoryId == mycat.Id && w.SubCategoryId == sub.Id && w.TopicId == null).OrderBy(eo =>
                        {
                            if (long.TryParse(eo.Number, out long number))
                            {
                                return number;
                            }
                            else if (eo.Number.Contains("."))
                            {
                                string[] parts = eo.Number.Split('.');
                                if (parts.Length > 3 && long.TryParse(parts[3], out long subNumber))
                                {
                                    return subNumber;
                                }
                            }

                            // Handle cases where eo.Number is not in a valid numeric format.
                            // You can return a default value, throw an exception, or handle it based on your requirement.
                            return 0;
                        }).Select(eo => new EOTreeVM
                        {
                            Active = eo.Active,
                            Description = eo.Description,
                            Id = eo.Id,
                            IsMeta = eo.isMetaEO,
                            IsSkillQualification = eo.IsSkillQualification,
                            Number = eo.Number
                        }).ToList(),
                        EnablingObjective_Topics = topics.Where(w => w.SubCategoryId == sub.Id).OrderBy(o => o.Number).Select(top => new EOTopicTreeVM
                        {
                            Active = top.Active,
                            Id = top.Id,
                            Number = top.Number,
                            Title = top.Title,
                            EnablingObjectives = eos.Where(w => w.CategoryId == mycat.Id && w.SubCategoryId == sub.Id && w.TopicId == top.Id).OrderBy(eo =>
                            {
                                if (long.TryParse(eo.Number, out long number))
                                {
                                    return number;
                                }
                                else if (eo.Number.Contains("."))
                                {
                                    string[] parts = eo.Number.Split('.');
                                    if (parts.Length > 3 && long.TryParse(parts[3], out long subNumber))
                                    {
                                        return subNumber;
                                    }
                                }

                                // Handle cases where eo.Number is not in a valid numeric format.
                                // You can return a default value, throw an exception, or handle it based on your requirement.
                                return 0;
                            }).Select(eo => new EOTreeVM
                            {
                                Active = eo.Active,
                                Description = eo.Description,
                                Id = eo.Id,
                                IsMeta = eo.isMetaEO,
                                IsSkillQualification = eo.IsSkillQualification,
                                Number = eo.Number
                            }).ToList(),
                        }).ToList(),
                    }).ToList(),
                };

                toReturnVM.Add(cat);
            }

            return toReturnVM;
        }

        public async Task<List<EnablingObjective_Category>> GetSQAsync()
        {
            var mycat = await _enablingObjective_CategoryService.AllQueryWithInclude(new string[] { "EnablingObjective_SubCategories.EnablingObjective_Topics.EnablingObjectives" }, true).IgnoreAutoIncludes().ToListAsync();
            foreach (var cat in mycat)
            {
                foreach (var subcat in cat.EnablingObjective_SubCategories)
                {
                    var eos = await _enablingObjectiveService.FindQuery(x => x.SubCategoryId == subcat.Id && x.TopicId == null && x.CategoryId == cat.Id && x.IsSkillQualification).ToListAsync();
                    subcat.EnablingObjectives = eos;
                }
            }
            mycat = mycat.Where(da => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, da, EnablingObjective_CategoryOperations.Read).Result.Succeeded).ToList();
            return mycat;
        }

        public async Task<List<EOCategoryVM>> GetSimplifiedCategories(string order)
        {
            var mycat = await _enablingObjective_CategoryService.FindQueryAsync(x => x.Active == true);
            var categories = await mycat.Select(x => new EOCategoryVM
            {
                Id = x.Id,
                Number = x.Number,
                Title = x.Title,
                Active = x.Active
            }).
            OrderBy(s=>s.Number)
            .ToListAsync();
            return categories;

        }

        public async Task<List<EnablingObjective>> GetEOAsync()
        {
            var eos = await _enablingObjectiveService.AllQuery().ToListAsync();

            // eos = eos.Where(e => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, e, EnablingObjectiveOperations.Read).Result.Succeeded);
            return eos?.ToList();
        }

        public async Task<string> GetEONumberWithTopicAsync(int selectedCatId, int selectedSubCatId, int selectedTopicId)
        {
            var currNum = (await _enablingObjectiveService.FindQueryWithDeleted(x => x.CategoryId == selectedCatId && x.SubCategoryId == selectedSubCatId && x.TopicId == selectedTopicId).ToListAsync()).Count;
            var catNum = (await _enablingObjective_CategoryService.FindQuery(x => x.Id == selectedCatId).Select(x => x.Number).ToListAsync()).LastOrDefault();
            var subCatNum = (await _enablingObjective_SubCategoryService.FindQuery(x => x.Id == selectedSubCatId).ToListAsync()).LastOrDefault()?.Number ?? 0;
            var topicNum = (await _enablingObjective_TopicService.FindQuery(x => x.Id == selectedTopicId).ToListAsync()).LastOrDefault()?.Number ?? 0;
            string eoNumber = null; eoNumber = catNum.ToString() + "." + subCatNum.ToString() + "." + topicNum + "." + (currNum + 1);
            var num = await GetEONumber(selectedCatId, selectedSubCatId, selectedTopicId, eoNumber);
            return num;
        }

        public async Task<string> GetEONumberWithoutTopicAsync(int selectedCatId, int selectedSubCatId)
        {
            //var eos = await _enablingObjectiveService.AllQuery().Where(x => x.CategoryId == selectedCatId && x.SubCategoryId == selectedSubCatId && x.TopicId == null).ToListAsync();
            //int eoId = 1;
            //string eoNumber = null;
            //if (eos.Count == 0)
            //{
            //    eoNumber = selectedCatId.ToString() + "." + selectedSubCatId.ToString() + "." + eoId;
            //}
            //else
            //{
            //    eoId = eos.Count + 1;
            //    eoNumber = selectedCatId.ToString() + "." + selectedSubCatId.ToString() + "." + eoId;
            //}
            var num = await GetEONumber(selectedCatId, selectedSubCatId, null, "");
            return num;
        }

        public async Task<bool> CheckIsMetaAsync(int id)
        {
            var isMeta = await _enablingObjectiveService.FindQuery(x => x.Id == id).Select(x => x.isMetaEO).FirstOrDefaultAsync();
            return isMeta;
        }

        public async Task<List<EnablingObjective_Category>> GetCategoryWithSubCategoryAsync()
        {
            var eoCatList = await _enablingObjective_CategoryService.AllAsync();
            eoCatList = eoCatList.Where(eo => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Read).Result.Succeeded);
            return eoCatList?.ToList();
        }

        public async Task<EnablingObjective> GetAsync(int eoId)
        {
            var eo = (await _enablingObjectiveService.FindAsync(x => x.Id == eoId)).FirstOrDefault();
            if (eo != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Read);
                if (result.Succeeded)
                {
                    return eo;
                }
                else
                {
                    throw new System.UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return eo;
        }

        public async Task<List<EnablingObjective_Category>> GetCategoriesAsync()
        {
            var eoCatList = await _enablingObjective_CategoryService.GetMinimalEOCatData();
            eoCatList = eoCatList.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EnablingObjective_CategoryOperations.Read).Result.Succeeded).ToList();
            return eoCatList.OrderBy(o => o.Number)?.ToList();
        }

        public async Task<string> GetCategoryIdForTopic(int subCatId)
        {
            var subCat = await _enablingObjective_SubCategoryService.FindQuery(x => x.Id == subCatId).Select(x => x.CategoryId).FirstOrDefaultAsync();
            var hashedId = _hasher.Encode(subCat.ToString());
            return hashedId;
        }

        public async System.Threading.Tasks.Task LinkPositions(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.Position_SQs) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFoundException"]);
            }
            else
            {
                foreach (var posId in options.PositionIds)
                {
                    var pos = await _posService.FindQuery(x => x.Id == posId).FirstOrDefaultAsync();
                    eo.LinkPositions(pos);
                    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task LinkEmployee(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.EnablingObjective_Employee_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFoundException"]);
            }
            else
            {
                foreach (var eoId in options.EmployeeIds)
                {
                    var emp = await _eo_empService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
                    eo.LinkEmployee(emp, options.StartDate);
                    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkEmployee(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.EnablingObjective_Employee_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFoundException"]);
            }
            else
            {
                foreach (var eoId in options.EmployeeIds)
                {
                    var emp = await _eo_empService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
                    eo.UnlinkEmployee(emp);
                    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }
        }

        /// Use this when to get employee data with count 
        public async Task<List<EmployeeWithCountOptions>> GetLinkedEmployees(int eoId)
        {
            var links = await _eo_emp_linkService.FindQueryWithIncludeAsync(x => x.EOID == eoId, new string[] { nameof(_eo_emp.Employee), "Employee.Person", "Employee.EmployeePositions.Position" }).
                Select(s => new EmployeeWithCountOptions( 
                    //id: s.Employee.Id, active: s.Employee.Active, email: s.Employee.Person.Username, image: s.Employee.Person.Image, number: s.Employee.EmployeeNumber, position: s.Employee.EmployeePositions.LastOrDefault(), description: , linkCount: s.Count()
                    s.Employee.EmployeeNumber,
                    s.Employee.Person.FirstName + ' ' + s.Employee.Person.LastName,
                    s.Employee.Id,
                    s.Employee.EnablingObjective_Employee_Links.Count(),
                    s.Employee.Active,
                    s.Employee.EmployeePositions.OrderBy(x=>x.Id).LastOrDefault(),
                    s.Employee.Person.Username,
                    s.Employee.Person.Image
                    )
                ).ToListAsync();
         

            return links;
        }

        public async System.Threading.Tasks.Task UnlinkPositions(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.Position_SQs) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFoundException"]);
            }
            else
            {
                foreach (var posId in options.PositionIds)
                {
                    var pos = await _posService.FindQuery(x => x.Id == posId).FirstOrDefaultAsync();
                    eo.UnlinkPositions(pos);
                    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }
        }

        public async Task<List<TaskPositionWithCount>> GetLinkedPositions(int eoId)
        {
            var sq_position = await _pos_sq_service.FindQueryWithIncludeAsync(x => x.EOId == eoId, new string[] { nameof(_pos_sq.Position) }).ToListAsync();
            var linkedPositions = sq_position.Select(x => x.Position).ToList();

            var posWithCount = new List<TaskPositionWithCount>();
            int count = 0;
            foreach (var position in linkedPositions)
            {
                count = await _pos_sq_service.GetCount(x => x.PositionId == position.Id);
                posWithCount.Add(new TaskPositionWithCount(position, count));
            }
            return posWithCount;
        }

        public async System.Threading.Tasks.Task LinkProcedureAsync(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.Procedure_EnablingObjective_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EnablingObjectiveNotFound"]);
            }
            else
            {
                foreach (var procId in options.ProcedureIds)
                {
                    var procedure = await _procedureService.FindQuery(x => x.Id == procId).FirstOrDefaultAsync();
                    eo.LinkProcedure(procedure, options.EOId);
                    await _enablingObjectiveService.UpdateAsync(eo);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkProcedureAsync(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.Procedure_EnablingObjective_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EnablingObjectiveNotFound"]);
            }
            else
            {
                foreach (var procId in options.ProcedureIds)
                {
                    var procedure = await _procedureService.FindQuery(x => x.Id == procId).FirstOrDefaultAsync();
                    eo.UnlinkProcedure(procedure);
                    await _enablingObjectiveService.UpdateAsync(eo);
                }
            }
        }

        public async Task<List<ProceduresWithLinkCount>> GetProcedureWithLinkCount(int eoId)
        {
            var links = await _proc_eo_linkService.FindWithIncludeAsync(x => x.EnablingObjectiveId == eoId, new string[] { nameof(_proc_eo_link.Procedure) });
            List<Domain.Entities.Core.Procedure> procList = new List<Domain.Entities.Core.Procedure>();
            procList.AddRange(links.Select(x => x.Procedure));

            List<ProceduresWithLinkCount> procWithCount = new List<ProceduresWithLinkCount>();
            foreach (var proc in procList)
            {
                var data = await _proc_eo_linkService.GetCount(x => x.ProcedureId == proc.Id);
                procWithCount.Add(new ProceduresWithLinkCount(proc.Id, data, proc.Number, proc.Title, proc.Active));
            }

            return procWithCount;
        }

        public async Task<List<ProceduresWithLinkCount>> GetLinkedProceduresToMetaEOAsync(int metaId)
        {
            var linkedEos = await _eo_metaEO_linkService.FindQuery(x => x.MetaEOId == metaId).Select(x => x.EOID).ToListAsync();
            List<ProceduresWithLinkCount> procList = new List<ProceduresWithLinkCount>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetProcedureWithLinkCount(eoId);
                procList.AddRange(data);
            }
            return procList.GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public async System.Threading.Tasks.Task LinkSaftyHazardAsync(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.SafetyHazard_EO_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFound"]);
            }
            else
            {
                foreach (var shId in options.SafetyHazardIds)
                {
                    var sh = await _saftyHazardService.FindQuery(x => x.Id == shId).FirstOrDefaultAsync();
                    eo.LinkSaftyHazard(sh, options.EOId);
                    await _enablingObjectiveService.UpdateAsync(eo);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkSaftyHazardAsync(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.SafetyHazard_EO_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFound"]);
            }
            else
            {
                foreach (var shId in options.SafetyHazardIds)
                {
                    var sh = await _saftyHazardService.FindQuery(x => x.Id == shId).FirstOrDefaultAsync();
                    eo.UnlinkSaftyHazard(sh);
                    await _enablingObjectiveService.UpdateAsync(eo);
                }
            }
        }

        public async Task<List<SafetyHazardWithLinkCount>> GetSafetyHazardWithLinkCounts(int eoId)
        {
            var links = await _sh_eo_linkService.FindWithIncludeAsync(x => x.EOID == eoId, new string[] { nameof(_sh_eo_link.SaftyHazard) });
            List<Domain.Entities.Core.SaftyHazard> shList = new List<Domain.Entities.Core.SaftyHazard>();
            shList.AddRange(links.Select(x => x.SaftyHazard));

            List<SafetyHazardWithLinkCount> shWithCount = new List<SafetyHazardWithLinkCount>();
            foreach (var sh in shList)
            {
                var data = await _sh_eo_linkService.GetCount(x => x.SafetyHazardId == sh.Id);
                shWithCount.Add(new SafetyHazardWithLinkCount(sh.Id, sh.Title, sh.Number, data, sh.Active));
            }

            return shWithCount.OrderBy(x=>x.Number).ToList();
        }

        public async Task<List<SafetyHazardWithLinkCount>> GetLinkedSHWithMetaEOAsync(int metaId)
        {
            var linkedEos = await _eo_metaEO_linkService.FindQuery(x => x.MetaEOId == metaId).Select(x => x.EOID).ToListAsync();
            List<SafetyHazardWithLinkCount> shList = new List<SafetyHazardWithLinkCount>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetSafetyHazardWithLinkCounts(eoId);
                shList.AddRange(data);
            }
            return shList.GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public async Task<string> GetEONumber(int catId, int subCatId, int? topicId, string dupNumber)
        {
            string number = "";
            var lastEO = new EnablingObjective();
            var orderedEos = await _enablingObjectiveService.FindQueryWithDeleted(x => x.CategoryId == catId && x.SubCategoryId == subCatId && x.TopicId == topicId).IgnoreQueryFilters().OrderBy(x => x.Number).Select(s => new EnablingObjective { Number = s.Number, Id = s.Id }).ToListAsync();
            EnablingObjective nexttEO = new EnablingObjective();
            if (orderedEos.Count == 0)
            {
                nexttEO.Number = "0";
            }
            foreach (var orderedEO in orderedEos)
            {
                if (nexttEO?.Id == 0)
                {
                    nexttEO = orderedEO;
                }
                else
                {
                    var nextNum = !nexttEO.Number.Contains(".") ? int.Parse(nexttEO.Number) : int.Parse(nexttEO.Number.Split(".")[3]);
                    var currNum = !orderedEO.Number.Contains(".") ? int.Parse(orderedEO.Number) : int.Parse(orderedEO.Number.Split(".")[3]);

                    if (currNum > nextNum)
                    {
                        nexttEO = orderedEO;
                    }
                }
            }
            if (topicId == 0 || topicId == null)
            {
                // var eos = await _enablingObjectiveService.FindQueryWithDeleted(x => x.CategoryId == catId && x.SubCategoryId == subCatId && x.TopicId == topicId).IgnoreQueryFilters().OrderBy(x => x.Number).LastOrDefaultAsync();
                var cat = await _enablingObjective_CategoryService.FindQueryWithIncludeAsync(x => x.Id == catId, new string[] { "EnablingObjective_SubCategories" }).Select(s => new EnablingObjective_Category { Id = s.Id, Number = s.Number, EnablingObjective_SubCategories = s.EnablingObjective_SubCategories }).FirstOrDefaultAsync();
                var subCat = cat.EnablingObjective_SubCategories.Where(x => x.Id == subCatId).FirstOrDefault();
                number = cat.Number + "." + subCat.Number + "." + "0." + (nexttEO?.Id == 0 || !nexttEO.Number.Contains(".") ? int.Parse(nexttEO.Number) + 1 : int.Parse(nexttEO.Number.Split(".")[3]) + 1).ToString();
            }
            else
            {
                //var eos = await _enablingObjectiveService.FindQueryWithDeleted(x => x.CategoryId == catId && x.SubCategoryId == subCatId && x.TopicId == topicId).OrderBy(x => x.Number).LastOrDefaultAsync();
                var cat = await _enablingObjective_CategoryService.FindQueryWithDeleted(x => x.Id == catId).Select(s => new EnablingObjective_Category { Id = s.Id, Number = s.Number }).FirstOrDefaultAsync();
                var subCat = await _enablingObjective_SubCategoryService.FindQueryWithDeleted(x => x.Id == subCatId).Select(s => new EnablingObjective_SubCategory { Id = s.Id, Number = s.Number }).FirstOrDefaultAsync();
                var topic = await _enablingObjective_TopicService.FindQueryWithDeleted(x => x.Id == topicId).Select(s => new EnablingObjective_Topic { Id = s.Id, Number = s.Number }).FirstOrDefaultAsync();
                number = cat.Number + "." + subCat.Number + "." + topic.Number + "." + (nexttEO?.Id == 0 || !nexttEO.Number.Contains(".") ? int.Parse(nexttEO.Number) + 1 : int.Parse(nexttEO.Number.Split(".")[3]) + 1).ToString();
            }

            return number;
        }

        public async Task<EnablingObjective> UpdateAsync(int eoId, EnablingObjectiveCreateOptions options)
        {
            var eo = await _enablingObjectiveService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Update);

            if (result.Succeeded)
            {
                eo.CategoryId = options.CategoryId;
                eo.SubCategoryId = options.SubCategoryId;
                if (options.TopicId == 0)
                {
                    eo.TopicId = null;
                }
                else
                {
                    eo.TopicId = options.TopicId;
                }
                eo.Description = options.Statement;
                eo.Number = options.Number;
                eo.EffectiveDate = options.EffectiveDate;
                eo.isMetaEO = options.isMetaEO;
                eo.SetIsSkillQualification(options.IsSkill);
                eo.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                eo.ModifiedDate = DateTime.Now;
                var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return eo;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<EnablingObjective_Category> GetCategoryAsync(int categoryId)
        {
            var eoCat = await _enablingObjective_CategoryService.GetAsync(categoryId);
            if (eoCat != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eoCat, EnablingObjective_CategoryOperations.Read);
                if (result.Succeeded)
                {
                    return eoCat;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["EO_CatNotFound"]);
            }
        }

        public async Task<int> GetTopicNumberAsync(int catId, int subCatId)
        {
            var topicNumber = (await _enablingObjective_TopicService.FindQueryWithDeleted(x => x.SubCategoryId == subCatId).ToListAsync()).LastOrDefault()?.Number ?? 0;
            return topicNumber + 1;
        }

        public async Task<List<EnablingObjective_Topic>> GetTopicsAsync(int subCategoryId)
        {
            var eoTopics = await _enablingObjective_TopicService.FindAsync(r => r.SubCategoryId == subCategoryId);
            eoTopics = eoTopics.Where(t => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, t, EnablingObjective_TopicOperations.Read).Result.Succeeded).ToList();
            return eoTopics.ToList();
        }

        public async Task<List<EnablingObjective_Topic>> GetTopicsAsync()
        {
            var eoTopics = await _enablingObjective_TopicService.FindAsync(z => z.Deleted == false);
            eoTopics = eoTopics.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EnablingObjective_TopicOperations.Read).Result.Succeeded).ToList();
            return eoTopics?.ToList();
        }

        public async Task<EnablingObjective_Topic> GetTopicAsync(int id)
        {
            var topic = await _enablingObjective_TopicService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            return topic;
        }

        public async Task<List<EnablingObjective_SubCategory>> GetSubCategoriesAsync(int categoryId)
        {
            var eoSubCats = await _enablingObjective_SubCategoryService.FindAsync(x => x.CategoryId == categoryId);
            eoSubCats = eoSubCats.Where(sc => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, sc, EnablingObjective_SubCategoryOperations.Read).Result.Succeeded).ToList();
            return eoSubCats.ToList();
        }

        public async Task<EnablingObjective_SubCategory> GetSubCategoryAsync(int subCatId)
        {
            var subCat = await _enablingObjective_SubCategoryService.FindQuery(x => x.Id == subCatId).FirstOrDefaultAsync();
            return subCat;
        }

        public async System.Threading.Tasks.Task LinkTaskAsync(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.Task_EnablingObjective_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EnablingObjectiveNotFound"]);
            }
            else
            {
                foreach (var taskId in options.TaskIds)
                {
                    var task = await _taskService.FindQuery(x => x.Id == taskId).FirstOrDefaultAsync();
                    eo.LinkTask(task);
                    await _enablingObjectiveService.UpdateAsync(eo);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkTasksAsync(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.Task_EnablingObjective_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EnablingObjectiveNotFound"]);
            }
            else
            {
                foreach (var taskId in options.TaskIds)
                {
                    var task = await _taskService.FindQuery(x => x.Id == taskId).FirstOrDefaultAsync();
                    eo.UnlinkTask(task);
                    await _enablingObjectiveService.UpdateAsync(eo);
                }
            }
        }

        public async Task<List<TaskWithCountOptions>> GetLinkedTasksWithCountAsync(int eoId)
        {
            var links = await _task_eo_linkService.FindWithIncludeAsync(x => x.EnablingObjectiveId == eoId, new string[] { nameof(_task_eo_link.Task) });
            List<Domain.Entities.Core.Task> taskList = new List<Domain.Entities.Core.Task>();
            taskList.AddRange(links.Select(x => x.Task));

            List<TaskWithCountOptions> taskWithCount = new List<TaskWithCountOptions>();
            foreach (var task in taskList)
            {
                var data = await _task_eo_linkService.GetCount(x => x.TaskId == task.Id);
                var taskNumber = await _taskService.FindQueryWithIncludeAsync(x => x.Id == task.Id, new string[] { "SubdutyArea.DutyArea" }).FirstOrDefaultAsync();

                var num = taskNumber.SubdutyArea.DutyArea.Letter + taskNumber.SubdutyArea.DutyArea.Number.ToString() + '.' + taskNumber.SubdutyArea.SubNumber.ToString() + '.' + task.Number.ToString();
                //var taskGroups = await _taskTrainingGroupService.GetCount(x => x.TaskId == task.Id);
                taskWithCount.Add(new TaskWithCountOptions(num, task.Description, task.Id, data, task.Active, 0));
            }

            return taskWithCount;
        }

        public async Task<List<TaskWithCountOptions>> GetLinkedTaskWithMetaEOs(int metaId)
        {
            var linkedEoIds = await _eo_metaEO_linkService.FindQuery(x => x.MetaEOId == metaId).Select(x => x.EOID).ToListAsync();
            List<TaskWithCountOptions> withCount = new List<TaskWithCountOptions>();
            foreach (var eoId in linkedEoIds)
            {
                var data = await GetLinkedTasksWithCountAsync(eoId);
                withCount.AddRange(data);
            }
            //withCount = withCount.GroupBy(x => x.Id).Select(s => s.First()).ToList();
            return withCount.GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public async System.Threading.Tasks.Task linkRegReqAsync(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.RegRequirement_EO_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFound"]);
            }
            else
            {
                foreach (var rrId in options.RRIds)
                {
                    var rr = await _rr_Service.FindQuery(x => x.Id == rrId).FirstOrDefaultAsync();
                    eo.LinkRR(rr);
                    await _enablingObjectiveService.UpdateAsync(eo);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkRRAsync(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.RegRequirement_EO_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFound"]);
            }
            else
            {
                foreach (var rrId in options.RRIds)
                {
                    var rr = await _rr_Service.FindQuery(x => x.Id == rrId).FirstOrDefaultAsync();
                    eo.UnlinkRR(rr);
                    await _enablingObjectiveService.UpdateAsync(eo);
                }
            }
        }

        public async System.Threading.Tasks.Task LinkILAAsync(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.ILA_EnablingObjective_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFound"]);
            }
            else
            {
                foreach (var ilaId in options.IlaIds)
                {
                    var ila = await _ilaService.FindQuery(x => x.Id == ilaId).FirstOrDefaultAsync();
                    eo.LinkILA(ila);
                    await _enablingObjectiveService.UpdateAsync(eo);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkILAAsync(EO_LinkOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.EOId, new string[] { nameof(_eo.ILA_EnablingObjective_Links) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFound"]);
            }
            else
            {
                foreach (var ilaId in options.IlaIds)
                {
                    var ila = await _ilaService.FindQuery(x => x.Id == ilaId).FirstOrDefaultAsync();
                    eo.UnlinkILA(ila);
                    await _enablingObjectiveService.UpdateAsync(eo);
                }
            }
        }

        public async Task<List<ILAWithCountOptions>> GetILAWithLinkCount(int eoId)
        {
            var links = await _ila_eo_linkService.FindWithIncludeAsync(x => x.EnablingObjectiveId == eoId, new string[] { nameof(_ila_eo_link.ILA) });
            List<Domain.Entities.Core.ILA> ilaList = new List<Domain.Entities.Core.ILA>();
            ilaList.AddRange(links.Select(x => x.ILA));

            List<ILAWithCountOptions> ilaWithCount = new List<ILAWithCountOptions>();
            foreach (var ila in ilaList)
            {
                var data = await _ila_eo_linkService.GetCount(x => x.ILAId == ila.Id);
                ilaWithCount.Add(new ILAWithCountOptions(ila.Number, ila.Name, ila.Id, data, ila.Active));
            }

            return ilaWithCount;
        }

        public async Task<List<ILAWithCountOptions>> GetLinkedILAToMetaEOWithCountAsync(int metaId)
        {
            var linkedEos = await _eo_metaEO_linkService.FindQuery(x => x.MetaEOId == metaId).Select(x => x.EOID).ToListAsync();
            List<ILAWithCountOptions> ilaList = new List<ILAWithCountOptions>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetILAWithLinkCount(eoId);
                ilaList.AddRange(data);
            }
            return ilaList;
        }

        public async Task<List<RegulatoryRequirementWithLinkCount>> GetLinkedRRWithCount(int eoId)
        {
            var links = await _rr_eo_linkService.FindQueryWithIncludeAsync(x => x.EOID == eoId, new string[] { nameof(_rr_eo_link.RegulatoryRequirement) }).ToListAsync();
            List<Domain.Entities.Core.RegulatoryRequirement> rrList = new List<Domain.Entities.Core.RegulatoryRequirement>();
            rrList.AddRange(links.Select(x => x.RegulatoryRequirement));

            List<RegulatoryRequirementWithLinkCount> rrWithCount = new List<RegulatoryRequirementWithLinkCount>();
            int count = 0;
            foreach (var item in rrList)
            {
                count = await _rr_eo_linkService.GetCount(x => x.RegulatoryRequirementId == item.Id);
                rrWithCount.Add(new RegulatoryRequirementWithLinkCount(item.Number, item.Title, item.Id, count, item.Active));
            }

            return rrWithCount;
        }

        public async Task<List<RegulatoryRequirementWithLinkCount>> GetLinkedRRWithMetaEOAsync(int metaId)
        {
            var linkedEos = await _eo_metaEO_linkService.FindQuery(x => x.MetaEOId == metaId).Select(x => x.EOID).ToListAsync();
            List<RegulatoryRequirementWithLinkCount> rrList = new List<RegulatoryRequirementWithLinkCount>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetLinkedRRWithCount(eoId);
                rrList.AddRange(data);
            }
            return rrList.GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public async System.Threading.Tasks.Task DeleteCategoryAsync(int id)
        {
            var cat = await _enablingObjective_CategoryService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOCategoryNotFoundException"]);
            }
            else
            {
                cat.Delete();
                await _enablingObjective_CategoryService.UpdateAsync(cat);
            }
        }

        public async System.Threading.Tasks.Task MakeInactiveCategoryAsync(int id)
        {
            var cat = await _enablingObjective_CategoryService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOCategoryNotFoundException"]);
            }
            else
            {
                cat.Deactivate();
                await _enablingObjective_CategoryService.UpdateAsync(cat);
                await CheckSubCatActive(cat);
            }
        }

        public async System.Threading.Tasks.Task MakeActiveCategoryAsync(int id)
        {
            var cat = await _enablingObjective_CategoryService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOCategoryNotFoundException"]);
            }
            else
            {
                cat.Activate();
                await _enablingObjective_CategoryService.UpdateAsync(cat);
            }
        }

        public async System.Threading.Tasks.Task DeleteSubCategoryAsync(int id)
        {
            var cat = await _enablingObjective_SubCategoryService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOSubCategoryNotFoundException"]);
            }
            else
            {
                cat.Delete();
                await _enablingObjective_SubCategoryService.UpdateAsync(cat);
            }
        }

        public async System.Threading.Tasks.Task MakeInactiveSubCategoryAsync(int id)
        {
            var cat = await _enablingObjective_SubCategoryService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOSubCategoryNotFoundException"]);
            }
            else
            {
                cat.Deactivate();
                await _enablingObjective_SubCategoryService.UpdateAsync(cat);
                await CheckTopicActive(cat);
            }
        }

        public async System.Threading.Tasks.Task MakeActiveSubCategoryAsync(int id)
        {
            var cat = await _enablingObjective_SubCategoryService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOSubCategoryNotFoundException"]);
            }
            else
            {
                cat.Activate();
                await _enablingObjective_SubCategoryService.UpdateAsync(cat);
            }
        }

        public async System.Threading.Tasks.Task DeleteTopicAsync(int id)
        {
            var cat = await _enablingObjective_TopicService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOTopicNotFoundException"]);
            }
            else
            {
                cat.Delete();
                await _enablingObjective_TopicService.UpdateAsync(cat);
            }
        }

        public async System.Threading.Tasks.Task MakeInactiveTopicAsync(int id)
        {
            var cat = await _enablingObjective_TopicService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOTopicNotFoundException"]);
            }
            else
            {
                cat.Deactivate();
                await _enablingObjective_TopicService.UpdateAsync(cat);
            }
        }

        public async System.Threading.Tasks.Task MakeActiveTopicAsync(int id)
        {
            var cat = await _enablingObjective_TopicService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cat == null)
            {
                throw new BadHttpRequestException(message: _localizer["EOTopicNotFoundException"]);
            }
            else
            {
                cat.Activate();
                await _enablingObjective_TopicService.UpdateAsync(cat);
            }
        }

        public async Task<EnablingObjectiveVM> CopyEOWithLinkages(int id, EnablingObjectiveCreateOptions options)
        {
            var eolist = (await _enablingObjectiveService.FindWithIncludeAsync(r => r.CategoryId == options.CategoryId
           && r.SubCategoryId == options.SubCategoryId, new[] { "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category" })).ToList();

            if (options.TopicId != null)
            {
                var validateOptions = eolist.Where(r => r.Number == options.Number && r.TopicId == options.TopicId).FirstOrDefault();
                if (validateOptions != null)
                {
                    throw new BadHttpRequestException(message: _localizer["EnablingObjective Number Already In Use"]);
                }
            }
            else
            {
                var validateOptions = eolist.Where(r => r.Number == options.Number && r.TopicId.GetValueOrDefault() == 0).FirstOrDefault();
                if (validateOptions != null)
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            var linkToCopy = await _enablingObjectiveService.GetForCopy(id);
            var currentUser = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            var eo = linkToCopy.Copy<EnablingObjective>(currentUser.Id);
            eo.UpdateEO(options.Number,options.Statement,options.isMetaEO,options.IsSkill,options.EffectiveDate);
            var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Create);
            if (!authResult.Succeeded)
            {
                throw new UnauthorizedAccessException(_localizer["OperationNotAllowed"]);
            }

            var validationResult = await _enablingObjectiveService.AddAsync(eo);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(string.Join(',', validationResult.Errors));
            }
            return new EnablingObjectiveVM(eo.Id, eo.Number, eo.Description);
        }
        public async Task<EOStatsCount> GetEOLinkedStats(int eoId)
        {
            var stats = new EOStatsCount();
            stats.Tasks = await _task_eo_linkService.GetCount(x => x.EnablingObjectiveId == eoId);
            stats.Procedures = await _proc_eo_linkService.GetCount(x => x.EnablingObjectiveId == eoId);
            stats.ILAs = await _ila_eo_linkService.GetCount(x => x.EnablingObjectiveId == eoId);
            stats.SafetyHazards = await _sh_eo_linkService.GetCount(x => x.EOID == eoId);
            stats.RRs = await _rr_eo_linkService.GetCount(x => x.EOID == eoId);
            stats.TestQuestions = await _testItemService.GetCount(x => x.EOId == eoId);
            stats.MetaEOs = await _eo_metaEO_linkService.GetCount(x => x.MetaEOId == eoId);
            stats.Positions = await _pos_sq_service.GetCount(x => x.EOId == eoId);
            stats.Employees = await _eo_emp_linkService.GetCount(x => x.EOID == eoId);
            return stats;
        }

        public async Task<EOStatsCount> GetMetaEOLinkedStatsAsync(int metaId)
        {
            var stats = new EOStatsCount
            {
                Tasks = 0,
                Procedures = 0,
                ILAs = 0,
                SafetyHazards = 0,
                RRs = 0,
                TestQuestions = 0,
                MetaEOs = 0,
                Employees = 0,
                Positions = 0,
            };
            //var linkedEOIds = await _eo_metaEO_linkService.FindQuery(x => x.MetaEOId == metaId).Select(x => x.EOID).ToListAsync();
            //foreach(var eoId in linkedEOIds)
            //{
            //    var data = await GetEOLinkedStats(eoId);
            //    stats.Tasks += data.Tasks;
            //    stats.Procedures += data.Procedures;
            //    stats.ILAs += data.ILAs;
            //    stats.SafetyHazards += data.SafetyHazards;
            //    stats.RRs += data.RRs;
            //    stats.TestQuestions += data.TestQuestions;
            //    stats.MetaEOs += data.MetaEOs;
            //    stats.Positions += data.Positions;
            //    stats.Employees += data.Employees;
            //}

            stats.Tasks = (await GetLinkedTaskWithMetaEOs(metaId)).Count;
            stats.Procedures = (await GetLinkedProceduresToMetaEOAsync(metaId)).Count;
            stats.ILAs = (await GetLinkedILAToMetaEOWithCountAsync(metaId)).Count;
            stats.SafetyHazards = (await GetLinkedSHWithMetaEOAsync(metaId)).Count;
            stats.RRs = (await GetLinkedRRWithMetaEOAsync(metaId)).Count;
            stats.TestQuestions = (await GetLinkedTestItemWithMetaEOAsync(metaId)).Count;
            //stats.Positions = (await GetLinkedPositionsWithMetaEOAsync(metaId)).Count;
            //stats.Employees = (await GetLinkedEmployees(metaId)).Count;
            return stats;
        }

        public async Task<EOStatsCount> GetEONotLinkedStats()
        {
            var eoIds = await _enablingObjectiveService.AllQuery().Where(s => s.Active == true).Select(x => x.Id).ToListAsync();
            var eoTaskIds = await _task_eo_linkService.AllQuery().Select(x => x.EnablingObjectiveId).Distinct().ToListAsync();
            var eoProcIds = await _proc_eo_linkService.AllQuery().Select(x => x.EnablingObjectiveId).Distinct().ToListAsync();
            var eoSHIds = await _sh_eo_linkService.AllQuery().Select(x => x.EOID).Distinct().ToListAsync();
            var eoRRIds = await _rr_eo_linkService.AllQuery().Select(x => x.EOID).Distinct().ToListAsync();
            var eoILAIds = await _ila_eo_linkService.AllQuery().Select(x => x.EnablingObjectiveId).Distinct().ToListAsync();
            var eoTestIds = await _testItemService.GetCount(x => x.EOId == null);


            var stats = new EOStatsCount()
            {
                ActiveEO = await _enablingObjectiveService.GetCount(x => x.Active == true),
                InActiveEO = await _enablingObjectiveService.GetCount(x => x.Active == false),
                TestQuestions = eoTestIds,
                Procedures = eoIds.Except(eoProcIds).Count(),
                ILAs = eoIds.Except(eoILAIds).Count(),
                RRs = eoIds.Except(eoRRIds).Count(),
                SafetyHazards = eoIds.Except(eoSHIds).Count(),
                Tasks = eoIds.Except(eoTaskIds).Count(),
            };

            return stats;
        }

        public async Task<List<EnablingObjective>> GetLinkedEOsAsync(int id, string type)
        {
            List<EnablingObjective> eoList = new List<EnablingObjective>();
            switch (type.Trim().ToLower())
            {
                case "task":
                    var tasklinkedEos = await _task_eo_linkService.FindQueryWithIncludeAsync(x => x.TaskId == id, new string[] { nameof(_task_eo_link.EnablingObjective) }).Select(x => x.EnablingObjective).ToListAsync();
                    eoList.AddRange(tasklinkedEos);
                    break;
                case "sh":
                    var shlinkedEos = await _sh_eo_linkService.FindQueryWithIncludeAsync(x => x.SafetyHazardId == id, new string[] { nameof(_sh_eo_link.EnablingObjective) }).Select(x => x.EnablingObjective).ToListAsync();
                    eoList.AddRange(shlinkedEos);
                    break;
                case "rr":
                    var rrlinkedEos = await _rr_eo_linkService.FindQueryWithIncludeAsync(x => x.RegulatoryRequirementId == id, new string[] { nameof(_rr_eo_link.EO) }).Select(x => x.EO).ToListAsync();
                    eoList.AddRange(rrlinkedEos);
                    break;
                case "ila":
                    var ilalinkedEos = await _ila_eo_linkService.FindQueryWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(_ila_eo_link.EnablingObjective) }).Select(x => x.EnablingObjective).ToListAsync();
                    eoList.AddRange(ilalinkedEos);
                    break;
                case "proc":
                    var proclinkedEos = await _proc_eo_linkService.FindQueryWithIncludeAsync(x => x.ProcedureId == id, new string[] { nameof(_proc_eo_link.EnablingObjective) }).Select(x => x.EnablingObjective).ToListAsync();
                    eoList.AddRange(proclinkedEos);
                    break;
                case "pos":
                    var poslinkedEOs = await _pos_sq_service.FindQueryWithIncludeAsync(x => x.PositionId == id, new string[] { nameof(_pos_sq.EnablingObjective) }).Select(x => x.EnablingObjective).ToListAsync();
                    eoList.AddRange(poslinkedEOs);
                    break;
                case "emp":
                    var emplinkedEos = await _eo_emp_linkService.FindQueryWithIncludeAsync(x => x.EmployeeId == id, new string[] { nameof(_eo_emp.EnablingObjective) }).Select(x => x.EnablingObjective).ToListAsync();
                    eoList.AddRange(emplinkedEos);
                    break;
            }
            return eoList;
        }

        public async Task<List<string>> GetLinkedIds(string name)
        {
            List<int> eoIds = new List<int>();
            switch (name.Trim().ToLower())
            {
                case "tasks":
                    eoIds = await _task_eo_linkService.AllQuery().Select(x => x.EnablingObjectiveId).ToListAsync();
                    break;
                case "safety hazards":
                    eoIds = await _sh_eo_linkService.AllQuery().Select(x => x.EOID).ToListAsync();
                    break;
                case "regulations":
                    eoIds = await _rr_eo_linkService.AllQuery().Select(x => x.EOID).ToListAsync();
                    break;
                case "procedures":
                    eoIds = await _proc_eo_linkService.AllQuery().Select(x => x.EnablingObjectiveId).ToListAsync();
                    break;
                case "test questions":
                    eoIds = await _testItemService.AllQuery().Where(x => x.EOId != null).Select(x => (int)x.EOId).ToListAsync();
                    break;
                case "ilas":
                    eoIds = await _ila_eo_linkService.AllQuery().Select(x => x.EnablingObjectiveId).ToListAsync();
                    break;
            }
            eoIds = eoIds.Distinct().ToList();

            var hashedIds = new List<string>();
            eoIds.ForEach(item =>
            {
                hashedIds.Add(_hasher.Encode(item.ToString()));
            });
            return hashedIds;
        }

        public async Task<EOWithAllDataVM> GetAllEODataAsync(int eoId)
        {
            var allData = new EOWithAllDataVM();
            allData.IlasWithCount = await GetILAWithLinkCount(eoId);
            allData.ProceduresWithCount = await GetProcedureWithLinkCount(eoId);
            allData.TasksWithCount = await GetLinkedTasksWithCountAsync(eoId);
            allData.SHsWithCount = await GetSafetyHazardWithLinkCounts(eoId);
            allData.RRsWithCount = await GetLinkedRRWithCount(eoId);
            allData.TIsWithCount = await GetLinkedTestItemsWithCountAsync(eoId);
            allData.PositionsWithCount = await GetLinkedPositions(eoId);
            allData.EmployeesWithCount = await GetLinkedEmployees(eoId);
            return allData;
        }

        public async Task<EnablingObjective> GetEOWithCatSubCatAndTopicAsync(int eoId)
        {
            var eo = await _enablingObjectiveService.FindQuery(x => x.Id == eoId, true).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFoundException"]);
            }
            else
            {
                eo.EnablingObjective_Category = await _enablingObjective_CategoryService.FindQuery(x => x.Id == eo.CategoryId, true).FirstOrDefaultAsync();
                eo.EnablingObjective_SubCategory = await _enablingObjective_SubCategoryService.FindQuery(x => x.Id == eo.SubCategoryId, true).FirstOrDefaultAsync();
                if (eo.TopicId.GetValueOrDefault() != 0)
                {
                    eo.EnablingObjective_Topic = await _enablingObjective_TopicService.FindQuery(x => x.Id == eo.TopicId, true).FirstOrDefaultAsync();
                }

                return eo;
            }
        }

        public async Task<List<TestItem>> GetLinkedTestItemsAsync(int eoId)
        {
            var testItems = await _testItemService.FindQuery(x => x.EOId == eoId).ToListAsync();
            return testItems;
        }

        public async Task<List<TestItemWithTestCount>> GetLinkedTestItemsWithCountAsync(int eoId)
        {
            var links = await _testItemService.FindWithIncludeAsync(x => x.EOId == eoId, new string[] { nameof(_testItem.EnablingObjective) });
            List<Domain.Entities.Core.TestItem> testItemList = new List<Domain.Entities.Core.TestItem>();
            testItemList.AddRange(links);
            int number = 1;
            List<TestItemWithTestCount> testWithCount = new List<TestItemWithTestCount>();
            foreach (var testItem in testItemList)
            {
                var data = await _test_item_linkService.GetCount(x => x.TestItemId == testItem.Id);
                testWithCount.Add(new TestItemWithTestCount(testItem.Id, testItem.Description, testItem.Active, data, testItem.Number));
                number = number + 1;
            }

            return testWithCount;
        }

        public async Task<List<TestItemWithTestCount>> GetLinkedTestItemWithMetaEOAsync(int metaId)
        {
            var linkedEos = await _eo_metaEO_linkService.FindQuery(x => x.MetaEOId == metaId).Select(x => x.EOID).ToListAsync();
            List<TestItemWithTestCount> testList = new List<TestItemWithTestCount>();
            foreach (var eoId in linkedEos)
            {
                var data = await GetLinkedTestItemsWithCountAsync(eoId);
                testList.AddRange(data);
            }
            return testList.GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public async Task<List<Test>> GetTestTestsItemIsLinkedToAsync(int testItemId)
        {
            var tests = await _test_item_linkService.FindQueryWithIncludeAsync(x => x.TestItemId == testItemId, new string[] { nameof(_test_item_link.Test) }).Select(x => x.Test).ToListAsync();
            return tests;
        }

        public async System.Threading.Tasks.Task UnlinkTestsAsync(int eoId, TestItemOptions options)
        {
            var eo = await _enablingObjectiveService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFoundException"]);
            }
            else
            {
                foreach (var testId in options.TestIds)
                {
                    var test = await _testItemService.FindQuery(x => x.Id == testId).FirstOrDefaultAsync();
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, test, TestItemOperations.Update);
                    if (result.Succeeded)
                    {
                        test.EOId = null;
                        var validationResult = await _testItemService.UpdateAsync(test);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task ReorderMetaEOLinksAsync(EnablingObjective_MetaEO_LinkOptions options)
        {
            var metaeo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.MetaEOId, new string[] { nameof(_eo.EnablingObjective_MetaEO_Links) }).FirstOrDefaultAsync();
            if (metaeo == null)
            {
                throw new BadHttpRequestException(message: _localizer["MetaEONotFoundException"]);
            }
            else
            {
                metaeo.UnlinkAllMetaEO();
                foreach (var eoId in options.EOIDs)
                {
                    var eo = await _enablingObjectiveService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
                    metaeo.LinkMetaEO(eo);
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, metaeo, EnablingObjectiveOperations.Update);
                    if (result.Succeeded)
                    {
                        var validationResult = await _enablingObjectiveService.UpdateAsync(metaeo);
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
        }

        public async System.Threading.Tasks.Task LinkMetaEOsAsync(EnablingObjective_MetaEO_LinkOptions options)
        {
            var metaeo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.MetaEOId, new string[] { nameof(_eo.EnablingObjective_MetaEO_Links) }).FirstOrDefaultAsync();
            if (metaeo == null)
            {
                throw new BadHttpRequestException(message: _localizer["MetaEONotFoundException"]);
            }
            else
            {
                foreach (var eoId in options.EOIDs)
                {
                    var eo = await _enablingObjectiveService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
                    metaeo.LinkMetaEO(eo);
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, metaeo, EnablingObjectiveOperations.Update);
                    if (result.Succeeded)
                    {
                        var validationResult = await _enablingObjectiveService.UpdateAsync(metaeo);
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
        }

        public async System.Threading.Tasks.Task UnlinkMetaEOsAsync(EnablingObjective_MetaEO_LinkOptions options)
        {
            var metaeo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == options.MetaEOId, new string[] { nameof(_eo.EnablingObjective_MetaEO_Links) }).FirstOrDefaultAsync();
            if (metaeo == null)
            {
                throw new BadHttpRequestException(message: _localizer["MetaEONotFoundException"]);
            }
            else
            {
                foreach (var eoId in options.EOIDs)
                {
                    var eo = await _enablingObjectiveService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
                    metaeo.UnlinkMetaEO(eo);
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, metaeo, EnablingObjectiveOperations.Delete);
                    if (result.Succeeded)
                    {
                        var validationResult = await _enablingObjectiveService.UpdateAsync(metaeo);
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
        }

        public async Task<List<EnablingObjective>> GetMetaEOLinksWithAsync(int metaEoId)
        {
            var result = await _eo_metaEO_linkService.FindQueryWithIncludeAsync(x => x.MetaEOId == metaEoId, new string[] { nameof(_eo_metaEO_link.EnablingObjective) }).Select(x => x.EnablingObjective).ToListAsync();
            return result;
        }

        public async Task<bool> CheckCatForEOWithLinkAsync(int catId)
        {
            var category = await _enablingObjective_CategoryService.FindQueryWithIncludeAsync(x => x.Id == catId, new string[] { nameof(_eoCat.EnablingObjective_SubCategories) }).FirstOrDefaultAsync();
            foreach (var subCat in category.EnablingObjective_SubCategories)
            {
                if (await CheckSubCatForEOWithLinkAsync(subCat.Id))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CheckSubCatForEOWithLinkAsync(int subCatId)
        {
            var subCategory = await _enablingObjective_SubCategoryService.FindQueryWithIncludeAsync(x => x.Id == subCatId, new string[] { nameof(_eoSub.EnablingObjective_Topics), nameof(_eoSub.EnablingObjectives) }).FirstOrDefaultAsync();
            foreach (var topic in subCategory.EnablingObjective_Topics)
            {
                if (await CheckTopicForEOWithLinkAsync(topic.Id))
                {
                    return true;
                }
            }
            var eos = await _enablingObjectiveService.FindQuery(x => x.SubCategoryId == subCatId && x.TopicId == null).ToListAsync();
            foreach (var eo in eos)
            {
                var stats = await GetEOLinkedStats(eo.Id);
                if (eo.Active)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CheckTopicForEOWithLinkAsync(int topicId)
        {
            var topic = await _enablingObjective_TopicService.FindQueryWithIncludeAsync(x => x.Id == topicId, new string[] { nameof(_eoTopic.EnablingObjectives) }).FirstOrDefaultAsync();
            foreach (var eo in topic.EnablingObjectives)
            {
                var stats = await GetEOLinkedStats(eo.Id);
                if (eo.Active)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<EnablingObjective_Step>> GetEO_StepsAsync(int eoId)
        {
            var eos = await _enablingObjectiveService.AllQueryWithInclude(new string[] { nameof(_eo.EnablingObjective_Steps) }).Where(x => x.Id == eoId).FirstOrDefaultAsync();
            var steps = eos.EnablingObjective_Steps.ToList(); // _task_StepService.FindAsync(x => x.TaskId == task.Id);

            steps = steps.Where(s => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, s, EnablingObjective_StepOperations.Read).Result.Succeeded)?.ToList();
            return steps.OrderBy(x => x.Number).ToList();
        }

        public async Task<EnablingObjective_Step> CreateStepAsync(int eoId, EnablingObjective_StepCreateOptions options)
        {
            var eo = await GetAsync(eoId);
            var step = new EnablingObjective_Step(eoId, options.Description, options.Number, options.ParentStepId, options.Active);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, step, EnablingObjective_StepOperations.Create);

            if (result.Succeeded)
            {
                step = eo.AddStep(step);
                step.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                step.CreatedDate = DateTime.Now;
                var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

                return step;
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<int> GetEOStepNumber(int id)
        {
            var num = await _eo_StepService.FindQueryWithDeleted(x => x.EOId == id).Select(x => x.Id).ToListAsync();
            return num.Count + 1;
        }

        public async Task<EnablingObjective_Step> UpdateStepAsync(int eoId, int stepId, EnablingObjective_StepUpdateOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == eoId, new string[] { nameof(_eo.EnablingObjective_Steps) }).FirstOrDefaultAsync();
            var step = eo.EnablingObjective_Steps.Where(x => x.Id == stepId).FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjective_StepOperations.Update);

            if (result.Succeeded)
            {
                // Todo Update Logic
                step.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                step.ModifiedDate = DateTime.Now;
                step.Description = options.Description;
                var validationResult = await _eo_StepService.UpdateAsync(step);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return step;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task ActivateStepAsync(int eoId, int stepId)
        {
            var eo = await GetAsync(eoId);
            var step = eo.EnablingObjective_Steps.Where(x => x.Id == stepId)?.FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjective_StepOperations.Update);

            if (result.Succeeded)
            {
                step.Activate();
                var validationResult = await _eo_StepService.UpdateAsync(step);
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

        public async System.Threading.Tasks.Task DeactivateStepAsync(int eoId, int stepId)
        {
            var eo = await GetAsync(eoId);
            var step = eo.EnablingObjective_Steps.Where(x => x.Id == stepId)?.FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjective_StepOperations.Update);

            if (result.Succeeded)
            {
                step.Deactivate();
                var validationResult = await _eo_StepService.UpdateAsync(step);
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

        public async System.Threading.Tasks.Task RemoveStepAsync(int eoId, int stepId)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == eoId, new string[] { nameof(_eo.EnablingObjective_Steps) }).FirstOrDefaultAsync();
            var step = eo.EnablingObjective_Steps.Where(x => x.Id == stepId)?.FirstOrDefault();

            step.Delete();
            var validationResult = await _eo_StepService.UpdateAsync(step);
        }

        public async Task<EnablingObjective_Question> AddQuestionAsync(int eoId, EnablingObjective_QuestionCreateOptions options)
        {
            var eo = await GetAsync(eoId);
            var question = new EnablingObjective_Question(eo.Id, options.Question, options.Answer, options.QuestionNumber);
            var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Update);
            var questionResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, EnablingObjective_QuestionOperations.Create);

            if (eoResult.Succeeded && questionResult.Succeeded)
            {
                var task_question = eo.AddQuestion(question);

                task_question.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                task_question.CreatedDate = DateTime.Now;
                var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return question;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task RemoveQuestionAsync(int eoId, int questionId)
        {
            var task = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == eoId, new string[] { nameof(_eo.EnablingObjective_Questions) }).FirstOrDefaultAsync();
            var question = await _eoquestionService.FindQuery(x => x.Id == questionId).FirstOrDefaultAsync();
            question.Delete();
            await _eoquestionService.UpdateAsync(question);

        }

        public async Task<int> GetQuestionNumber(int id)
        {
            var num = await _eoquestionService.AllQueryWithIncludeAndDeleted(new string[] { nameof(_eo_step.EnablingObjective) }).Where(x => x.EnablingObjectiveId == id).Select(x => x.Id).ToListAsync();
            return num.Count + 1;
        }

        public async System.Threading.Tasks.Task UpdateQuestionAsync(int id, int quesId, EnablingObjective_QuestionCreateOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { nameof(_eo.EnablingObjective_Questions) }).FirstOrDefaultAsync();
            var question = eo.EnablingObjective_Questions.Where(x => x.Id == quesId).FirstOrDefault();
            var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjective_QuestionOperations.Update);
            var questionResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, question, EnablingObjective_QuestionOperations.Update);
            if (eoResult.Succeeded && questionResult.Succeeded)
            {
                question.Question = options.Question;
                question.Answer = options.Answer;
                question.ModifiedDate = DateTime.Now;
                question.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                var validationResult = await _eoquestionService.UpdateAsync(question);
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

        public async Task<List<EnablingObjective_Question>> GetEO_QuestionsAsync(int taskId)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_eo.EnablingObjective_Questions) }).FirstOrDefaultAsync();
            var eoQuestion = eo?.EnablingObjective_Questions.OrderBy(x => x.QuestionNumber).ToList();
            eoQuestion = eoQuestion.Where(q => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, q, Task_QuestionOperations.Read).Result.Succeeded).ToList();

            return eoQuestion;
        }

        public async System.Threading.Tasks.Task UpdateQuestionNumbers(EnablingObjective_QuestionNumberOptions options)
        {
            for (var id = 0; id < options.QuestionIds.Length; id++)
            {
                var eo_question = await _eoquestionService.FindQuery(x => x.Id == options.QuestionIds[id]).FirstOrDefaultAsync();
                eo_question.QuestionNumber = options.Numbers[id];
                await _eoquestionService.UpdateAsync(eo_question);
            }
        }

        public async Task<List<EnablingObjective_Suggestion>> GetAllSuggestionsAsync(int eoId)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == eoId, new string[] { nameof(_eo.EnablingObjective_Suggestions) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFound"]);
            }
            else
            {
                return eo.EnablingObjective_Suggestions.OrderBy(x => x.Number).ToList();
            }
        }

        public async Task<EnablingObjective_Suggestion> CreateEOSuggestionAsync(int eoId, EnablingObjective_SuggestionOptions options)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == eoId, new string[] { nameof(_eo.EnablingObjective_Suggestions) }).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["TaskNotFound"]);
            }
            else
            {
                var number = await GetSuggestionNumberAsync(eoId);
                var eo_suggestion = new EnablingObjective_Suggestion(eoId, options.Description, number);
                eo.AddSuggestion(eo_suggestion);
                await _enablingObjectiveService.UpdateAsync(eo);
                return eo_suggestion;
            }
        }


        public async Task<int> GetSuggestionNumberAsync(int eoId)
        {
            var sugg = await _eo_suggestionService.FindQueryWithDeleted(x => x.EOId == eoId).ToListAsync();
            if (sugg == null)
            {
                return 1;
            }
            {
                var number = sugg.Count;
                return number + 1;
            }
        }

        public async System.Threading.Tasks.Task UpdateSuggestionAsync(int taskId, int suggestionId, EnablingObjective_SuggestionOptions options)
        {
            var suggestion = await _eo_suggestionService.FindQuery(x => x.Id == suggestionId).FirstOrDefaultAsync();
            var suggestionValidation = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, suggestion, EnablingObjective_SuggestionOperations.Update);
            if (suggestionValidation.Succeeded)
            {
                suggestion.Description = options.Description;
                var validationResult = await _eo_suggestionService.UpdateAsync(suggestion);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: String.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }

        }

        public async System.Threading.Tasks.Task DeleteSuggestionAsync(int eoId, int suggestionId)
        {
            var eo = await _enablingObjectiveService.FindQueryWithIncludeAsync(x => x.Id == eoId, new string[] { nameof(_eo.EnablingObjective_Suggestions) }).FirstOrDefaultAsync();
            var suggestion = eo.EnablingObjective_Suggestions.Where(x => x.Id == suggestionId).FirstOrDefault();
            var eoValidation = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, TaskOperations.Update);
            var suggestionValidation = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, suggestion, Task_SuggestionOperations.Delete);
            if (eoValidation.Succeeded && suggestionValidation.Succeeded)
            {
                //eo.RemoveSuggestion(suggestion);

                var validationResult = await _eo_suggestionService.UpdateAsync(suggestion);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: String.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task UpdateSuggestionNumbers(EnablingObjective_SuggestionNumberOptions options)
        {
            for (var id = 0; id < options.SuggestionIds.Length; id++)
            {
                var eo_suggestion = await _eo_suggestionService.FindQuery(x => x.Id == options.SuggestionIds[id]).FirstOrDefaultAsync();
                eo_suggestion.Number = options.Numbers[id];
                await _eo_suggestionService.UpdateAsync(eo_suggestion);
            }
        }

        public async System.Threading.Tasks.Task EditSpecificField(int id, EOSpecificUpdateOptions option)
        {
            var eo = await _enablingObjectiveService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFound"]);
            }
            else
            {
                switch (option.Field.ToLower().Trim())
                {
                    case "criteria":
                        eo.Criteria = option.Value;
                        break;
                    case "references":
                        eo.References = option.Value;
                        break;
                    case "conditions":
                        eo.Conditions = option.Value;
                        break;
                }
                await _enablingObjectiveService.UpdateAsync(eo);
            }
        }

        public async Task<List<Tool>> GetToolsAsync(int eoId)
        {
            var eo = await _enablingObjectiveService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
            if (eo == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFound"]);
            }
            else
            {
                List<Tool> tools = new List<Tool>();
                var eoTools = await _eo_toolService.FindQuery(x => x.EOId == eoId).ToListAsync();
                foreach (var eo_tool in eoTools)
                {
                    var tool = await _toolService.FindQuery(x => x.Id == eo_tool.ToolId).FirstOrDefaultAsync();
                    if (tool == null)
                    {
                        throw new BadHttpRequestException(message: _localizer["ToolNotFound"]);
                    }
                    else
                    {
                        tools.Add(tool);
                    }
                }
                return tools;
            }
        }

        public async System.Threading.Tasks.Task UpdateToolsAsync(int eoId, EnablingObjectiveOptions options)
        {
            await DeleteAllToolLinks(eoId);
            var eo = await _enablingObjectiveService.FindQuery(x => x.Id == eoId).FirstOrDefaultAsync();
            var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Update);
            List<Tool> tools = new List<Tool>();
            foreach (var toolId in options.ToolIds)
            {
                var tool = await _toolService.GetAsync(toolId);

                var toolsResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);

                if (eoResult.Succeeded && toolsResult.Succeeded)
                {
                    var task_tool = eo.AddTool(tool, eo.Id);
                    task_tool.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    task_tool.CreatedDate = DateTime.Now;
                    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        tools.Add(tool);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task DeleteAllToolLinks(int eoId)
        {
            var eo_tools = await _eo_toolService.FindQuery(x => x.EOId == eoId).ToListAsync();
            foreach (var tool in eo_tools)
            {
                var validationResult = await _eo_toolService.DeleteAsync(tool);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }


        public async Task<List<Tool>> AddToolAsync(int eoId, ToolAddOptions options)
        {
            var eo = await GetAsync(eoId);
            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Update);
            List<Tool> tools = new List<Tool>();
            foreach (var toolId in options.ToolIds)
            {

                var tool = await _toolService.GetAsync(toolId);

                var toolsResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);

                if (taskResult.Succeeded && toolsResult.Succeeded)
                {
                    var task_tool = eo.AddTool(tool, eo.Id);
                    task_tool.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    task_tool.CreatedDate = DateTime.Now;
                    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        tools.Add(tool);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return tools;
        }

        public async Task<List<EnablingObjective_Tool>> GetEOToolLinksAsync(int eoId)
        {
            var eo_tools = await _eo_toolService.FindQuery(x => x.EOId == eoId).ToListAsync();
            return eo_tools;
        }

        public async System.Threading.Tasks.Task RemoveTools(int eoId, EnablingObjectiveOptions options)
        {
            var eo = await _enablingObjectiveService.AllQueryWithInclude(new string[] { nameof(_eo.EnablingObjective_Tools) }).Where(x => x.Id == eoId).FirstOrDefaultAsync();
            foreach (var toolId in options.ToolIds)
            {
                var tool = await _toolService.FindQuery(x => x.Id == toolId).FirstOrDefaultAsync();

                var eoResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Update);
                var toolsResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);

                if (eoResult.Succeeded && toolsResult.Succeeded)
                {
                    eo.RemoveTool(tool);

                    var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
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

        public async System.Threading.Tasks.Task RemoveToolAsync(int eoId, int toolId)
        {
            var eo = await GetAsync(eoId);
            var tool = await _toolService.GetAsync(toolId);

            var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, eo, EnablingObjectiveOperations.Update);
            var toolsResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tool, ToolOperations.Read);

            if (taskResult.Succeeded && toolsResult.Succeeded)
            {
                eo.RemoveTool(tool);

                var validationResult = await _enablingObjectiveService.UpdateAsync(eo);
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

        public async Task<List<EnablingObjective>> GetSQForSubCatAndTopicAsync(SQForTQVM options)
        {
            var eos = await _enablingObjectiveService.FindQuery(x => x.SubCategoryId == options.SubCatId && x.TopicId == options.TopicId && x.IsSkillQualification).ToListAsync();
            return eos;
        }

        public async Task<List<EnablingObjective_SubCategory>> GetSubCatWithNumberAsync(int id)
        {
            var subCats = await _enablingObjective_SubCategoryService.FindQuery(x => x.CategoryId == id).ToListAsync();
            var cat = await _enablingObjective_CategoryService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (cat == null)
            {
                throw new BadHttpRequestException(message: _localizer["CategoryNotFoundException"]);
            }
            else
            {
                for (int i = 0; i < subCats.Count; i++)
                {
                    subCats[i].Title = cat.Number.ToString() + "." + subCats[i].Number.ToString() + " - " + subCats[i].Title;
                }

                return subCats;
            }
        }

        public async Task<List<EnablingObjective_Topic>> GetTopicNumberAsync(int id)
        {
            var topics = await _enablingObjective_TopicService.FindQuery(x => x.SubCategoryId == id).ToListAsync();
            var subCat = await _enablingObjective_SubCategoryService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (subCat == null)
            {
                throw new BadHttpRequestException(message: _localizer["SubCategory Not Found"]);
            }
            else
            {
                var cat = await _enablingObjective_CategoryService.FindQuery(x => x.Id == subCat.CategoryId).FirstOrDefaultAsync();
                if (cat == null)
                {
                    throw new BadHttpRequestException(message: _localizer["CategoryNotFoundException"]);
                }
                else
                {
                    for (int i = 0; i < topics.Count; i++)
                    {
                        topics[i].Title = cat.Number.ToString() + "." + subCat.Number.ToString() + "." + topics[i].Number.ToString() + " - " + topics[i].Title;
                    }

                    return topics;
                }
            }
        }

        public async Task<bool> CheckCanEOBeDeactivatedAsync(int id)
        {
            var eo = await _enablingObjectiveService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (eo != null)
            {
                if (eo.isMetaEO)
                {
                    var eoLinks = await _eo_metaEO_linkService.FindQuery(x => x.MetaEOId == eo.Id).ToListAsync();
                    foreach (var eoLink in eoLinks)
                    {
                        var linkedEO = await _enablingObjectiveService.FindQuery(x => x.Id == eoLink.EOID).FirstOrDefaultAsync();
                        if (linkedEO == null)
                        {
                            throw new BadHttpRequestException(message: _localizer["Enabling Objective Not Found"]);
                        }
                        else
                        {
                            var testQuestions = await _testItemService.FindQuery(x => x.EOId == linkedEO.Id).ToListAsync();
                            foreach (var tq in testQuestions)
                            {
                                var testLinkss = await _test_item_linkService.FindQuery(x => x.TestItemId == tq.Id).ToListAsync();
                                foreach (var tl in testLinkss)
                                {
                                    var test = await _testService.FindQuery(x => x.Id == tl.TestId && x.IsPublished == true).FirstOrDefaultAsync();
                                    if (test != null)
                                    {
                                        var isReleased = await _classScheduleRosterService.FindQuery(x => x.TestId == test.Id && x.IsReleased == true).AnyAsync();
                                        if (isReleased)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return false;
                }
                else
                {
                    var testQuestions = await _testItemService.FindQuery(x => x.EOId == eo.Id).ToListAsync();
                    foreach (var tq in testQuestions)
                    {
                        var testLinkss = await _test_item_linkService.FindQuery(x => x.TestItemId == tq.Id).ToListAsync();
                        foreach (var tl in testLinkss)
                        {
                            var test = await _testService.FindQuery(x => x.Id == tl.TestId && x.IsPublished == true).FirstOrDefaultAsync();
                            if (test != null)
                            {
                                var isReleased = await _classScheduleRosterService.FindQuery(x => x.TestId == test.Id && x.IsReleased == true).AnyAsync();
                                if (isReleased)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Enabling Objective Not Found"]);
            }
        }


        public async Task<List<EnablingObjective>> GetEOActiveInactive(string option)
        {
            var rrList = new List<EnablingObjective>();

            switch (option.ToLower().Trim())
            {
                case "active":
                    rrList = await _enablingObjectiveService.FindQuery(x => x.Active == true).Select(s => new EnablingObjective
                    {
                        Id = s.Id,
                        Description = s.Description,
                        Number=s.Number
                    }).OrderBy(o => o.Description).ToListAsync();
                    break;
                case "inactive":
                    rrList = await _enablingObjectiveService.FindQuery(x => x.Active == false).Select(s => new EnablingObjective
                    {
                        Id = s.Id,
                        Description = s.Description,
                        Number = s.Number
                    }).OrderBy(o => o.Description).ToListAsync();
                    break;

            }

            return rrList;

        }

        public async Task<List<EOCategoryVM>>  GetSimplifiedSubCategories(int categoryId)
        {
            var mycat = await _enablingObjective_SubCategoryService.FindQueryAsync(x => x.Active == true && x.CategoryId==categoryId);
            var categories = await mycat.Select(x => new EOCategoryVM
            {
                Id = x.Id,
                Number = x.Number.GetValueOrDefault(),
                Title = x.Title,
                Active = x.Active
            }).
            OrderBy(s => s.Number)
            .ToListAsync();
            return categories;
        }

        public async Task<List<EOCategoryVM>>  GetSimplifiedTopics(int subCategoryId)
        {
            var mycat = await _enablingObjective_TopicService.FindQueryAsync(x => x.Active == true && x.SubCategoryId==subCategoryId);
            var categories = await mycat.Select(x => new EOCategoryVM
            {
                Id = x.Id,
                Number = x.Number.GetValueOrDefault(),
                Title = x.Title,
                Active = x.Active
            }).
            OrderBy(s => s.Number)
            .ToListAsync();
            return categories;
        }
        public async Task<List<EnablingObjective_Category>> GetSQsByPositionIdsAsync(List<int> positionIds)
        {
            var eos = await _pos_sq_service.GetSQByPositionIdsAsync(positionIds);
            var categoryIds = eos.Select(eo => eo.CategoryId).Distinct().ToList();
            var subCatIds = eos.Select(eo => eo.SubCategoryId).Distinct().ToList();
            var topicIds = eos.Select(eo => eo.TopicId).Distinct().ToList();

            var mycat = await _enablingObjective_CategoryService.GetMinimalEOCatDataByIds(categoryIds);
            var mySubCats = await _enablingObjective_SubCategoryService.GetMinimalEOSubCatDataByIds(subCatIds);
            var topics = await _enablingObjective_TopicService.GetMinimalEOTopicDataByIds(topicIds);

            var subCatsByCategory = mySubCats.GroupBy(sc => sc.CategoryId).ToDictionary(g => g.Key, g => g.OrderBy(o => o.Number).ToList());

            var topicsBySubCat = topics.GroupBy(t => t.SubCategoryId).ToDictionary(g => g.Key, g => g.OrderBy(o => o.Number).ToList());

            var eosBySubCat = eos.Where(eo => eo.TopicId == null).GroupBy(eo => (eo.CategoryId, eo.SubCategoryId))
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(eo => ParseEoNumber(eo.Number)).ToList()
                );

            var eosByTopic = eos.Where(eo => eo.TopicId != null).GroupBy(eo => (eo.CategoryId, eo.SubCategoryId, eo.TopicId.Value))
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(eo => ParseEoNumber(eo.Number)).ToList()
                );

            mycat = mycat.OrderBy(c => c.Number).ToList();
            foreach (var cat in mycat)
            {
                if (subCatsByCategory.TryGetValue(cat.Id, out var subcats))
                {
                    cat.EnablingObjective_SubCategories = subcats;
                    foreach (var sub in subcats)
                    {
                        if (eosBySubCat.TryGetValue((cat.Id, sub.Id), out var eoList))
                            sub.EnablingObjectives = eoList;

                        if (topicsBySubCat.TryGetValue(sub.Id, out var topicList))
                        {
                            sub.EnablingObjective_Topics = topicList;
                            foreach (var topic in topicList)
                            {
                                if (eosByTopic.TryGetValue((cat.Id, sub.Id, topic.Id), out var topicEos))
                                    topic.EnablingObjectives = topicEos;
                            }
                        }
                    }
                }
            }

            mycat = mycat.Where(da => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, da, EnablingObjective_CategoryOperations.Read).Result.Succeeded).ToList();

            return mycat;
        }

        private static long ParseEoNumber(string number)
        {
            if (long.TryParse(number, out long num))
                return num;

            if (number.Contains("."))
            {
                var parts = number.Split('.');
                if (parts.Length > 3 && long.TryParse(parts[3], out long subNum))
                    return subNum;
            }

            return 0;
        }
    }
}
