using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TaskSpecs
{
    public class TaskSubdutyAreaIdRequiredSpec : ISpecification<Task>
    {
        public bool IsSatisfiedBy(Task entity, params object[] args)
        {
            return entity.SubdutyAreaId > 0;
        }
    }
}
