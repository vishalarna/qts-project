using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.PositionSpecs
{
    public class PositionNameRequiredSpec : ISpecification<Position>
    {
        public bool IsSatisfiedBy(Position entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.PositionTitle);
        }
    }
}
