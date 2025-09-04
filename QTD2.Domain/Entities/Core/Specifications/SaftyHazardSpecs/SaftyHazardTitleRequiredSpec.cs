namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazardSpecs
{
    public class SaftyHazardTitleRequiredSpec : Interfaces.Specification.ISpecification<SaftyHazard>
    {
        public bool IsSatisfiedBy(SaftyHazard entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
