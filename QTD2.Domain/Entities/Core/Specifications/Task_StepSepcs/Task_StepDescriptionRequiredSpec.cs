namespace QTD2.Domain.Entities.Core.Specifications.Task_StepSepcs
{
    public class Task_StepDescriptionRequiredSpec : Interfaces.Specification.ISpecification<Task_Step>
    {
        public bool IsSatisfiedBy(Task_Step entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
