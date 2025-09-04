using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EmployeeOrganizationRepository : Common.Repository<EmployeeOrganization>, IEmployeeOrganizationRepository
    {
        public EmployeeOrganizationRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
