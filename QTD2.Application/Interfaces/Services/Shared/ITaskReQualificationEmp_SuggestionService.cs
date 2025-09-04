using QTD2.Infrastructure.Model.TaskReQualificationEmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskReQualificationEmp_SuggestionService
    {
        public Task<TaskReQualificationEmpSuggestionVM> GetSuggestionData(int qualificationId, int taskId, int employeeId);

        public System.Threading.Tasks.Task CreateOrUpdateSuggestionsAsync(TaskReQualificationEmpSuggestionVM options);

        public Task<bool> GetShowSuggestionBit(int qualificationId);

        public Task<bool> GeQuestionAnswerBit(int qualificationId);

    }
}
