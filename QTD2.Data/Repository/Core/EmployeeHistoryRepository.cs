using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EmployeeHistoryRepository : Common.Repository<EmployeeHistory>, IEmployeeHistoryRepository
    {
        public EmployeeHistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}