using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_CategorySpecs
{
    public class EO_CategoryNumberRequiredSpec : ISpecification<EnablingObjective_Category>
    {
        public bool IsSatisfiedBy(EnablingObjective_Category entity, params object[] args)
        {
            return entity.Number > 0;
        }
    }
}
