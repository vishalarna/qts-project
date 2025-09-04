namespace QTD2.Domain.Entities.Core.Specifications.Task_EnablingObjective_LinkSpecs
{
    public class Task_EO_LinkTaskIdRequiredSpec : Interfaces.Specification.ISpecification<Task_EnablingObjective_Link>
    {
        public bool IsSatisfiedBy(Task_EnablingObjective_Link entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
