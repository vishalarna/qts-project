using QTD2.Domain.Interfaces.Specification;
namespace QTD2.Domain.Entities.Core.Specifications.Position_TaskSpecs
{
    public class R5ImpactedTaskPositionTaskIdRequiredSpecs : ISpecification<Position_Task>
    {
        public bool IsSatisfiedBy(Position_Task entity, params object[] args)
        {
            foreach (var r5ImpactedTask in entity.R5ImpactedTasks)
            {
                if (r5ImpactedTask.PositionTaskId <= 0) return false;
            }
            return true;
        }
    }
}