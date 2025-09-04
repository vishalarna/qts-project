using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
namespace QTD2.Data.Repository.Core
{
    public class CertificationIssuingAuthorityRepository : Common.Repository<CertificationIssuingAuthority>, ICertificationIssuingAuthorityRepository
    {
        public CertificationIssuingAuthorityRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
