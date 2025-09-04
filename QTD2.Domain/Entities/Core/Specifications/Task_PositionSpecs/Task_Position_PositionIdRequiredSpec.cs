using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_PositionSpecs
{
    public class Task_Position_PositionIdRequiredSpec : ISpecification<Task_Position>
    {
        public bool IsSatisfiedBy(Task_Position entity, params object[] args)
        {
            return entity.PositionId > 0;
        }
    }
}
