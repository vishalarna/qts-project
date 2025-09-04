using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_SaftyHazard_ControlSpecs
{
    public class VSHC_NumberRequiredSpec : ISpecification<Version_SaftyHazard_Control>
    {
        public bool IsSatisfiedBy(Version_SaftyHazard_Control entity, params object[] args)
        {
            return entity.Number > 0;
        }
    }
}
