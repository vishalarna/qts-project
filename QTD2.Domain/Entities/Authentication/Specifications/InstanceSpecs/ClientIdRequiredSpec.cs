using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Authentication.Specifications.InstanceSpecs
{
    public class ClientIdRequiredSpec : ISpecification<Instance>
    {
        public bool IsSatisfiedBy(Instance entity, params object[] args)
        {
            return entity.ClientId > 0;
        }
    }
}
