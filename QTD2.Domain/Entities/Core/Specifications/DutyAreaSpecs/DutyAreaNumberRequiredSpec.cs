using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.DutyAreaSpecs
{
    public class DutyAreaNumberRequiredSpec : ISpecification<DutyArea>
    {
        public bool IsSatisfiedBy(DutyArea entity, params object[] args)
        {
            return entity.Number > 0;
        }
    }
}
