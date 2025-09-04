using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.RR_IssuingAuthoritySpecs
{
    public class RR_IATitleRequiredSpec : ISpecification<RR_IssuingAuthority>
    {
        public bool IsSatisfiedBy(RR_IssuingAuthority entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
