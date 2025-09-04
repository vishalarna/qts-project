namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazard_AbatementSpecs
{
    public class SaftyHazard_AbatementDescriptionRequiredSpec : Interfaces.Specification.ISpecification<SaftyHazard_Abatement>
    {
        public bool IsSatisfiedBy(SaftyHazard_Abatement entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
