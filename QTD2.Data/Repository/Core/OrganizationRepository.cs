using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class OrganizationRepository : Common.Repository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
