using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EmployeeOrganizationSpecs
{
    public class EmpOrgOrganizationIdRequiredSpec : ISpecification<EmployeeOrganization>
    {
        public bool IsSatisfiedBy(EmployeeOrganization entity, params object[] args)
        {
            return entity.OrganizationId > 0;
        }
    }
}
