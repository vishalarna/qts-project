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
    public class TaskReQualificationEmp_SuggestionService : Common.Service<TaskReQualificationEmp_Suggestion>, ITaskReQualificationEmp_SuggestionService
    {
        public TaskReQualificationEmp_SuggestionService(ITaskReQualificationEmp_SuggestionRepository repository, ITaskReQualificationEmp_SuggestionValidation validation)
            : base(repository, validation)
        {
        }
    }
}
