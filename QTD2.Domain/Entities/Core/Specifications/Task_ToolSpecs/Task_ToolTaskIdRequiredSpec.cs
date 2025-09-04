namespace QTD2.Domain.Entities.Core.Specifications.Task_ToolSpecs
{
    public class Task_ToolTaskIdRequiredSpec : Interfaces.Specification.ISpecification<Task_Tool>
    {
        public bool IsSatisfiedBy(Task_Tool entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
