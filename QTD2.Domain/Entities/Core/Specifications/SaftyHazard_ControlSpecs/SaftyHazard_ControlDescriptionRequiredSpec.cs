namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazard_ControlSpecs
{
    public class SaftyHazard_ControlDescriptionRequiredSpec : Interfaces.Specification.ISpecification<SaftyHazard_Control>
    {
        public bool IsSatisfiedBy(SaftyHazard_Control entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
