using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class EnablingObjectiveService : Common.Service<EnablingObjective>, IEnablingObjectiveService
    {
        private readonly IEnablingObjective_TopicService _eoTopicService;
        private readonly IEnablingObjective_SubCategoryService _eoSubCategoryService;
        private readonly IEnablingObjective_CategoryService _eoCategoryService;
        public EnablingObjectiveService(IEnablingObjectiveRepository enablingObjectiveRepository, IEnablingObjectiveValidation enablingObjectiveValidation,
            IEnablingObjective_TopicService eoTopicService, IEnablingObjective_SubCategoryService eoSubCategoryService, IEnablingObjective_CategoryService eoCategoryService)
            : base(enablingObjectiveRepository, enablingObjectiveValidation)
        {
            _eoTopicService = eoTopicService;
            _eoSubCategoryService = eoSubCategoryService;
            _eoCategoryService = eoCategoryService;
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesLinkedToTaskAsync(int taskId)
        {
            var enablingObjectives = await FindAsync(r => r.Task_EnablingObjective_Links.Where(r => r.TaskId == taskId).Any());

            foreach (var eo in enablingObjectives)
            {
                eo.EnablingObjective_Topic = eo.TopicId.GetValueOrDefault() == 0 ? null : (await _eoTopicService.GetAsync(eo.TopicId.Value));
                eo.EnablingObjective_SubCategory = eo.SubCategoryId == 0 ? null : (await _eoSubCategoryService.GetAsync(eo.SubCategoryId));
                eo.EnablingObjective_Category = eo.CategoryId == 0 ? null : (await _eoCategoryService.GetAsync(eo.CategoryId));

            }
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesLinkedToProcedureAsync(int procedureId)
        {
            var enablingObjectives = await FindAsync(r => r.Procedure_EnablingObjective_Links.Where(r => r.ProcedureId == procedureId).Any());
            foreach (var eo in enablingObjectives)
            {
                eo.EnablingObjective_Topic = eo.TopicId.GetValueOrDefault() == 0 ? null : (await _eoTopicService.GetAsync(eo.TopicId.Value));
                eo.EnablingObjective_SubCategory = eo.SubCategoryId == 0 ? null : (await _eoSubCategoryService.GetAsync(eo.SubCategoryId));
                eo.EnablingObjective_Category = eo.CategoryId == 0 ? null : (await _eoCategoryService.GetAsync(eo.CategoryId));
            }
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetLinksForMyDataPositionLinkage(string activePositions, string eoStatus, string eoFlaggedAsSkill, bool includeMetaEOs, string employeeStatus, DateTime employeeStartDate, DateTime employeeEndDate)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();
            if (!string.IsNullOrEmpty(eoStatus) && eoStatus == "Active Only")
                predicates.Add(r => r.Active);
            else if (!string.IsNullOrEmpty(eoStatus) && eoStatus == "Inactive Only")
                predicates.Add(r => !r.Active);
            if (!string.IsNullOrEmpty(employeeStatus) && employeeStatus == "Active Only")
                predicates.Add(r => r.EnablingObjective_Employee_Links.Select(e => e.Employee.Active == true).Count() > 0);
            else if (!string.IsNullOrEmpty(employeeStatus) && employeeStatus == "Inactive Only")
                predicates.Add(r => r.EnablingObjective_Employee_Links.Select(e => e.Employee.Active == false).Count() > 0);
            if (!string.IsNullOrEmpty(activePositions) && activePositions == "Active Only")
                predicates.Add(r => r.Position_SQs.Select(e => e.Position.Active == true).Count() > 0);
            else if (!string.IsNullOrEmpty(activePositions) && activePositions == "Inactive Only")
                predicates.Add(r => r.Position_SQs.Select(e => e.Position.Active == false).Count() > 0);
            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Employee_Links.Employee.Person", "Position_SQs.Position.EmployeePositions" });
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetItemByEnablingObjectiveAsync(List<int> objectivesIDs)
        {
            var enablingObjectives = await FindWithIncludeAsync(r => objectivesIDs.Contains(r.Id), new[] { "TestItems.TestItemType", "TestItems.TestItemMCQs", "TestItems.TestItemTrueFalses", "TestItems.TestItemFillBlanks", "TestItems.TestItemShortAnswers" });
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetMinimalEOData()
        {
            var eos = await AllQuery().Select(s => new EnablingObjective
            {
                Active = s.Active,
                Id = s.Id,
                CategoryId = s.CategoryId,
                SubCategoryId = s.SubCategoryId,
                TopicId = s.TopicId,
                Description = s.Description,
                Deleted = s.Deleted,
                Number = s.Number,
                isMetaEO = s.isMetaEO,
                IsSkillQualification = s.IsSkillQualification,
            }).ToListAsync();

            return eos;
        }

        public async Task<List<EnablingObjective>> GetCompactedEOData()
        {
            var eos = await AllQuery().Select(s => new EnablingObjective
            {
                Active = s.Active,
                Id = s.Id,
                CategoryId = s.CategoryId,
                SubCategoryId = s.SubCategoryId,
                TopicId = s.TopicId,
                Description = s.Description,
                Conditions = s.Conditions,
                Criteria = s.Criteria,
                EffectiveDate = s.EffectiveDate,
                Deleted = s.Deleted,
                Number = s.Number,
                isMetaEO = s.isMetaEO,
                IsSkillQualification = s.IsSkillQualification,
                References = s.References
            }).ToListAsync();

            return eos;
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesByEOIDs(List<int> enablingObjectiveIDs)
        {
            var enablingObjectives = await FindAsync(r => enablingObjectiveIDs.Contains(r.Id));
            foreach (var eo in enablingObjectives)
            {
                eo.EnablingObjective_Topic = eo.TopicId.GetValueOrDefault() == 0 ? null : (await _eoTopicService.GetAsync(eo.TopicId.Value));
                eo.EnablingObjective_SubCategory = eo.SubCategoryId == 0 ? null : (await _eoSubCategoryService.GetAsync(eo.SubCategoryId));
                eo.EnablingObjective_Category = eo.CategoryId == 0 ? null : (await _eoCategoryService.GetAsync(eo.CategoryId));

                if (eo.EnablingObjective_Topic != null) eo.EnablingObjective_Topic.EnablingObjectives = null;
                if (eo.EnablingObjective_SubCategory != null)
                {
                    eo.EnablingObjective_SubCategory.EnablingObjective_Topics = null;
                    eo.EnablingObjective_SubCategory.EnablingObjectives = null;
                }
                if (eo.EnablingObjective_Category != null)
                {
                    eo.EnablingObjective_Category.EnablingObjective_SubCategories = null;
                    eo.EnablingObjective_Category.EnablingObjectives = null;
                }
            }
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesByIDs(List<int> enablingObjectiveIDs)
        {
            var enablingObjectives = await FindAsync(r => enablingObjectiveIDs.Contains(r.Id));
            foreach (var eo in enablingObjectives)
            {
                if (eo.TopicId.GetValueOrDefault() == 0)
                {
                    eo.EnablingObjective_Topic = null;
                }
                else
                {
                    eo.EnablingObjective_Topic = await _eoTopicService.GetAsync(eo.TopicId.Value);
                    eo.EnablingObjective_Topic.EnablingObjectives = null;
                }
                if (eo.SubCategoryId == 0)
                {
                    eo.EnablingObjective_SubCategory = null;
                }
                else
                {
                    eo.EnablingObjective_SubCategory = await _eoSubCategoryService.GetAsync(eo.SubCategoryId);
                    eo.EnablingObjective_SubCategory.EnablingObjective_Topics = null;
                    eo.EnablingObjective_SubCategory.EnablingObjectives = null;
                }
                if (eo.CategoryId == 0)
                {
                    eo.EnablingObjective_Category = null;
                }
                else
                {
                    eo.EnablingObjective_Category = await _eoCategoryService.GetAsync(eo.CategoryId);
                    eo.EnablingObjective_Category.EnablingObjective_SubCategories = null;
                    eo.EnablingObjective_Category.EnablingObjectives = null;
                }
            }
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesLinkedToILAAsync(int iLAId)
        {
            var enablingObjectives = await FindAsync(r => r.ILA_EnablingObjective_Links.Where(r => r.ILAId == iLAId).Any());

            foreach (var eo in enablingObjectives)
            {
                eo.EnablingObjective_Topic = eo.TopicId.GetValueOrDefault() == 0 ? null : (await _eoTopicService.GetAsync(eo.TopicId.Value));
                eo.EnablingObjective_SubCategory = eo.SubCategoryId == 0 ? null : (await _eoSubCategoryService.GetAsync(eo.SubCategoryId));
                eo.EnablingObjective_Category = eo.CategoryId == 0 ? null : (await _eoCategoryService.GetAsync(eo.CategoryId));

            }
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetILAsLinkedToEnablingObjectives(List<int> enablingObjectiveIDs, bool showTrainingResources)
        {
            string includes = showTrainingResources ? "ILA_EnablingObjective_Links.ILA" : "ILA_EnablingObjective_Links.ILA.ILA_Resources";
            var enablingObjectives = await FindWithIncludeAsync(r => enablingObjectiveIDs.Contains(r.Id), new[] { includes });
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesNotLinkedToTaskAsync(string activeStatus, bool includeMetoEOs, bool includeSkillQualifications)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();

            predicates.Add(r => r.Task_EnablingObjective_Links.Where(r => r.Active).Count() == 0);

            if (activeStatus.ToUpper() == "ACTIVE ONLY")
            {
                predicates.Add(r => r.Active);
            }
            if (activeStatus.ToUpper() == "INACTIVE ONLY")
            {
                predicates.Add(r => !r.Active);
            }

            if (!includeMetoEOs)
            {
                predicates.Add(r => !r.isMetaEO);
            }

            if (!includeSkillQualifications)
            {
                predicates.Add(r => !r.IsSkillQualification);
            }

            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category" });

            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesNotLinkedToILAAsync(string activeStatus, bool includeMetoEOs, bool includeSkillQualifications)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();

            predicates.Add(r => r.ILA_EnablingObjective_Links.Where(r => r.Active).Count() == 0);

            if (activeStatus.ToUpper() == "ACTIVE ONLY")
            {
                predicates.Add(r => r.Active);
            }
            if (activeStatus.ToUpper() == "INACTIVE ONLY")
            {
                predicates.Add(r => !r.Active);
            }

            if (!includeMetoEOs)
            {
                predicates.Add(r => !r.isMetaEO);
            }

            if (!includeSkillQualifications)
            {
                predicates.Add(r => !r.IsSkillQualification);
            }

            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category" });

            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesByCategoriesAsync(List<int> categoryIds, string activeStatus, bool showMetaEosOnly, bool showSkillQualificationsOnly)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();

            predicates.Add(r => categoryIds.Contains(r.EnablingObjective_Category.Id));

            if (activeStatus.ToUpper() == "ACTIVE ONLY")
            {
                predicates.Add(r => r.Active);
            }
            if (activeStatus.ToUpper() == "INACTIVE ONLY")
            {
                predicates.Add(r => !r.Active);
            }

            if (showMetaEosOnly)
            {
                predicates.Add(r => r.isMetaEO);
            }

            if (showSkillQualificationsOnly)
            {
                predicates.Add(r => r.IsSkillQualification);
            }

            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category", "EnablingObjective_MetaEO_Links.EnablingObjective.EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category" });

            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetTasksByEnablingObjectivesAsync(List<int> enablingObjectiveIds)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();

            predicates.Add(r => enablingObjectiveIds.Contains(r.Id));

            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category", "Task_EnablingObjective_Links", "EnablingObjective_MetaEO_Links.EnablingObjective.Task_EnablingObjective_Links", "EnablingObjective_MetaEO_Links.EnablingObjective.EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category" });

            return enablingObjectives.ToList();
        }

        public async Task<List<Entities.Core.EnablingObjective>> GetEnablingObjectivesByPositionOrSkillAsync(int positionId, List<int> enablingObjectiveIds, string status)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();

            if (enablingObjectiveIds.Count() > 0)
                predicates.Add(r => enablingObjectiveIds.Contains(r.Id));

            if (positionId != 0)
                predicates.Add(r => r.Position_SQs.Any(r => positionId == r.PositionId));

            if (status.ToUpper() == "ACTIVE")
            {
                predicates.Add(r => r.Active);
            }
            if (status.ToUpper() == "INACTIVE")
            {
                predicates.Add(r => !r.Active);
            }

            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "Position_SQs" });

            return enablingObjectives.ToList();
        }


        public async Task<List<EnablingObjective>> GetForSkillQualificationAssessmentByPositionOrTask(int positionId, int taskId, string status)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();

            predicates.Add(r => r.IsSkillQualification);
            if (positionId != 0)
            {
                predicates.Add(r => r.Position_SQs.Any(r => positionId == r.PositionId));
            }

            if (taskId != 0)
            {
                predicates.Add(r => r.Task_EnablingObjective_Links.Any(r => taskId == r.TaskId));
            }

            if (status.ToUpper() == "ACTIVE")
            {
                predicates.Add(r => r.Active);
            }
            if (status.ToUpper() == "INACTIVE")
            {
                predicates.Add(r => !r.Active);
            }

            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "Position_SQs" });


            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetILAsByEnablingObjectiveAsync(List<int> enablingObjectiveIds, bool includeUnlinkedEos)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();

            predicates.Add(r => enablingObjectiveIds.Contains(r.Id));

            if (!includeUnlinkedEos)
            {
                predicates.Add(r => r.ILA_EnablingObjective_Links.Count() > 0);
            }

            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "ILA_EnablingObjective_Links.ILA.Provider", "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category" });

            return enablingObjectives.ToList();
        }

        public async System.Threading.Tasks.Task<List<EnablingObjective>> GetEnablingObjectivesAsync()
        {
            return (await AllWithIncludeAsync(new string[] { "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category" })).ToList();
        }
        public async System.Threading.Tasks.Task<List<EnablingObjective>> GetSQEnablingObjectivesAsync()
        {
            return (await FindWithIncludeAsync((r => r.IsSkillQualification), new string[] { "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category" })).ToList();
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesAllDataByEoIdAsync(List<int> eoIds)
        {
            List<Expression<Func<Domain.Entities.Core.EnablingObjective, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.EnablingObjective, bool>>>();
            predicates.Add(eo => eoIds.Contains(eo.Id));
            var enablingObjectives = (await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category" })).ToList();
            var eoWithPositions = (await FindWithIncludeAsync(predicates, new[] { "Position_SQs.Position" })).ToList();
            var eoWithTools = (await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Tools.Tool" }, true)).ToList();
            var eoWithProcedures = (await FindWithIncludeAsync(predicates, new[] { "Procedure_EnablingObjective_Links.Procedure" })).ToList();
            var eoWithSaftyHazards = (await FindWithIncludeAsync(predicates, new[] { "SafetyHazard_EO_Links.SaftyHazard" })).ToList();
            var eoWithSuggestions = (await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Suggestions" })).ToList();
            var eoWithQuestions = (await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Questions" })).ToList();
            var eoWithRegulatoryRequirements = (await FindWithIncludeAsync(predicates, new[] { "RegRequirement_EO_Links.RegulatoryRequirement" })).ToList();
            var eoWithTasks = (await FindWithIncludeAsync(predicates, new[] { "Task_EnablingObjective_Links.Task" })).ToList();
            var eoWithSteps = (await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Steps" })).ToList();

            foreach (var eo in enablingObjectives)
            {
                eo.EnablingObjective_Tools = eoWithTools.FirstOrDefault(r => r.Id == eo.Id).EnablingObjective_Tools;
                eo.Procedure_EnablingObjective_Links = eoWithProcedures.FirstOrDefault(r => r.Id == eo.Id).Procedure_EnablingObjective_Links;
                eo.SafetyHazard_EO_Links = eoWithSaftyHazards.FirstOrDefault(r => r.Id == eo.Id).SafetyHazard_EO_Links;
                eo.EnablingObjective_Suggestions = eoWithSuggestions.FirstOrDefault(r => r.Id == eo.Id).EnablingObjective_Suggestions;
                eo.EnablingObjective_Questions = eoWithQuestions.FirstOrDefault(r => r.Id == eo.Id).EnablingObjective_Questions;
                eo.RegRequirement_EO_Links = eoWithRegulatoryRequirements.FirstOrDefault(r => r.Id == eo.Id).RegRequirement_EO_Links;
                eo.Task_EnablingObjective_Links = eoWithTasks.FirstOrDefault(r => r.Id == eo.Id).Task_EnablingObjective_Links;
                eo.EnablingObjective_Steps = eoWithSteps.FirstOrDefault(r => r.Id == eo.Id).EnablingObjective_Steps;
            }
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesByIdAsync(List<int> eoIds, bool includeMetoEOs, bool includeSkillQualifications, bool includeInactiveEnablingObjectives)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();
            predicates.Add(r => eoIds.Contains(r.Id));
            if (!includeMetoEOs)
            {
                predicates.Add(r => !r.isMetaEO);
            }

            if (!includeSkillQualifications)
            {
                predicates.Add(r => !r.IsSkillQualification);
            }
            if (!includeInactiveEnablingObjectives)
            {
                predicates.Add(r => r.Active);
            }
            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category" });
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetEnablingObjectivesBySafetyHazardAsync(List<int> eoIds, bool includeMetaEnablingObjectives, bool includeSkillQualifications, bool includeInactiveEnablingObjectives)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();
            predicates.Add(r => eoIds.Contains(r.Id));

            if (!includeMetaEnablingObjectives)
            {
                predicates.Add(r => !r.isMetaEO);
            }
            if (!includeSkillQualifications)
            {
                predicates.Add(r => !r.IsSkillQualification);
            }
            if (!includeInactiveEnablingObjectives)
            {
                predicates.Add(r => r.Active);
            }
            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Topic", "EnablingObjective_SubCategory", "EnablingObjective_Category", "EnablingObjective_MetaEO_Links" });
            return enablingObjectives.ToList();
        }
        public async Task<EnablingObjective> GetForCopy(int id)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();
            predicates.Add(r => r.Id == id);
            var enablingObjectives = (await FindWithIncludeAsync(predicates, new[] { "TestItems" }, true)).FirstOrDefault(); ;

            var tasks_Eo_links = (await FindWithIncludeAsync(predicates, new[] { "Task_EnablingObjective_Links" }, true)).FirstOrDefault();
            var safetyHazardsEo_links = (await FindWithIncludeAsync(predicates, new[] { "SafetyHazard_EO_Links" }, true)).FirstOrDefault();
            var procedureEo_links = (await FindWithIncludeAsync(predicates, new[] { "Procedure_EnablingObjective_Links" }, true)).FirstOrDefault();
            var ilaEo_links = (await FindWithIncludeAsync(predicates, new[] { "ILA_EnablingObjective_Links" }, true)).FirstOrDefault();
            var enablingObjectives_MetaEo_links = (await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_MetaEO_Links" }, true)).FirstOrDefault();
            var regRequirement_Eo_links = (await FindWithIncludeAsync(predicates, new[] { "RegRequirement_EO_Links" }, true)).FirstOrDefault();

            enablingObjectives.Task_EnablingObjective_Links = tasks_Eo_links.Task_EnablingObjective_Links;
            enablingObjectives.SafetyHazard_EO_Links = safetyHazardsEo_links.SafetyHazard_EO_Links;
            enablingObjectives.Procedure_EnablingObjective_Links = procedureEo_links.Procedure_EnablingObjective_Links;
            enablingObjectives.ILA_EnablingObjective_Links = ilaEo_links.ILA_EnablingObjective_Links;
            enablingObjectives.EnablingObjective_MetaEO_Links = enablingObjectives_MetaEo_links.EnablingObjective_MetaEO_Links;
            enablingObjectives.RegRequirement_EO_Links = regRequirement_Eo_links.RegRequirement_EO_Links;
            return enablingObjectives;
        }

        public async Task<List<EnablingObjective>> GetProcedureLinkedEnablingObjectivesAsync(List<int> enablingObjectiveIds)
        {
            List<Expression<Func<EnablingObjective, bool>>> predicates = new List<Expression<Func<EnablingObjective, bool>>>();
            predicates.Add(r => enablingObjectiveIds.Contains(r.Id));
            var enablingObjectives = await FindWithIncludeAsync(predicates, new[] { "EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category", "Procedure_EnablingObjective_Links", "EnablingObjective_MetaEO_Links.EnablingObjective.Procedure_EnablingObjective_Links", "EnablingObjective_MetaEO_Links.EnablingObjective.EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category" });
            return enablingObjectives.ToList();
        }

        public async Task<List<EnablingObjective>> GetEOAsync()
        {
            var sqs = (await FindWithIncludeAsync(x => x.IsSkillQualification, new[] { "EnablingObjective_Topic", "EnablingObjective_Category", "EnablingObjective_SubCategory", "Position_SQs" })).ToList();
            return sqs;
        }

        public async Task<List<EnablingObjective>> GetEOByIdAsync(int eoId)
        {
            var sqs = (await FindWithIncludeAsync(x => x.IsSkillQualification && x.Id == eoId, new[] { "EnablingObjective_Topic", "EnablingObjective_Category", "EnablingObjective_SubCategory", "Position_SQs" })).ToList();
            return sqs;
        }

        public async Task<List<EnablingObjective_Suggestion>> GetAllSuggestionByIdAsync(int eoId)
        {
            var enablingObjective = (await FindWithIncludeAsync(x => x.Id == eoId, new string[] { "EnablingObjective_Suggestions" })).FirstOrDefault();
            if (enablingObjective == null)
            {
                throw new QTDServerException("Eo Not Found", false);
            }
            else
            {
                return enablingObjective.EnablingObjective_Suggestions.OrderBy(x => x.Number).ToList();
            }
        }

        public async Task<List<EnablingObjective_Step>> GetAllStepAsync(int eoId)
        {
            var enablingObjective = (await FindWithIncludeAsync(x => x.Id == eoId, new string[] { "EnablingObjective_Steps" })).FirstOrDefault();
            var eo_steps = enablingObjective.EnablingObjective_Steps.ToList();
            return eo_steps.OrderBy(x => x.Number).ToList();
        }
        public async Task<List<EnablingObjective_Question>> GetAllQuestionByIdAsync(int eoId)
        {
            var enablingObjective = (await FindWithIncludeAsync(x => x.Id == eoId, new string[] { "EnablingObjective_Questions" })).FirstOrDefault();
            var eoQuestion = enablingObjective?.EnablingObjective_Questions.OrderBy(x => x.QuestionNumber).ToList();
            return eoQuestion;
        }
        public async Task<EnablingObjective> GetEnablingObjectiveByIdAsync(int eoId)
        {
            return (await FindAsync(x => x.Id == eoId)).FirstOrDefault();
        }

        public async Task<EnablingObjective> GetMetaEnablingObjectiveAsync(int eoId)
        {
            return (await FindWithIncludeAsync(x => x.Id == eoId && x.isMetaEO, new string[] { "EnablingObjective_MetaEO_Links.EnablingObjective" })).FirstOrDefault();
        }
    }
}
