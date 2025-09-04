using QTD2.Infrastructure.Model.TaskReQualificationEmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskReQualificationEmp_QuestionAnswerService
    {
        public Task<TaskReQualificationEmpQuestionVM> GetQuestionsData(int qualificationId, int taskId, int employeeId);

        public System.Threading.Tasks.Task CreateOrUpdateQuestionsDataAsync(TaskReQualificationEmpQuestionVM options);
     
    }
}
