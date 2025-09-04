using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.PositionSpecs
{
    public class PositionHistoryPositionIdRequiredSpecs : ISpecification<Position_History>
    {
        public bool IsSatisfiedBy(Position_History entity, params object[] args)
        {
            return entity.PositionId > 0;
        }
    }
}
