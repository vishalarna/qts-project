using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_Task_Tool_LinkSpecs
{
    public class VTTL_VersionTaskIdRequiredSpec : ISpecification<Version_Task_Tool_Link>
    {
        public bool IsSatisfiedBy(Version_Task_Tool_Link entity, params object[] args)
        {
            return entity.Version_TaskId > 0;
        }
    }
}
