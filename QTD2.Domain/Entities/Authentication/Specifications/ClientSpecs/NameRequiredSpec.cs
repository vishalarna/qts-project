using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Authentication.Specifications.ClientSpecs
{
    public class NameRequiredSpec : ISpecification<Client>
    {
        public bool IsSatisfiedBy(Client entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
