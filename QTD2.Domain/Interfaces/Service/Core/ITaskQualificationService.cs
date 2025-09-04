using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITaskQualificationService : Common.IService<TaskQualification>
    {
        System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationRecordsAsync(List<int> employeeIds, List<DateTime> dateRange, bool includePesudoTask, bool includeTrainees);
        System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationEvaluatorAsync(List<int> employeeIds, bool showAssignedAndPendingQualifications, bool showCompletedTaskQualifications);
        Task<List<TaskQualification>> GetByEvaluatorAsync(List<int> evaluatorsToFilter);
        Task<List<TaskQualification>> GetTaskQualificationByTaskIdAsync(List<int> tasksId);
        Task<List<TaskQualification>> GetTQsWithConditionAndIncludes(Expression<Func<TaskQualification,bool>> predicate, string[] includes);
        Task<List<TaskQualification>> GetCompactTQsWithConditionAndIncludes(Expression<Func<TaskQualification,bool>> predicate, string[] includes);
        System.Threading.Tasks.Task<TaskQualification> GetForNotificationAsync(int taskQualificationId);
        Task<List<TaskQualification>> GetPendingTaskQualifications();
        System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationsListByEmpId(int employeeId, DateTime date);
        System.Threading.Tasks.Task<List<TaskQualification>> GetPendingTaskQualificationsListAsTraineeByEmpId(int employeeId);
        System.Threading.Tasks.Task<List<TaskQualification>> GetCompletedTaskQualificationsListAsTraineeByEmpId(int employeeId);
        System.Threading.Tasks.Task<List<TaskQualification>> GetCompletedTaskQualificationsListAsEvalByEmpId(int employeeId);
        System.Threading.Tasks.Task<string> GetEmployeeNamebyIdAsync(int taskQualificationId);
        System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationRecordsByEmployeeIdsAsync(List<int> employeeIds, bool includeEvaluatorAndModeofQualification);
        System.Threading.Tasks.Task<TaskQualification> GetTaskQualificationAsync(int? ClassScheduleId,int? employeeId, int? sequence);
        Task<List<TaskQualification>> GetRecentTaskQualificationsAsync();
        Task<List<TaskQualification>> GetTaskQualificationsForEMPTaskQualificationDetails(List<int> employeeIds, List<int> positionIds, List<int> taskIds, string qualificationStatus);
        System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationsByEmpIds(List<int> employeeIds);
        System.Threading.Tasks.Task<List<TaskQualification>> GetByTaskIdAsync(int taskId);
        System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationsForEmployeeTaskQualificationDatesByTaskGenerator(List<int> taskIds);
        System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationsForEmployeeTaskQualificationRecordsForGivenPositionGenerator(List<int> employeeIds, List<int> taskIds, List<DateTime> dateRange);
        System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationsDashboardCountByEmpId(int employeeId);
        System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationsForSummaryOfTaskQualificationBySubDutyAreaGeneratorAsync(List<int> employeeIds, List<int> taskIds);
        Task<List<TaskQualification>> GetPendingTaskQualificationsByILAIdAsync(int ilaId);
        Task<List<TaskQualification>> GetPendingTaskQualificationsByILAIdAndEmpIdsAsync(int ilaId,List<int>empIds);
    }
}
