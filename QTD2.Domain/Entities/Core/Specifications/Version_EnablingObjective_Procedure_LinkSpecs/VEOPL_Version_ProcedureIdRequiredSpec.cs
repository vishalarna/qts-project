using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_Procedure_LinkSpecs
{
    public class VEOPL_Version_ProcedureIdRequiredSpec : ISpecification<Version_EnablingObjective_Procedure_Link>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective_Procedure_Link entity, params object[] args)
        {
            return entity.Version_ProcedureId > 0;
        }
    }
}
