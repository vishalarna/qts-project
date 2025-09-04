using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ProviderLevelSpecs
{
    public class ProviderLevelNameRequiredSpec : ISpecification<ProviderLevel>
    {
        public bool IsSatisfiedBy(ProviderLevel entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
