using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SelfRegistrationOptionsSpecs
{
    public class SelfRegistrationOptionsILAIdRequiredSpec : ISpecification<ILA_SelfRegistrationOptions>
    {
        public bool IsSatisfiedBy(ILA_SelfRegistrationOptions entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
