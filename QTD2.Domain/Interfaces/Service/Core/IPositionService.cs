using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IPositionService : IService<Position>
    {
        System.Threading.Tasks.Task<IEnumerable<Position>> GetByIdListAsync(IEnumerable<int> positionIds);
        System.Threading.Tasks.Task<List<Position>> GetForMyDataPositionDetailsAsync(List<int> positionIds, string filterType, string version);
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> GetTaskRequalificationByPositionAsync(string activePositions, bool includeCustomTasks, bool includeTrainees, DateTime startDate, DateTime endDate);
        System.Threading.Tasks.Task<List<Position>> GetAllActiveCompactPositions();
        System.Threading.Tasks.Task<List<Position>> GetAllTrainingSummaryByPositionAsync(List<int> positionIds, List<int> organizationIds);
        System.Threading.Tasks.Task<List<Position>> GetAllOJTGuideByPositionAsync(int positionId);
        System.Threading.Tasks.Task<List<Position>> GetPositionsByIdsAsync(int positionId);
        System.Threading.Tasks.Task<List<Position>> GetPositionTasksByIdAsync(List<int> positionIds);
        System.Threading.Tasks.Task<List<Position>> GetAllCompactPositions();
        System.Threading.Tasks.Task<List<Position>> GetTaskRequalificationByPositionAsync(List<int> positionIds,bool includeTrainees);
        System.Threading.Tasks.Task<Position> GetCompactPositions(int posId);

        System.Threading.Tasks.Task<int> GetPositionsNotLinkedToTaskCount();
        System.Threading.Tasks.Task<int> GetPositionsNotLinkedToSQsCount();
        System.Threading.Tasks.Task<int> GetPositionsNotLinkedEMPCount();
        System.Threading.Tasks.Task<List<Position>> GetPositionsAsync();
        System.Threading.Tasks.Task<string> GetPositionTitleByIdAsync(int positionId);
        System.Threading.Tasks.Task<List<Position>> GetAllActivePositionsWithPosTitleAsync();
        System.Threading.Tasks.Task<Position> GetAnnualPositionsTaskListReviewAsync(int positionId, DateTime startDate, DateTime endDate, bool includePsuedoTasks,bool includeRRPositions);
        System.Threading.Tasks.Task<List<Position>> GetPositionsByIdsAsync(List<int> positionIds);
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> GetPositionTasksByIdsAsync(List<int> positionIds);
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> InitialTrainingByPositionAsync(string trainingProgramId, bool includeInactiveILA);    
        System.Threading.Tasks.Task<List<Position>> GetPositionTaskHistoryAsync(List<int> positionIds);
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> TrainingProgramCompletionHistoryAsync(List<int> trainingProgramId, List<DateTime> dateRanges, bool includeInActiveIla);
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Position>> GetEmployeesByPositionAsync(List<int> positionIDs,bool includeCurrentPosition,string activeStatus, bool includeTrainee);
        System.Threading.Tasks.Task<List<Position>> GetPositionsForTasksMetbyPosition(List<int> positionIds, string allActiveInactiveOnlyEmployees, bool currentPositionsOnly, bool includeTrainees);
        System.Threading.Tasks.Task<List<Position>> GetPositionsForProcedureAndRegulatoryRequirementTrainingSummarybyPosition(List<int> positionIds);
        Task<List<Position>> GetILAsByPositionAsync(List<int> positionIds);
        Task<List<Position>> GetForSafetyHazardsByPositionMatrix(List<int> positionIds);
        Task<List<Position>> GetForEnablingObjectivesByPositionMatrixAsync(List<int> positionIds);
        Task<List<Position>> GetPositionSqsWithEOAsync(List<int> positionIds);
        public System.Threading.Tasks.Task<List<Position>> GetPositionsByIdAsync(List<int> positionIds, string trainingProgramId);
        public Task<Position> GetPositionByNameAsync(string positionName);
    }
}
