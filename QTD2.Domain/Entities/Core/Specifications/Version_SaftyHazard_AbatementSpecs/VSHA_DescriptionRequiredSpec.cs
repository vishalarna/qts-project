using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_SaftyHazard_AbatementSpecs
{
    public class VSHA_DescriptionRequiredSpec : ISpecification<Version_SaftyHazard_Abatement>
    {
        public bool IsSatisfiedBy(Version_SaftyHazard_Abatement entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
