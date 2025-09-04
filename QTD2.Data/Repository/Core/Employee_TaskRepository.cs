using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Employee_TaskRepository : Common.Repository<Employee_Task>, IEmployee_TaskRepository
    {
        public Employee_TaskRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
