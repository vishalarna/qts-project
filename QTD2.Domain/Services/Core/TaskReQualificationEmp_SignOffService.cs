using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class TaskReQualificationEmp_SignOffService : Common.Service<TaskReQualificationEmp_SignOff>, ITaskReQualificationEmp_SignOffService
    {
        public TaskReQualificationEmp_SignOffService(ITaskReQualificationEmp_SignOffRepository repository, ITaskReQualificationEmp_SignOffValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<TaskReQualificationEmp_SignOff>> GetTaskReQualificationEmp_SignOffByEmployeeId(int employeeId)
        {
            var taskReQualification = await FindWithIncludeAsync(x => x.EvaluatorId == employeeId && x.IsCompleted == true, new string[] { "TaskQualification.Task", "TaskQualification.TQEmpSetting" });
            return taskReQualification.ToList();
        }

        public async Task<List<TaskReQualificationEmp_SignOff>> GetTaskReQualificationEmp_SignOffByTQId(int taskQualificationId)
        {
            var taskQualificationEmp = (await FindWithIncludeAsync(x => x.TaskQualificationId == taskQualificationId && x.IsCompleted == true, new string[] { "Evaluator.Person" })).ToList();
            return taskQualificationEmp;
        }
        public async Task<List<TaskReQualificationEmp_SignOff>> GetTaskReQualificationsEmp_SignOffByTQId(int taskQualificationId,int evaluatorId)
        {
            return (await FindAsync(x => x.TaskQualificationId == taskQualificationId && x.EvaluatorId == evaluatorId)).ToList();
        }

        public async Task<List<TaskReQualificationEmp_SignOff>> GetTaskReQualificationEmp_SignOffListByTQIds(List<int> tqIds)
        {
            return (await FindAsync(x => tqIds.Contains(x.TaskQualificationId) && x.IsCompleted == true)).ToList();
        }
    }
}
