using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITaskService : Common.IService<Entities.Core.Task>
    {
        public System.Threading.Tasks.Task<IEnumerable<Task>> GetByPositionListAsync(IEnumerable<int> positionIds);
        public System.Threading.Tasks.Task<IEnumerable<Task>> GetByTaskGroupAsync(int taskGroupId, bool includeGroups);
        public System.Threading.Tasks.Task<List<Task>> GetLinksForMyDataPositionLinkageAsync(string activePositions, string taskStatus, string employeeStatus, DateTime employeeStartDate, DateTime employeeEndDate);
        public System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Task>> GetTaskQualificationEvaluatorAsync(string activeStatus);
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Task>> GetByListOfIds(List<int> list);
        public System.Threading.Tasks.Task<IEnumerable<Task>> GetTasksByTaskIdsAsync(List<int> taskIds);
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Task>> GetAllTasksOrderByNumber();
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Task>> GetTasksAsync();
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Task>> GetMinimizedTaskForTree();
        System.Threading.Tasks.Task<Task> GetTaskByIdAsync(int taskId);
        System.Threading.Tasks.Task<List<Task>> GetEnablingObjectivesByTaskAsync(List<int> taskIds,bool isRR = false);
        System.Threading.Tasks.Task<List<Task>> GetILAsLinkedToTaskAndEoAsync(List<int> taskIds,bool includeILAsOfEOTask,bool showTrainingResources);
        System.Threading.Tasks.Task<IEnumerable<Task>> GetTaskDetailsByTaskIdsAsync(List<int>positionIds,List<int> taskIds, bool includePseudoTasks, string tasksType, string activeInactive,bool rrTasksOnly, List<int> trIds);
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Task>> GetTasksWithPositionTasksAsync();
        public System.Threading.Tasks.Task<List<Task>> GetTasksWithDutySubDutyAreaByTaskIdsAsync(List<int> taskIds);
        public System.Threading.Tasks.Task<List<Task>> GetTasksWithDutySubDutyAreaPositionTaskPositionsByTaskIdsAsync(List<int> taskIds);
        public System.Threading.Tasks.Task<List<Task>> GetTasksHistoryByTaskIdsAsync(List<int> taskIds);
        public System.Threading.Tasks.Task<List<Task>> GetTasksForEmployeeTaskQualificationDatesByTaskGenerator(List<int> taskIds, bool rrTasksOnly);
        public System.Threading.Tasks.Task<List<Task>> GetTasksForEmployeeTaskQualificationRecordsForGivenPositionGenerator(List<int> taskIds, bool reliabilityRelatedTasksOnly, bool includeInactiveTasks, bool includePseudoTasks);
        System.Threading.Tasks.Task<List<Task>> GetProceduresByTaskAsync(List<int> taskIds, bool includeInactive);
        System.Threading.Tasks.Task<List<Task>> GetILAsByTaskAsync(List<int> taskIds, bool includeUnlinkedTasks);
        System.Threading.Tasks.Task<List<Task>> GetTasksListBySubDutyAreaIdAsync(int subDutyAreaId);
        System.Threading.Tasks.Task<List<Task>> GetTasksNotLinkedToILAAsync(string activeStatus, bool reliabilityRelatedTasksOnly, bool includeMetaTasks, bool includePseudoTasks);
        System.Threading.Tasks.Task<List<Task>> GetTasksByProcedureAsync(List<int> taskIds, bool reliabilityRelatedTasksOnly, bool includeInactiveTasks);
        System.Threading.Tasks.Task<List<Task>> GetTasksByIdsAsync(List<int> taskIds, bool reliabilityRelatedTasksOnly, bool includeMetaTasks, bool includeInactiveTasks, bool includePseudoTasks);
        System.Threading.Tasks.Task<List<Task>> GetTasksNotLinkedToPositionAsync(string activeStatus, bool reliabilityRelatedTasksOnly, bool includeMetaTasks, bool includePseudoTasks);
        public System.Threading.Tasks.Task<List<Task>> GetSafetyHazardsByTaskIdsAsync(List<int> taskIds);
        System.Threading.Tasks.Task<List<Task>> GetEnablingObjectivesMetaEosSQsByTaskAsync(List<int> taskIds, List<int> includeObjectivesLinkIds);
        System.Threading.Tasks.Task<List<Task>> GetEnablingObjectivesMetaEosLinksByTaskIdsAsync(List<int> taskIds);
        System.Threading.Tasks.Task<List<Task>> GetTasksWithoutTaskTrainingGroupsAsync(bool includeRRTasks, bool includeInactiveTasks, bool includePseudoTasks);
        System.Threading.Tasks.Task<List<Entities.Core.Task>> GetTasksByTrainingTaskGroupIdsAsync(List<int> trIds, bool includeRRTasks, bool includeInactiveTasks, bool includePseudoTasks);
        System.Threading.Tasks.Task<List<Entities.Core.Task>> GetTasksByIdAsync(List<int> taskIds, bool includeMetaTasks, bool includePseudoTasks);
        System.Threading.Tasks.Task<List<Entities.Core.Task>> GetTasksByIdsAndDatesAsync(List<int> taskIds, DateTime startDate, DateTime endDate, bool includeRRTasks);
        System.Threading.Tasks.Task<Entities.Core.Task> GetForCopyAsync(int taskId);
    }
}
