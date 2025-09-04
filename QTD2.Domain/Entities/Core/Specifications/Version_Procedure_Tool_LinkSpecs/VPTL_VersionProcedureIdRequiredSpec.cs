using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_Procedure_Tool_LinkSpecs
{
    public class VPTL_VersionProcedureIdRequiredSpec : ISpecification<Version_Procedure_Tool_Link>
    {
        public bool IsSatisfiedBy(Version_Procedure_Tool_Link entity, params object[] args)
        {
            return entity.Version_ProcedureId > 0;
        }
    }
}
