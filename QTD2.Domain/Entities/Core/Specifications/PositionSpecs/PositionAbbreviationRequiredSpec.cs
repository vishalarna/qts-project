using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.PositionSpecs
{
    public class PositionAbbreviationRequiredSpec : ISpecification<Position>
    {
        public bool IsSatisfiedBy(Position entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.PositionAbbreviation);
        }
    }
}
