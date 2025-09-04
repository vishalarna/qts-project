using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EmployeePositionRepository : Common.Repository<EmployeePosition>, IEmployeePositionRepository
    {
        public EmployeePositionRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
