namespace QTD2.Domain.Entities.Core.Specifications.Task_StepSepcs
{
    public class Task_StepNumberRequiredSpec : Interfaces.Specification.ISpecification<Task_Step>
    {
        public bool IsSatisfiedBy(Task_Step entity, params object[] args)
        {
            return entity.Number > 0;
        }
    }
}
