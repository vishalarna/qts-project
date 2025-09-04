using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;


namespace QTD2.Data.Repository.Core
{
    public class CertificationSubRequirementRepository : Common.Repository<CertificationSubRequirement>, ICertificationSubRequirementRepository
    {
        public CertificationSubRequirementRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}