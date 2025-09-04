namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazard_ControlSpecs
{
    public class SaftyHazard_ControlNumberRequiredSpec : Interfaces.Specification.ISpecification<SaftyHazard_Control>
    {
        public bool IsSatisfiedBy(SaftyHazard_Control entity, params object[] args)
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
