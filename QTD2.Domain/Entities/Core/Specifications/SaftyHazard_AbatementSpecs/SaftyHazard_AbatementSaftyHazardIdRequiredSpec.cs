namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazard_AbatementSpecs
{
    public class SaftyHazard_AbatementSaftyHazardIdRequiredSpec : Interfaces.Specification.ISpecification<SaftyHazard_Abatement>
    {
        public bool IsSatisfiedBy(SaftyHazard_Abatement entity, params object[] args)
        {
            return entity.SaftyHazardId > 0;
        }
    }
}
