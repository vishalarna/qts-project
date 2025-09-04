using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Employee_TaskService : Common.Service<Employee_Task>, IEmployee_TaskService
    {
        public Employee_TaskService(IEmployee_TaskRepository repository, IEmployee_TaskValidation validation)
            : base(repository, validation)
        {
        }
    }
}
