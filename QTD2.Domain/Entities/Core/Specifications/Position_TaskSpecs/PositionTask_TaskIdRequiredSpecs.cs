using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Position_TaskSpecs
{
    public class PositionTask_TaskIdRequiredSpecs : ISpecification<Position_Task>
    {
        public bool IsSatisfiedBy(Position_Task entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
