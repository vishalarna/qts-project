using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class CertificationRepository : Common.Repository<Certification>, ICertificationRepository
    {
        public CertificationRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
