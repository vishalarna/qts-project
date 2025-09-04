using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.RegulatoryRequirementSpecs
{
    public class RRDescriptionRequiredSpec : ISpecification<RegulatoryRequirement>
    {
        public bool IsSatisfiedBy(RegulatoryRequirement entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
