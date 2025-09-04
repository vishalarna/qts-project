using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.DutyArea;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.Position_Task_Link;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.SubdutyArea;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_Collaborator_Link;
using QTD2.Infrastructure.Model.Task_ILA_Link;
using QTD2.Infrastructure.Model.Task_Question;
using QTD2.Infrastructure.Model.Task_Reference_Link;
using QTD2.Infrastructure.Model.Task_RR_Link;
using QTD2.Infrastructure.Model.Task_Step;
using QTD2.Infrastructure.Model.Task_Suggestion;
using QTD2.Infrastructure.Model.Task_TrainingGroup;
using QTD2.Infrastructure.Model.Tool;
using QTD2.Infrastructure.Model.TreeDataVMs;
using QTD2.Infrastructure.Model.Version_Task;
using Task = QTD2.Domain.Entities.Core.Task;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskService
    {
        public Task<List<Domain.Entities.Core.Task>> GetAsync();

        public Task<Domain.Entities.Core.Task> GetAsync(int id);

        public Task<TaskWithAllLinkedDataVM> GetAllLinkDataOfTaskAsync(int id);

        public Task<Domain.Entities.Core.Task> GetAllTaskDataWithLinks(int id);

        public Task<Domain.Entities.Core.Task> CreateAsync(TaskCreateOptions options);

        public Task<Domain.Entities.Core.Task> CopyTask(int taskId, TaskCopyOptions options);

        public Task<Domain.Entities.Core.Task> UpdateAsync(int id, TaskUpdateOptions options);

        public System.Threading.Tasks.Task EditSpecificField(int id, SpecificUpdateOptions option);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task DeactivateAsync(int id);

        public System.Threading.Tasks.Task ActivateAsync(int id);

        public Task<List<DutyArea>> GetDutyAreasAsync();

        public Task<List<DutyArea>> GetDutyAreasOrderedByAsync(string orderBy);

        public Task<List<DutyArea>> GetDutyAreasWithSubDutyAreaAsync();

        public Task<List<DutyAreaTreeVM>> GetMinimizedTreeDataForTaskTree();

        public Task<DutyArea> GetDutyAreaAsync(int id);

        public Task<int> GetDutyAreaNumberAsync(string letter);

        public Task<DutyArea> CreateDutyAreaAsync(DutyAreaCreateOptions options);

        public Task<DutyArea> UpdateDutyAreaAsync(int id, DutyAreaUpdateOptions options);

        public System.Threading.Tasks.Task DeleteDutyAreaAsync(int id);

        public System.Threading.Tasks.Task DeactivateDutyAreaAsync(int id);

        public System.Threading.Tasks.Task ActivateDutyAreaAsync(int id);

        public Task<SubdutyArea> CreateSubDutyAreaAsync(int dutyAreaId, SubdutyAreaCreateOptions options);

        public Task<SubdutyArea> UpdateSubDutyAreaAsync(int daId, SubdutyAreaUpdateOptions options);

        public Task<List<SubdutyArea>> GetSubDutyAreas(int dutyAreaId);

        public Task<SubdutyArea> GetSubDutyArea(int id);
        public System.Threading.Tasks.Task DeleteSubDutyAreaAsync(int id);

        public System.Threading.Tasks.Task DeactivateSubDutyAreaAsync(int id);

        public System.Threading.Tasks.Task ActivateSubDutyAreaAsync(int id);

        public Task<List<Task_Question>> GetTask_QuestionsAsync(int taskId);

        public Task<Task_Question> AddQuestionAsync(int taskId, QuestionCreateOptions options);

        public System.Threading.Tasks.Task RemoveQuestionAsync(int taskId, int questionId);

        public Task<int> GetQuestionNumber(int id);

        public System.Threading.Tasks.Task UpdateQuestionAsync(int id, int quesId, QuestionCreateOptions options);

        public Task<List<Tool>> GetToolsAsync(int taskId);

        public System.Threading.Tasks.Task UpdateToolsAsync(int taskID, TaskOptions options);

        public Task<List<Tool>> AddToolAsync(int taskId, ToolAddOptions options);

        public Task<List<Task_Tool>> GetTaskToolLinksAsync(int taskId);

        public System.Threading.Tasks.Task RemoveTools(int taskId, TaskOptions options);

        public System.Threading.Tasks.Task RemoveToolAsync(int taskId, int toolId);

        public Task<List<EmployeeTaskVM>> GetTaskPositionEmployeesAsync(int taskId);

        public Task<List<EmployeeTaskVM>> GetLinkedEmployeeToMetaTaskWithCountAsync(int metaId);

        public Task<List<TaskPositionWithCount>> GetLinkedPositionsAsync(int taskId);

        public Task<List<TaskPositionWithCount>> GetLinkedPositionToMetaTaskWithCountAsync(int metaId);

        public System.Threading.Tasks.Task<PositionLinkResultVM> LinkPositionAsync(int taskId, TaskOptions options);

        public Task<List<Position>> LinkWithoutUnlinkPositions(int taskId, TaskOptions options);

        public System.Threading.Tasks.Task UnlinkPositionAsync(int taskId, TaskOptions options);

        public Task<List<TaskWithCountOptions>> GetTaskLinkedToPosition(int id);

        public Task<List<SaftyHazard>> GetLinkedSaftyHazardsAsync(int taskId);

        public System.Threading.Tasks.Task LinkSaftyHazardsAsync(int taskId, TaskOptions options);

        public Task<List<SafetyHazard_Task_Link>> GetTaskSHLinksAsync(int taskId);

        public System.Threading.Tasks.Task UnlinkSaftyHazardAsync(int taskId, TaskOptions options);

        public Task<List<SafetyHazardWithLinkCount>> GetLinkedSHWithCount(int id);

        public Task<List<SafetyHazardWithLinkCount>> GetLinkedSHWithMetaTaskAsync(int metaId);

        public Task<List<TaskRRWithCount>> GetLinkedRRWithMetaTaskAsync(int metaId);

        public Task<List<EnablingObjectiveWithCountOptions>> GetLinkedEOWithMetaTaskAsync(int metaId);

        public Task<List<TaskWithCountOptions>> GetTaskSHIsLinkedTo(int id);

        public Task<List<Procedure>> GetLinkedProceduresAsync(int taskId);

        public System.Threading.Tasks.Task UpdateTask_StepNumber(int[] numbers, int[] ids);

        public Task<List<MetaTask_OJTVM>> GetMetaTaskStepsAsync(int taskId);

        public System.Threading.Tasks.Task UpdateQuestionNumbers(Task_QuestionNumberOptions options);

        public System.Threading.Tasks.Task UpdateSuggestionNumbers(Task_SuggestionNumberOptions options);

        public System.Threading.Tasks.Task LinkProcedureAsync(int taskId, TaskOptions options);

        public Task<List<Procedure_Task_Link>> GetTaskProcLinks(int taskId);

        public System.Threading.Tasks.Task UnlinkProcedureAsync(int taskId, TaskOptions options);

        public Task<List<ProceduresWithLinkCount>> GetLinkedProceduresToMetaTaskAsync(int metaId);

        public Task<List<ProceduresWithLinkCount>> GetLinkedProcedureWithCount(int id);

        public Task<List<TaskWithCountOptions>> GetTasksProcIsLinkedTo(int id);

        public Task<List<EnablingObjective>> GetLinkedEnablingObjectivesAsync(int taskId);

        public Task<List<EnablingObjectiveWithCountOptions>> GetLinkedEOWithCount(int id);

        public Task<List<EnablingObjective>> LinkEnablingObjectiveAsync(int taskId, TaskOptions options);

        public Task<List<Task_EnablingObjective_Link>> GetTaskEOLinks(int taskId);

        public System.Threading.Tasks.Task UnlinkEnablingObjectiveAsync(int taskId, TaskOptions options);

        public Task<List<TaskWithCountOptions>> GetTasksEOIsLinkedToAsync(int id);

        public Task<Domain.Entities.Core.Task> LinkTaskReference(int taskId, Task_Reference_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkTaskReference(int taskId, int taskRefId);

        public Task<List<ILA_TaskObjective_Link>> GetTaskILALinks(int taskId);

        public System.Threading.Tasks.Task LinkILA(int taskId, TaskOptions options);

        public System.Threading.Tasks.Task UnlinkILA(int taskId, TaskOptions options);

        public Task<List<ILAWithCountOptions>> GetLinkedILAWithCount(int id);

        public Task<List<ILAWithCountOptions>> GetLinkedILAToMetaTaskWithCountAsync(int metaId);

        public Task<List<TaskWithCountOptions>> GetTasksILAIsLinkedWith(int id);

        public Task<List<RR_Task_Link>> GetTaskRRLinks(int taskId);

        public System.Threading.Tasks.Task LinkRR(int taskId, TaskOptions options);

        public System.Threading.Tasks.Task UnlinkRR(int taskId, TaskOptions options);

        public Task<List<TaskRRWithCount>> GetLinkedRRWithCount(int id);

        public Task<List<TaskWithCountOptions>> GetTaskLinkedToRR(int id);

        public Task<Domain.Entities.Core.Task> LinkTaskColab(int taskId, Task_Collaborator_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkTaskColab(int taskId, int taskColabId);

        public Task<Domain.Entities.Core.Task> LinkMetaTask(int taskId, TaskOptions options);

        public System.Threading.Tasks.Task UnlinkMetaTask(int taskId, TaskOptions options);

        public Task<List<TaskMetaLinkVM>> GetLinkedMetaTasks(int metaTaskId);

        public Task<List<Task_Step>> GetTask_StepsAsync(int taskId);

        public Task<Task_Step> CreateStepAsync(int taskId, Task_StepCreateOptions options);

        public Task<int> GetTaskStepNumber(int id);

        public Task<List<SubdutyArea>> getAllSubDutyAreas();

        public Task<Task_Step> UpdateStepAsync(int taskId, int stepNumber, Task_StepUpdateOptions options);

        public System.Threading.Tasks.Task RemoveStepAsync(int taskId, int stepNumber);

        public System.Threading.Tasks.Task ActivateStepAsync(int taskId, int stepNumber);

        public System.Threading.Tasks.Task DeactivateStepAsync(int taskId, int stepNumber);

        public Task<List<Domain.Entities.Core.Task>> getUsingSDAId(int id);

        public Task<TaskStatsCount> GetTaskLinkedStats(int taskId);

        public Task<TaskStatsCount> GetLinkedMetaStatsAsync(int taskId);

        public Task<List<TaskWithNumberVM>> GetTasksWithSDAIdAsync(int sdaId);

        public Task<List<SubdutyArea>> GetSDAWithNumberBydaId(int daId);

        public Task<TaskStatsCount> GetTaskNotLinkedStats();

        public Task<List<string>> GetLinkedIds(string option);

        public Task<Task_Suggestion> CreateTaskSuggestionAsync(int taskId, Task_SuggestionOptions options);

        public Task<List<Task_Suggestion>> GetAllSuggestionsAsync(int taskId);

        public Task<int> GetSuggestionNumberAsync(int taskId);

        public System.Threading.Tasks.Task UpdateSuggestionAsync(int taskId, int suggestionId, Task_SuggestionOptions options);

        public System.Threading.Tasks.Task DeleteSuggestionAsync(int taskId, int suggestionId);

        public Task<List<TrainingGroup>> GetLinkedTrainingGroups(int id);

        public System.Threading.Tasks.Task LinkTrainingGroupsAsync(int id, Task_TrainingGroupOptions options);

        public System.Threading.Tasks.Task UnlinkTrainingGroupsAsync(int id, Task_TrainingGroupOptions options);

        public Task<TaskNumberVM> GetTaskNumberWithLetter(int id, int? taskId = null);

        public Task<List<TaskWithNumberVM>> GetPendingTasks();

        public Task<bool> DAHasTaskWithLinks(int daId);

        public Task<bool> SDAHasTaskWithLinks(int sdaId);

        public Task<bool> TaskHasLinks(int taskId);

        public Task<List<Tool>> GetMetaToolsDataAsync(int taskId);

        public Task<List<MetaTask_QuestionsVM>> GetMetaTaskQuestionDataAsync(int taskId);

        public Task<List<MetaTask_SuggestionsVM>> GetMetaTaskSuggestionsAsync(int taskId);

        public Task<List<TrainingGroup>> GetMetaTaskTrainingGroups(int taskId);

        public Task<MetaTaskOJTVM> GetCondCritRefForMetaAsync(int id);

        public Task<TaskRequalVM> GetRequalInfoAsync(int id);

        public Task<bool> CanTaskBeDeactivatedAsync(int id);

        public Task<List<Task>> GetTaskActiveInactive(string option);
        public Task<List<TaskWithPositionCompactVM>> GetTasksWithDutySubDutyAreaAsync();
        public Task<List<DutyAreaTreeVM>> GetTaskTreeDataByPositionsAsync(List<int> posId);

        public Task<List<ToolDataVM>> GetToolsDataByTaskIdAsync(int taskId);

        public System.Threading.Tasks.Task UpdateTaskAndVersionTaskAsync(VersionTaskModel options);

    }
}
