using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_NercStandard_LinkSpecs
{
    public class ILA_NercStd_LinkILAIdRequiredSpec : ISpecification<ILA_NercStandard_Link>
    {
        public bool IsSatisfiedBy(ILA_NercStandard_Link entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
