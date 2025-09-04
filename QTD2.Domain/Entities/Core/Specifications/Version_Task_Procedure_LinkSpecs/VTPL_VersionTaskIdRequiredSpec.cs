using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_Task_Procedure_LinkSpecs
{
    public class VTPL_VersionTaskIdRequiredSpec : ISpecification<Version_Task_Procedure_Link>
    {
        public bool IsSatisfiedBy(Version_Task_Procedure_Link entity, params object[] args)
        {
            return entity.Version_TaskId > 0;
        }
    }
}
