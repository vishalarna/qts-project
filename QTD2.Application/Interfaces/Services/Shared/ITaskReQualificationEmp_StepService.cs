using QTD2.Infrastructure.Model.TaskReQualificationEmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskReQualificationEmp_StepService
    {
        public Task<TaskReQualificationEmpStepVM> GetStepsData(int qualificationId, int taskId, int employeeId);

        public System.Threading.Tasks.Task CreateOrUpdateStepsAsync(TaskReQualificationEmpStepVM options);
    }
}
