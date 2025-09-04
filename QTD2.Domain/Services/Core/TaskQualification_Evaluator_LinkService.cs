using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TaskQualification_Evaluator_LinkService : Common.Service<TaskQualification_Evaluator_Link>, ITaskQualification_Evaluator_LinkService
    {
        public TaskQualification_Evaluator_LinkService(ITaskQualification_Evaluator_LinkRepository repository, ITaskQualification_Evaluator_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<TaskQualification_Evaluator_Link> GetForNotificationAsync(int id)
        {
            var tq = await FindWithIncludeAsync(r => r.Id == id, new[] { "Evaluator.Person", "TaskQualification.Task.SubdutyArea.DutyArea", "TaskQualification.Employee.Person" });
            return tq.First();
        }

        public async Task<List<TaskQualification_Evaluator_Link>> GetPendingTaskQualificationsByEvaluator(int evaluatorId)
        {
            var tq_evals = await FindWithIncludeAsync(
                                                    r => r.EvaluatorId == evaluatorId && r.Active
                                                    && r.TaskQualification.IsReleasedToEMP && !r.TaskQualification.IsRecalled
                                                    && !r.TaskQualification.CriteriaMet
                                                    && !(r.TaskQualification.TaskReQualificationEmp_SignOff.Where(s => s.EvaluatorId == evaluatorId && (s.IsCompleted ?? false)).Any())
                                                    && r.TaskQualification.TaskQualificationDate == null && r.TaskQualification.DueDate > DateTime.Now
                                                    , 
                                                new[] { 
                                                "Evaluator.Person"
                                                , "TaskQualification.Employee.Person"
                                                , "TaskQualification.Employee.EmployeePositions.Position"
                                                , "TaskQualification.TQEmpSetting"
                                                , "TaskQualification.TaskReQualificationEmp_SignOff"
                                                , "TaskQualification.TaskQualificationStatus"
                                                });

            return tq_evals.Where(r=>r.TaskQualification.IsPending).ToList();
        }

        public async System.Threading.Tasks.Task<List<TaskQualification_Evaluator_Link>> GetTaskEvaluatorLinksByTQIdAsync(int taskQualificationId)
        {
            return (await FindAsync(x => x.TaskQualificationId == taskQualificationId)).ToList();
        }

        public async Task<List<TaskQualification_Evaluator_Link>> GetTaskQualificationsByEmployeeId(int empId)
        {
            var tq_evals = await FindAsync(x=>x.EvaluatorId == empId);
            return tq_evals.ToList();
        }

        public async Task<List<TaskQualification_Evaluator_Link>> GetTaskQualificationsEvalLinksByEmployeeId(List<int> empIds)
        {
            List<Expression<Func<TaskQualification_Evaluator_Link, bool>>> predicates = new List<Expression<Func<TaskQualification_Evaluator_Link, bool>>>();
            predicates.Add(x => empIds.Contains(x.EvaluatorId));
            var tqevals = (await FindWithIncludeAsync(predicates, new string[] { "TaskQualification.Employee.Person", "TaskQualification.TQEmpSetting" }, true)).ToList();
            return tqevals;
        }
    }
}
