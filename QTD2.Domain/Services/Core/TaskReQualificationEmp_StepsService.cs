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
    public class TaskReQualificationEmp_StepsService : Common.Service<TaskReQualificationEmp_Steps>, ITaskReQualificationEmp_StepsService
    {
        public TaskReQualificationEmp_StepsService(ITaskReQualificationEmp_StepsRepository repository, ITaskReQualificationEmp_StepsValidation validation)
            : base(repository, validation)
        {
        }
    }
}
