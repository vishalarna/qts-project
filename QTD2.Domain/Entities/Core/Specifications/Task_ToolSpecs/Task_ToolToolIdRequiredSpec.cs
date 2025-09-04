namespace QTD2.Domain.Entities.Core.Specifications.Task_ToolSpecs
{
    public class Task_ToolToolIdRequiredSpec : Interfaces.Specification.ISpecification<Task_Tool>
    {
        public bool IsSatisfiedBy(Task_Tool entity, params object[] args)
        {
            return entity.ToolId > 0;
        }
    }
}
