using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TaskSpecs
{
    public class TaskDescriptionRequiredSpec : ISpecification<Task>
    {
        public bool IsSatisfiedBy(Task entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
