using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_SafetyHazard_LinkSpecs
{
    public class ILA_SH_LinkILAIdRequiredSpec : ISpecification<ILA_SafetyHazard_Link>
    {
        public bool IsSatisfiedBy(ILA_SafetyHazard_Link entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
