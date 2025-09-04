using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_PositionSpecs
{
    public class Task_Position_TaskIdRequiredSpec : ISpecification<Task_Position>
    {
        public bool IsSatisfiedBy(Task_Position entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
