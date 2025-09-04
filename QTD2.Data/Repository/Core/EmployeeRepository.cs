using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EmployeeRepository : Common.Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
