namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazardSpecs
{
    public class SH_SaftHazardCategoryIdRequiredSpec : Interfaces.Specification.ISpecification<SaftyHazard>
    {
        public bool IsSatisfiedBy(SaftyHazard entity, params object[] args)
        {
            return entity.SaftyHazardCategoryId > 0;
        }
    }
}
