using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications
{
    public class MetaILA_SummaryTestTestTypeIdRequiredSpec : ISpecification<MetaILA_SummaryTest>
    {
        public bool IsSatisfiedBy(MetaILA_SummaryTest entity, params object[] args)
        {
            return entity.TestTypeId > 0;
        }
    }
}
