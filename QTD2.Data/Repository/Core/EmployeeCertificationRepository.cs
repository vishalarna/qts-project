using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EmployeeCertificationRepository : Common.Repository<EmployeeCertification>, IEmployeeCertificationRepository
    {
        public EmployeeCertificationRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
