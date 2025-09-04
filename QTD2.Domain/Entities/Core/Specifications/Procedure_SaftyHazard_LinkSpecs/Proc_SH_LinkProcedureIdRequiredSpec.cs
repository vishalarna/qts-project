namespace QTD2.Domain.Entities.Core.Specifications.Procedure_SaftyHazard_LinkSpecs
{
    public class Proc_SH_LinkProcedureIdRequiredSpec : Interfaces.Specification.ISpecification<Procedure_SaftyHazard_Link>
    {
        public bool IsSatisfiedBy(Procedure_SaftyHazard_Link entity, params object[] args)
        {
            return entity.ProcedureId > 0;
        }
    }
}
