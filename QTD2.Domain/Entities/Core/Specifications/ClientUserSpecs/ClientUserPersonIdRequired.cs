using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientUserSpecs
{
    public class ClientUserPersonIdRequired : ISpecification<ClientUser>
    {
        public bool IsSatisfiedBy(ClientUser entity, params object[] args)
        {
            return entity.PersonId > 0;
        }
    }
}
