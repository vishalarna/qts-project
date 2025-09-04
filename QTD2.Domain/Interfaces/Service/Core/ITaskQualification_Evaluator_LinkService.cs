using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITaskQualification_Evaluator_LinkService : Common.IService<TaskQualification_Evaluator_Link>
    {
        System.Threading.Tasks.Task<TaskQualification_Evaluator_Link> GetForNotificationAsync(int id);
        System.Threading.Tasks.Task<List<TaskQualification_Evaluator_Link>> GetTaskEvaluatorLinksByTQIdAsync(int taskQualificationId);
        System.Threading.Tasks.Task<List<TaskQualification_Evaluator_Link>> GetPendingTaskQualificationsByEvaluator(int evaluatorId);
        System.Threading.Tasks.Task<List<TaskQualification_Evaluator_Link>> GetTaskQualificationsByEmployeeId(int empId);
        System.Threading.Tasks.Task<List<TaskQualification_Evaluator_Link>> GetTaskQualificationsEvalLinksByEmployeeId(List<int> empIds);
    }
}
