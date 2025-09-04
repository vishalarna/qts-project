using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EmployeeOrganizationSpecs
{
    public class EmpOrgEmployeeIdRequiredSpec : ISpecification<EmployeeOrganization>
    {
        public bool IsSatisfiedBy(EmployeeOrganization entity, params object[] args)
        {
            return entity.EmployeeId > 0;
        }
    }
}
