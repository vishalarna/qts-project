namespace QTD2.Domain.Entities.Core.Specifications.Procedure_EnablingObjective_LinkSpecs
{
    public class Proc_EO_LinkProcedureIdRequiredSpec : Interfaces.Specification.ISpecification<Procedure_EnablingObjective_Link>
    {
        public bool IsSatisfiedBy(Procedure_EnablingObjective_Link entity, params object[] args)
        {
            return entity.ProcedureId > 0;
        }
    }
}
