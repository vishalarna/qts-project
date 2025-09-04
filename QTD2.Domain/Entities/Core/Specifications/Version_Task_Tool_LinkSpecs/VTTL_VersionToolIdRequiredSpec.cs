using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_Task_Tool_LinkSpecs
{
    public class VTTL_VersionToolIdRequiredSpec : ISpecification<Version_Task_Tool_Link>
    {
        public bool IsSatisfiedBy(Version_Task_Tool_Link entity, params object[] args)
        {
            return entity.Version_ToolId > 0;
        }
    }
}
