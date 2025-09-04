using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.OrganizationSpecs
{
    public class OrganizatonNameRequiredSpec : ISpecification<Organization>
    {
        public bool IsSatisfiedBy(Organization entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
