using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_Task_StepSpecs
{
    public class VTS_TaskStepIdRequiredSpec : ISpecification<Version_Task_Step>
    {
        public bool IsSatisfiedBy(Version_Task_Step entity, params object[] args)
        {
            return entity.TaskStepId > 0;
        }
    }
}
