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
    public class TaskReQualificationEmp_QuestionAnswerService : Common.Service<TaskReQualificationEmp_QuestionAnswer>, ITaskReQualificationEmp_QuestionAnswerService
    {
        public TaskReQualificationEmp_QuestionAnswerService(ITaskReQualificationEmp_QuestionAnswerRepository repository, ITaskReQualificationEmp_QuestionAnswerValidation validation)
            : base(repository, validation)
        {
        }
    }
}
