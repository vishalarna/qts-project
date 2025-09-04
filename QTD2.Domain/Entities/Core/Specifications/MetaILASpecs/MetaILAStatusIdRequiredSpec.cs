using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.MetaILASpecs
{
    public class MetaILAStatusIdRequiredSpec : ISpecification<MetaILA>
    {
        public bool IsSatisfiedBy(MetaILA entity, params object[] args)
        {
            return entity.MetaILAStatusId > 0;
        }
    }
}
