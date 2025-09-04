namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazard_ControlSpecs
{
    public class SaftyHazard_ControlSaftyHazardIdRequiredSpec : Interfaces.Specification.ISpecification<SaftyHazard_Control>
    {
        public bool IsSatisfiedBy(SaftyHazard_Control entity, params object[] args)
        {
            return entity.SaftyHazardId > 0;
        }
    }
}
