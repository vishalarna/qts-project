using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_Task_QuestionSpecs
{
    public class VTQ_VersionTaskIdRequiredSpec : ISpecification<Version_Task_Question>
    {
        public bool IsSatisfiedBy(Version_Task_Question entity, params object[] args)
        {
            return entity.VersionTaskId > 0;
        }
    }
}
