using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEnablingObjectiveService : Common.IService<EnablingObjective>
    {
        public System.Threading.Tasks.Task<List<EnablingObjective>> GetLinksForMyDataPositionLinkage(string activePositions, string eoStatus, string eoFlaggedAsSkill, bool includeMetaEOs, string employeeStatus, DateTime employeeStartDate, DateTime employeeEndDate);
        Task<List<EnablingObjective>> GetEnablingObjectivesLinkedToTaskAsync(int id);
        Task<List<EnablingObjective>> GetEnablingObjectivesLinkedToProcedureAsync(int id);

        Task<List<EnablingObjective>> GetItemByEnablingObjectiveAsync(List<int> objectivesIDs);

        Task<List<EnablingObjective>> GetMinimalEOData();

        Task<List<EnablingObjective>> GetCompactedEOData();

        public System.Threading.Tasks.Task<List<EnablingObjective>> GetEnablingObjectivesByEOIDs(List<int> enablingObjectiveIDs);
        public Task<List<EnablingObjective>> GetEnablingObjectivesLinkedToILAAsync(int iLAId);
        public Task<List<EnablingObjective>> GetEnablingObjectivesByIDs(List<int> enablingObjectiveIDs);
        public Task<List<EnablingObjective>> GetILAsLinkedToEnablingObjectives(List<int> enablingObjectiveIDs, bool showTrainingResources);
        public Task<List<EnablingObjective>> GetEnablingObjectivesNotLinkedToTaskAsync(string activeStatus, bool includeMetoEOs, bool includeSkillQualifications);
        Task<List<EnablingObjective>> GetEnablingObjectivesNotLinkedToILAAsync(string activeStatus, bool includeMetoEOs, bool includeSkillQualifications);
        Task<List<EnablingObjective>> GetEnablingObjectivesByCategoriesAsync(List<int> categoryIds, string activeStatus, bool showMetaEosOnly, bool showSkillQualificationsOnly);
        Task<List<EnablingObjective>> GetTasksByEnablingObjectivesAsync(List<int> enablingObjectiveIds);
        Task<List<EnablingObjective>> GetEnablingObjectivesByPositionOrSkillAsync(int positionId, List<int> enablingObjectiveIds, string status);
        Task<List<EnablingObjective>> GetForSkillQualificationAssessmentByPositionOrTask(int positionId, int taskId, string status);
        Task<List<EnablingObjective>> GetILAsByEnablingObjectiveAsync(List<int> enablingObjectiveIds, bool includeUnlinkedEos);
        Task<List<EnablingObjective>> GetEnablingObjectivesAsync();
        Task<List<EnablingObjective>> GetSQEnablingObjectivesAsync();
        Task<List<EnablingObjective>> GetEnablingObjectivesAllDataByEoIdAsync(List<int> eoIds);
        Task<List<EnablingObjective>> GetEnablingObjectivesByIdAsync(List<int> eoIds, bool includeMetoEOs, bool includeSkillQualifications, bool includeInactiveEnablingObjectives);
        Task<List<EnablingObjective>> GetEnablingObjectivesBySafetyHazardAsync(List<int> eoIds, bool includeMetaEnablingObjectives, bool includeSkillQualifications, bool includeInactiveEnablingObjectives);
        Task<EnablingObjective> GetForCopy(int id);
        public Task<List<EnablingObjective>> GetProcedureLinkedEnablingObjectivesAsync(List<int> enablingObjectiveIds);
        public Task<List<EnablingObjective>> GetEOAsync();
        public Task<List<EnablingObjective>> GetEOByIdAsync(int eoId);
        public Task<List<EnablingObjective_Suggestion>> GetAllSuggestionByIdAsync(int eoId);
        public Task<List<EnablingObjective_Step>> GetAllStepAsync(int eoId);
        public Task<List<EnablingObjective_Question>> GetAllQuestionByIdAsync(int eoId);
        public Task<EnablingObjective> GetEnablingObjectiveByIdAsync(int eoId);
        public Task<EnablingObjective> GetMetaEnablingObjectiveAsync(int eoId);
    }
}
