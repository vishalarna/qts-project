using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications
{
    public class MetaILA_SummaryTestTestIdRequiredSpec : ISpecification<MetaILA_SummaryTest>
    {
        public bool IsSatisfiedBy(MetaILA_SummaryTest entity, params object[] args)
        {
            return entity.TestId > 0;
        }
    }
}
