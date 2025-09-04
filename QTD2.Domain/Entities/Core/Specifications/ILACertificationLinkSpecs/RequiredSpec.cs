using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Authentication.Specifications.ILACertificationLinkSpecs
{
    public class RequiredSpec : ISpecification<Client>
    {
        public bool IsSatisfiedBy(Client entity, params object[] args)
        {
            return true;
        }
    }
}
