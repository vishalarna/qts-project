using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TaskQualificationService : Common.Service<TaskQualification>, ITaskQualificationService
    {
        public TaskQualificationService(ITaskQualificationRepository repository, ITaskQualificationValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<TaskQualification>> GetByEvaluatorAsync(List<int> evaluatorsToFilter)
        {
            var taskQuals = await FindWithIncludeAsync(r => r.TaskQualification_Evaluator_Links.Any(s => evaluatorsToFilter.Contains(s.EvaluatorId)), new[] { "Employee.Person", "TaskQualification_Evaluator_Links" });
            return taskQuals.ToList();
        }

        public async Task<List<TaskQualification>> GetTaskQualificationByTaskIdAsync(List<int> tasksId)
        {
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();
            predicates.Add(r => tasksId.Contains(r.TaskId));
            var employeeTasks = (await FindWithIncludeAsync(predicates, new[] { "TQEmpSetting" })).ToList();
            return employeeTasks;
        }

        public async System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationEvaluatorAsync(List<int> employeeIds, bool showAssignedAndPendingQualifications, bool showCompletedTaskQualifications)
        {
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();

            if (employeeIds != null)
                predicates.Add(r => employeeIds.Contains(r.EmpId));

            if(showAssignedAndPendingQualifications)
            {
                predicates.Add(r => r.TQStatusId == 3);
            }

            if (showCompletedTaskQualifications) { }

            return (await FindWithIncludeAsync(predicates, new string[] { "TaskQualification_Evaluator_Links.Evaluator.Person", "Employee.Person", "Task.SubdutyArea.DutyArea" })).ToList();
        }

        public async System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationRecordsAsync(List<int> employeeIds,List<DateTime>dateRange, bool includePesudoTask, bool includeTrainees)
        {
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();
            List<Domain.Entities.Core.Task> tasksToExclude = new List<Domain.Entities.Core.Task>();
            if (employeeIds != null)
                predicates.Add(r => employeeIds.Contains(r.EmpId));
            if (!includeTrainees)
            {
                predicates.Add(r => !r.Employee.EmployeePositions.Any(ep => ep.Trainee == true));
            }
            if (dateRange.Any())
            {
                var startDate = dateRange[0].Date;
                var endDate = dateRange[1].Date.AddDays(1);

                predicates.Add(r => r.TaskQualificationDate.HasValue && r.TaskQualificationDate.Value >= startDate &&  r.TaskQualificationDate.Value < endDate);
            }
            var taskQualification = await FindWithIncludeAsync(predicates, new[] { "Employee.Person", "Task", "EvaluationMethod", "Employee.EmployeePositions", "TaskQualification_Evaluator_Links.Evaluator.Person", "TaskReQualificationEmp_SignOff.EvaluationMethod","ClassSchedule.ClassSchedule_Employee","ClassSchedule.ILA" });
            return taskQualification.Where(r=>r.IsComplete).ToList();
        }

        public async System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationRecordsByEmployeeIdsAsync(List<int> employeeIds, bool includeEvaluatorAndModeofQualification)
        {
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();
            if (employeeIds != null)
                predicates.Add(r => employeeIds.Contains(r.EmpId));
            var taskQualification = new List<TaskQualification>();
            if (includeEvaluatorAndModeofQualification)
            {
                taskQualification = (await FindWithIncludeAsync(predicates, new[] { "TaskReQualificationEmp_SignOff.EvaluationMethod", "EvaluationMethod", "TaskReQualificationEmp_SignOff.Evaluator.Person", "Task.Task_EnablingObjective_Links", "TaskQualification_Evaluator_Links" },true)).ToList();
            }
            else
            {
                taskQualification = (await FindWithIncludeAsync(predicates, new[] { "Task.Task_EnablingObjective_Links" })).ToList();
            }
            return taskQualification;
        }

        public async Task<List<TaskQualification>> GetTQsWithConditionAndIncludes(Expression<Func<TaskQualification, bool>> predicate, string[] includes)
        {
            if (includes != null && includes.Length > 0)
            {
                var tqs = await FindQueryWithIncludeAsync(predicate, includes).ToListAsync();
                return tqs;
            }
            else
            {
                var tqs = await FindAsync(predicate);
                return tqs.ToList();
            }
        }

        public async Task<List<TaskQualification>> GetCompactTQsWithConditionAndIncludes(Expression<Func<TaskQualification, bool>> predicate, string[] includes)
        {
            if (includes != null && includes.Length > 0)
            {
                var tqs = await FindQueryWithIncludeAsync(predicate, includes).Select(s => new TaskQualification
                {
                    Active = s.Active,
                    Id = s.Id,
                    EmpId = s.EmpId,
                    TaskId = s.TaskId
                }).ToListAsync();
                return tqs;
            }
            else
            {
                var tqs = await FindAsync(predicate);
                return tqs.Select(s => new TaskQualification
                {
                    Active = s.Active,
                    Id = s.Id,
                    EmpId = s.EmpId,
                    TaskId = s.TaskId
                }).ToList();
            }
        }

        public async Task<TaskQualification> GetForNotificationAsync(int taskQualificationId)
        {
            var tqs = await FindWithIncludeAsync(r => r.Id == taskQualificationId, new[] { "Employee.Person", "EvaluationMethod", "TaskQualification_Evaluator_Links.Evaluator.Person", "Task.SubdutyArea.DutyArea" });
            return tqs.First();
        }

        public async Task<List<TaskQualification>> GetPendingTaskQualifications()
        {
            var tqs = await FindWithIncludeAsync(r => r.IsReleasedToEMP && !r.CriteriaMet && r.DueDate > DateTime.Now, new[] { "Employee", "TaskQualification_Evaluator_Links.Evaluator" });
            return tqs.ToList();
        }

        public async Task<List<TaskQualification>> GetPendingTaskQualificationsListAsTraineeByEmpId(int employeeId)
        {
            var pendingTQ = (await FindWithIncludeAsync(x => x.EmpId == employeeId && x.Active && x.IsReleasedToEMP && !x.IsRecalled, new string[] { "TaskQualification_Evaluator_Links" }));
            return pendingTQ.Where(x => x.IsPending).ToList();
        }

        public async Task<List<TaskQualification>> GetCompletedTaskQualificationsListAsTraineeByEmpId(int employeeId)
        {
            return (await FindAsync(x => x.EmpId == employeeId)).Where(x=>x.IsComplete).ToList();
        }

        public async Task<List<TaskQualification>> GetCompletedTaskQualificationsListAsEvalByEmpId(int employeeId)
        {
            return (await FindAsync(x => x.TaskQualification_Evaluator_Links.Any(y => y.EvaluatorId == employeeId))).Where(x=>x.IsComplete).ToList();
        }

        public async System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationsListByEmpId(int employeeId, DateTime date)
        {
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();
            if (employeeId > 0)
                predicates.Add(r => r.EmpId == employeeId && r.CriteriaMet == false && (r.DueDate > DateTime.Today || r.DueDate == null));

            var taskQualList = await FindWithIncludeAsync(predicates, new string[] { "TQEmpSetting", "TaskQualificationStatus" });
            return taskQualList.ToList();
        }

        public async Task<string> GetEmployeeNamebyIdAsync(int taskQualificationId)
        {
            var taskQualification = await GetWithIncludeAsync(taskQualificationId, new[] { "Employee.Person" });
            return taskQualification != null ? taskQualification?.Employee?.Person?.FirstName + " " + taskQualification?.Employee?.Person?.LastName : null;
        }

        public async System.Threading.Tasks.Task<TaskQualification> GetTaskQualificationAsync(int? ClassScheduleId, int? employeeId, int? sequence)
       {
          
            var tqs = await FindWithIncludeAsync(r => r.ClassScheduleId == ClassScheduleId && r.Sequence == sequence && r.EmpId == employeeId, new[] { "Employee" });

           return tqs.FirstOrDefault();
       }

        public async Task<List<TaskQualification>> GetRecentTaskQualificationsAsync()
        {
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();
            predicates.Add(x => (x.ModifiedDate == null ? (x.CreatedDate == null ? true : DateTime.Compare(x.CreatedDate.Value.Date, DateTime.Now.Date.AddMonths(-5)) >= 0) : DateTime.Compare(x.ModifiedDate.Value.Date, DateTime.Now.Date.AddMonths(-5)) >= 0));
            predicates.Add(x => x.DueDate.HasValue == true);
            predicates.Add(x => !x.IsRecalled);
            return (await FindWithIncludeAsync( predicates, new string[] { "TaskQualification_Evaluator_Links", "TaskQualificationStatus"},true)).ToList();
        }

		public async Task<List<TaskQualification>> GetTaskQualificationsForEMPTaskQualificationDetails(List<int> employeeIds, List<int> positionIds, List<int> taskIds, string taskQualificationStatus)
		{
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();
            
            predicates.Add(tq => employeeIds.Contains(tq.EmpId));

            if (positionIds.Count > 0) {
                predicates.Add(tq => tq.Task.Position_Tasks.Any(pt => positionIds.Contains(pt.PositionId) && pt.Active));
            }

            if (taskIds.Count > 0) {
                predicates.Add(tq => taskIds.Contains(tq.TaskId));
            }
           
            if (taskQualificationStatus == "Completed Only")
            {
                predicates.Add(tq => tq.CriteriaMet);
            }
            else if (taskQualificationStatus == "Not Completed Only")
            {
                predicates.Add(tq => !tq.CriteriaMet);
            }

            var taskQualifications = await FindWithIncludeAsync(predicates, new string[] {
                "Task.Position_Tasks",
                "Employee.Person",
                "Employee.EmployeePositions.Position",
                "EvaluationMethod",
                "Task.Task_Steps",
                "Task.Task_MetaTask_Links.Task.Task_Steps",
                "TaskQualification_Evaluator_Links.Evaluator.Person",
                "TaskReQualificationEmp_Steps",
                "TaskReQualificationEmp_SignOff.EvaluationMethod",
                "ClassSchedule.ClassSchedule_Employee",
                "ClassSchedule.ILA"
            });

            return taskQualifications.ToList();
		}

        public async System.Threading.Tasks.Task<List<TaskQualification>> GetTaskQualificationsByEmpIds(List<int> employeeIds)
        {
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();
            predicates.Add(r => employeeIds.Contains(r.EmpId));
            var taskQuals = (await FindWithIncludeAsync(predicates, new string[] { "TaskQualificationStatus" },true)).ToList();
            return taskQuals;
        }

        public async Task<List<TaskQualification>> GetByTaskIdAsync(int taskId)
        {
            var taskQuals = await FindAsync(r => r.TaskId == taskId);
            return taskQuals.ToList();
        }

        public async Task<List<TaskQualification>> GetTaskQualificationsForEmployeeTaskQualificationDatesByTaskGenerator(List<int> taskIds)
        {
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();
            predicates.Add(r => taskIds.Contains(r.TaskId));
            var taskQualifications = (await FindAsync(predicates, true)).Where(x=>x.IsComplete && x.CriteriaMet).ToList();
            return taskQualifications;
        }

        public async Task<List<TaskQualification>> GetTaskQualificationsForEmployeeTaskQualificationRecordsForGivenPositionGenerator(List<int> employeeIds, List<int> taskIds, List<DateTime> dateRange)
        {
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();
            predicates.Add(r => employeeIds.Contains(r.EmpId));
            predicates.Add(r => taskIds.Contains(r.TaskId));
            if (dateRange.Any())
            {
                var startDate = dateRange[0].Date;
                var endDate = dateRange[1].Date.AddDays(1);

                predicates.Add(r => r.TaskQualificationDate.HasValue && r.TaskQualificationDate.Value >= startDate && r.TaskQualificationDate.Value < endDate);
            }
            var taskQualifications = (await FindWithIncludeAsync(predicates, new string[] { "EvaluationMethod", "TaskQualification_Evaluator_Links", "TaskReQualificationEmp_SignOff.EvaluationMethod", "ClassSchedule.ClassSchedule_Employee", "ClassSchedule.ILA" }, true)).ToList();
            return taskQualifications.Where(r => r.IsComplete && r.TaskQualificationDate != null).ToList();
        }

        public async Task<List<TaskQualification>> GetTaskQualificationsDashboardCountByEmpId(int employeeId)
        {
            var pendingTqs =  (await FindWithIncludeAsync(x => x.EmpId == employeeId && x.Active && x.IsReleasedToEMP, new string[] { "TaskReQualificationEmp_SignOff", "TQEmpSetting" }));
            return pendingTqs.Where(x => x.IsPending).ToList();
        }

        public async Task<List<TaskQualification>> GetTaskQualificationsForSummaryOfTaskQualificationBySubDutyAreaGeneratorAsync(List<int> employeeIds, List<int> taskIds)
        {
            List<Expression<Func<TaskQualification, bool>>> predicates = new List<Expression<Func<TaskQualification, bool>>>();
            predicates.Add(r => employeeIds.Contains(r.EmpId));
            predicates.Add(r => taskIds.Contains(r.TaskId));
            var taskQualifications = (await FindWithIncludeAsync(predicates, new string[] { "EvaluationMethod", "TaskQualification_Evaluator_Links.Evaluator.Person", "TaskReQualificationEmp_SignOff.EvaluationMethod" }, true)).ToList();
            return taskQualifications.Where(r => r.CriteriaMet && r.TaskQualificationDate != null).ToList();
        }

        public async Task<List<TaskQualification>> GetPendingTaskQualificationsByILAIdAsync(int ilaId)
        {
            return (await FindAsync(x => x.ClassSchedule.ILAID == ilaId && x.TaskQualificationStatus.Name == "Pending" && !x.IsRecalled && x.IsReleasedToEMP && x.Active)).ToList();
        }

        public async Task<List<TaskQualification>> GetPendingTaskQualificationsByILAIdAndEmpIdsAsync(int ilaId, List<int> empIds)
        {
            return (await FindWithIncludeAsync(x => x.ClassSchedule.ILAID == ilaId && x.TaskQualificationStatus.Name == "Pending" && !x.IsRecalled && x.IsReleasedToEMP && x.Active && empIds.Contains(x.EmpId), new[] { "TaskQualification_Evaluator_Links.Evaluator.Person", "TaskReQualificationEmp_SignOff" } )).ToList();
        }
    }
}
