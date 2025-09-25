using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjective_Category;
using QTD2.Infrastructure.Model.EnablingObjective_MetaEO_Link;
using QTD2.Infrastructure.Model.EnablingObjective_Procedure_Link;
using QTD2.Infrastructure.Model.EnablingObjective_Question;
using QTD2.Infrastructure.Model.EnablingObjective_SaftyHazard_Link;
using QTD2.Infrastructure.Model.EnablingObjective_Step;
using QTD2.Infrastructure.Model.EnablingObjective_SubCategory;
using QTD2.Infrastructure.Model.EnablingObjective_Suggestion;
using QTD2.Infrastructure.Model.EnablingObjective_Topic;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.TestItem;
using QTD2.Infrastructure.Model.Tool;
using QTD2.Infrastructure.Model.TreeDataVMs;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEnablingObjectiveService
    {
        public Task<List<EnablingObjective_Category>> GetAsync();

        public Task<List<EOCatTreeVM>> GetMinimalDataForTreeAsync();

        public Task<List<EnablingObjective_Category>> GetSQAsync();
        public Task<List<EOCategoryVM>> GetSimplifiedCategories(string order);
        public Task<List<EOCategoryVM>> GetSimplifiedSubCategories(int categoryId);
        public Task<List<EOCategoryVM>> GetSimplifiedTopics(int subCategoryId);

        public Task<List<EnablingObjective>> GetEOAsync();

        public Task<string> GetEONumberWithTopicAsync(int selectedCatId, int selectedSubCatId, int selectedTopicId);

        public Task<string> GetEONumberWithoutTopicAsync(int selectedCatId, int selectedSubCatId);

        public Task<bool> CheckIsMetaAsync(int id);

        public Task<EnablingObjective> GetAsync(int id);

        public Task<EnablingObjective> CreateAsync(EnablingObjectiveCreateOptions options);

        public Task<EnablingObjective> CreateFromILAAsync(EnablingObjectiveCreateOptions options);

        public Task<EnablingObjective> UpdateAsync(int id, EnablingObjectiveCreateOptions options);

        public Task<EnablingObjective> DeleteAsync(int id);

        public Task<EnablingObjective> DeactivateAsync(int id);

        public Task<EnablingObjective> ActivateAsync(int id);

        public System.Threading.Tasks.Task LinkProcedureAsync(EO_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkProcedureAsync(EO_LinkOptions options);

        public Task<List<ProceduresWithLinkCount>> GetProcedureWithLinkCount(int eoId);

        public Task<List<ProceduresWithLinkCount>> GetLinkedProceduresToMetaEOAsync(int metaId);

        public System.Threading.Tasks.Task LinkSaftyHazardAsync(EO_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkSaftyHazardAsync(EO_LinkOptions options);

        public Task<List<SafetyHazardWithLinkCount>> GetSafetyHazardWithLinkCounts(int eoId);

        public Task<List<SafetyHazardWithLinkCount>> GetLinkedSHWithMetaEOAsync(int metaId);

        public Task<List<EnablingObjective_Category>> GetCategoriesAsync();

        public Task<EnablingObjective_Category> GetCategoryAsync(int categoryId);

        public Task<List<EnablingObjective_SubCategory>> GetSubCatWithNumberAsync(int id);
        public Task<List<EnablingObjective_Topic>> GetTopicNumberAsync(int id);

        public Task<List<EnablingObjective_Category>> GetCategoryWithSubCategoryAsync();

        public Task<EnablingObjective_Category> CreateCategoryAsync(EnablingObjective_CategoryOptions options);

        public Task<EnablingObjective_Category> UpdateCategoryAsync(int id, EnablingObjective_CategoryOptions options);

        public Task<int> GetCategoryNumberAsync();

        public Task<EnablingObjective_SubCategory> CreateSubCategoryAsync(EnablingObjective_SubCategoryOptions options);

        public Task<List<EnablingObjective_SubCategory>> GetAllSubCategories();

        public Task<List<EnablingObjective_Topic>> GetAllTopics();

        public Task<List<EnablingObjective>> GetAllSqs();

        public Task<List<EnablingObjective_SubCategory>> GetSubCategoriesAsync(int categoryId);

        public Task<EnablingObjective_SubCategory> GetSubCategoryAsync(int subCatId);

        public Task<int> GetSubCategoryNumberAsync(int catId);

        public Task<EnablingObjective_SubCategory> UpdateSubCategoryAsync(int subCatId, EnablingObjective_SubCategoryOptions options);

        public Task<EnablingObjective_Topic> CreateTopicAsync(int subCategoryId, EnablingObjective_TopicOptions options);

        public Task<EnablingObjective_Topic> UpdateTopicAsync(int topicId, EnablingObjective_TopicOptions options);

        public Task<List<EnablingObjective_Topic>> GetTopicsAsync(int subCategoryId);

        public Task<string> GetCategoryIdForTopic(int subCatId);

        public Task<List<EnablingObjective_Topic>> GetTopicsAsync();

        public Task<int> GetTopicNumberAsync(int catId, int subCatId);

        public Task<EnablingObjective_Topic> GetTopicAsync(int id);

        public System.Threading.Tasks.Task LinkTaskAsync(EO_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkTasksAsync(EO_LinkOptions options);

        public Task<List<TaskWithCountOptions>> GetLinkedTasksWithCountAsync(int eoId);

        public Task<List<TaskWithCountOptions>> GetLinkedTaskWithMetaEOs(int metaId);

        public System.Threading.Tasks.Task linkRegReqAsync(EO_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkRRAsync(EO_LinkOptions options);

        public Task<List<RegulatoryRequirementWithLinkCount>> GetLinkedRRWithCount(int eoId);

        public Task<List<RegulatoryRequirementWithLinkCount>> GetLinkedRRWithMetaEOAsync(int metaId);


        public System.Threading.Tasks.Task LinkILAAsync(EO_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkILAAsync(EO_LinkOptions options);

        public Task<List<ILAWithCountOptions>> GetILAWithLinkCount(int eoId);

        public Task<List<ILAWithCountOptions>> GetLinkedILAToMetaEOWithCountAsync(int metaId);

        public System.Threading.Tasks.Task DeleteCategoryAsync(int id);

        public System.Threading.Tasks.Task MakeInactiveCategoryAsync(int id);

        public System.Threading.Tasks.Task MakeActiveCategoryAsync(int id);

        public System.Threading.Tasks.Task DeleteSubCategoryAsync(int id);

        public System.Threading.Tasks.Task MakeInactiveSubCategoryAsync(int id);

        public System.Threading.Tasks.Task MakeActiveSubCategoryAsync(int id);

        public System.Threading.Tasks.Task DeleteTopicAsync(int id);

        public System.Threading.Tasks.Task MakeInactiveTopicAsync(int id);

        public System.Threading.Tasks.Task MakeActiveTopicAsync(int id);

        public Task<EnablingObjectiveVM> CopyEOWithLinkages(int id, EnablingObjectiveCreateOptions options);

        public Task<EOStatsCount> GetEOLinkedStats(int eoId);

        public Task<EOStatsCount> GetMetaEOLinkedStatsAsync(int metaId);

        public Task<EOStatsCount> GetEONotLinkedStats();

        public Task<List<EnablingObjective>> GetLinkedEOsAsync(int id, string type);

        public Task<List<string>> GetLinkedIds(string name);

        public Task<EOWithAllDataVM> GetAllEODataAsync(int eoId);

        public Task<EnablingObjective> GetEOWithCatSubCatAndTopicAsync(int eoId);

        public Task<List<TestItem>> GetLinkedTestItemsAsync(int eoId);

        public Task<List<TestItemWithTestCount>> GetLinkedTestItemsWithCountAsync(int eoId);

        public Task<List<TestItemWithTestCount>> GetLinkedTestItemWithMetaEOAsync(int metaId);

        public Task<List<Test>> GetTestTestsItemIsLinkedToAsync(int testItemId);

        public System.Threading.Tasks.Task UnlinkTestsAsync(int eoId,TestItemOptions options);

        public System.Threading.Tasks.Task LinkMetaEOsAsync(EnablingObjective_MetaEO_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkMetaEOsAsync(EnablingObjective_MetaEO_LinkOptions options);

        public Task<List<EnablingObjective>> GetMetaEOLinksWithAsync(int metaEoId);

        public System.Threading.Tasks.Task ReorderMetaEOLinksAsync(EnablingObjective_MetaEO_LinkOptions options);

        public Task<bool> CheckCatForEOWithLinkAsync(int catId);

        public Task<bool> CheckSubCatForEOWithLinkAsync(int subCatId);

        public Task<bool> CheckTopicForEOWithLinkAsync(int topicId);

        public Task<List<EnablingObjective_Step>> GetEO_StepsAsync(int eoId);

        public Task<EnablingObjective_Step> CreateStepAsync(int eoId, EnablingObjective_StepCreateOptions options);

        public Task<int> GetEOStepNumber(int id);

        public Task<EnablingObjective_Step> UpdateStepAsync(int eoId, int stepNumber, EnablingObjective_StepUpdateOptions options);

        public System.Threading.Tasks.Task RemoveStepAsync(int eoId, int stepNumber);

        public System.Threading.Tasks.Task ActivateStepAsync(int eoId, int stepNumber);

        public System.Threading.Tasks.Task DeactivateStepAsync(int eoId, int stepNumber);

        public System.Threading.Tasks.Task LinkPositions(EO_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkPositions(EO_LinkOptions options);

        public Task<List<TaskPositionWithCount>> GetLinkedPositions(int eoId);

        public System.Threading.Tasks.Task LinkEmployee(EO_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkEmployee(EO_LinkOptions options);

        public Task<List<EmployeeWithCountOptions>> GetLinkedEmployees(int eoId);

        public Task<List<EnablingObjective_Question>> GetEO_QuestionsAsync(int taskId);

        public Task<EnablingObjective_Question> AddQuestionAsync(int eoId, EnablingObjective_QuestionCreateOptions options);

        public System.Threading.Tasks.Task RemoveQuestionAsync(int eoId, int questionId);

        public Task<int> GetQuestionNumber(int id);

        public System.Threading.Tasks.Task UpdateQuestionAsync(int id, int quesId, EnablingObjective_QuestionCreateOptions options);

        public System.Threading.Tasks.Task UpdateQuestionNumbers(EnablingObjective_QuestionNumberOptions options);

        public Task<EnablingObjective_Suggestion> CreateEOSuggestionAsync(int eoId, EnablingObjective_SuggestionOptions options);

        public Task<List<EnablingObjective_Suggestion>> GetAllSuggestionsAsync(int eoId);

        public Task<int> GetSuggestionNumberAsync(int eoId);

        public System.Threading.Tasks.Task UpdateSuggestionAsync(int eoId, int suggestionId, EnablingObjective_SuggestionOptions options);

        public System.Threading.Tasks.Task DeleteSuggestionAsync(int eoId, int suggestionId);

        public System.Threading.Tasks.Task UpdateSuggestionNumbers(EnablingObjective_SuggestionNumberOptions options);

        public System.Threading.Tasks.Task EditSpecificField(int id, EOSpecificUpdateOptions option);

        public Task<List<Tool>> GetToolsAsync(int eoId);

        public System.Threading.Tasks.Task UpdateToolsAsync(int eoId, EnablingObjectiveOptions options);

        public Task<List<Tool>> AddToolAsync(int eoId, ToolAddOptions options);

        public Task<List<EnablingObjective_Tool>> GetEOToolLinksAsync(int eoId);

        public System.Threading.Tasks.Task RemoveTools(int eoId, EnablingObjectiveOptions options);

        public System.Threading.Tasks.Task RemoveToolAsync(int eoId, int toolId);

        public Task<List<EnablingObjective>> GetSQForSubCatAndTopicAsync(SQForTQVM options);

        public Task<bool> CheckCanEOBeDeactivatedAsync(int id);

        public Task<List<EnablingObjective>> GetEOActiveInactive(string option);

        Task<List<EnablingObjective_Category>> GetSQsByPositionIdsAsync(List<int> positionIds);

    }
}
