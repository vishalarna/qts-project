using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.RegulatoryRequirementSpecs
{
    public class RRIssuingAuthorityIdRequiredSpec : ISpecification<RegulatoryRequirement>
    {
        public bool IsSatisfiedBy(RegulatoryRequirement entity, params object[] args)
        {
            return entity.IssuingAuthorityId > 0;
        }
    }
}
