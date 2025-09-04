using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Position_SQSpecs
{
    public class PositionSQEoIdRequiredSpecs : ISpecification<Positions_SQ>
    {
        public bool IsSatisfiedBy(Positions_SQ entity, params object[] args)
        {
            return entity.EOId > 0;
        }
    }
}
