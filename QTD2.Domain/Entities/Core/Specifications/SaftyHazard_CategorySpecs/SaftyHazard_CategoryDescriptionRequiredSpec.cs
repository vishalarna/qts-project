namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazard_CategorySpecs
{
    public class SaftyHazard_CategoryDescriptionRequiredSpec : Interfaces.Specification.ISpecification<SaftyHazard_Category>
    {
        public bool IsSatisfiedBy(SaftyHazard_Category entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
