namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazard_AbatementSpecs
{
    public class SaftyHazard_AbatementNumberRequiredSpec : Interfaces.Specification.ISpecification<SaftyHazard_Abatement>
    {
        public bool IsSatisfiedBy(SaftyHazard_Abatement entity, params object[] args)
        {
            if (entity.Deleted)
            {
                return entity.Number == null;
            }
            else
            {
                return entity.Number > 0;
            }
        }
    }
}
