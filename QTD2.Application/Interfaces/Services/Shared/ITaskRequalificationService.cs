using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.FilterOptions;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_Requalification;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskRequalificationService
    {
        public Task<TaskQualificationWithEvalsVM> GetAsync(int qualificationId);

        public Task<TaskQualification> UpdateAsync(int id, TaskQualificationCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public Task<List<TaskQualificationEmpVM>> GetAllQualificationsForEmp(int empId, int taskId);

        public Task<List<TaskReQualificationTabVM>> FilterByPositionAsync(TaskRequalificationFilterOptions options);

        public Task<List<TaskReQualificationTabVM>> FilterByTaskAsync(TaskRequalificationFilterOptions options);

        public Task<TaskReQualificationTabVM> GetTaskWithNumberAsync(int id);

        public Task<List<TaskQualificationWithoutPosDateVM>> GetPendingQualifications();

        public Task<List<TaskQualificationWithoutPosDateVM>> GetEmployeesWithoutTQRecordsAsync();

        public Task<List<TaskQualificationEmpVM>> GetEmpLinkedToTaskAsync(int id);

        public Task<List<TaskReQualificationTabVM>> FilterBySQAsync(TaskRequalificationFilterOptions options);

        public Task<List<TQEvaluatorWithCountVM>> GetEvaluatorWithCount();

        public Task<List<TQEmpWithTasksVM>> GetEmpWithTasksForTQEvaluator(int id);

        public Task<List<TaskWithNumberVM>> GetRequalTasksForEval(int id);

        public Task<List<TaskWithNumberVM>> GetRequalTasksForEmp(int id);

        public System.Threading.Tasks.Task RemoveEvaluatorAsync(int id);

        public Task<List<TaskReQualificationTabVM>> FilterByEMPAsync(TaskRequalificationFilterOptions options);

        public Task<List<TaskReQualificationTabVM>> FilterByEvalAsync(TaskRequalificationFilterOptions options);

        public Task<List<TaskReQualificationTabVM>> FilterByTGAsync(TaskRequalificationFilterOptions options);

        public Task<List<TaskQualificationWithoutPosDateVM>> GetEmpsWithoutPosQualDateAsync();

        public Task<TaskQualificationStatsVM> GetStatsAsync();

        public Task<List<TQReleasedToEMPVM>> GetTQReleasedToEMP();

        public Task<List<DutyArea>> GetTaskTreeDataWithPositionIdsAsync(EMPFilterOptions option);
        public Task<List<EnablingObjective>> GetEoTreeWithPositionIds(EMPFilterOptions option);

        public Task<List<Employee>> GetEmpDataForPositionsAsync(EMPFilterOptions options);

        public Task<TaskQualification> CreateTaskQualificationAsync(TaskQualificationCreateOptions options);

        public Task<List<TaskQualificationEmpVM>> GetRecentTQAsync();

        public System.Threading.Tasks.Task CreateAndReleaseTaskAndSkillQualifications(TQReleaseByTaskAndSkillOptions options);

        public System.Threading.Tasks.Task UpdateReleasedTQ(ReleasedTQAndSQUpdateOptions options);

        public System.Threading.Tasks.Task ReassignTaskQualification(ReassignTQVM options);

        public Task<List<TaskQualificationEmpVM>> GetTQLinkedToEMPAsync(int empId);

        public Task<List<TaskQualificationPengingEvaluatorVM>> GetEmployeePendingTaskRequalification();

     //   public Task<List<TQTasksWithEmployeesVM>> GetEmpWithTasksForTQEvaluatorEmpTaskView();

        //NEW application services

        public Task<List<TaskQualificationEmpVM>> GetPendingTaskRequalificationByEmpId(int employeeId);

        public Task<List<TQEmpWithPosAndTaskVM>> GetPendingTaskQualificationsAsTraineeAsync(int employeeId);

        public Task<List<TQEmpWithPosAndTaskVM>> GetCompletedTaskQualificationsAsTraineeAsync(int employeeId);
        public Task<List<TQEmpWithPosAndTaskVM>> GetCompletedTaskQualificationsAsEvaluatorAsync(int employeeId);
        public Task<bool> GetTQEvaluatorBitAsync(int employeeId);
    }
}
