using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ProviderSpecs
{
    public class ProviederNameRequiredSpec : ISpecification<Provider>
    {
        public bool IsSatisfiedBy(Provider entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
