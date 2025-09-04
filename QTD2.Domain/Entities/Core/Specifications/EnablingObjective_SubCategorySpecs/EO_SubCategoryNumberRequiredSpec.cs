using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_SubCategorySpecs
{
    public class EO_SubCategoryNumberRequiredSpec : ISpecification<EnablingObjective_SubCategory>
    {
        public bool IsSatisfiedBy(EnablingObjective_SubCategory entity, params object[] args)
        {
            return entity.Number > 0;
        }
    }
}
