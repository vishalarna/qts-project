using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjectiveSpecs
{
    public class EONumberRequiredSpec : ISpecification<EnablingObjective>
    {
        public bool IsSatisfiedBy(EnablingObjective entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Number);
        }
    }
}
