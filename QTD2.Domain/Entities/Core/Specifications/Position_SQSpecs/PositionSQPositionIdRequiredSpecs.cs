using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Position_SQSpecs
{
    public class PositionSQPositionIdRequiredSpecs : ISpecification<Positions_SQ>
    {
        public bool IsSatisfiedBy(Positions_SQ entity, params object[] args)
        {
            return entity.PositionId > 0;
        }
    }
}
