using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications
{
    public class MetaILA_Employee_MetaILAIdRequiredSpec : ISpecification<MetaILA_Employee>
    {
        public bool IsSatisfiedBy(MetaILA_Employee entity, params object[] args)
        {
            return entity.MetaILAId > 0;
        }
    }
}
