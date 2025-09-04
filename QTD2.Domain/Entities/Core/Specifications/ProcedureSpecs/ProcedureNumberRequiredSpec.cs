namespace QTD2.Domain.Entities.Core.Specifications.ProcedureSpecs
{
    public class ProcedureNumberRequiredSpec : Interfaces.Specification.ISpecification<Procedure>
    {
        public bool IsSatisfiedBy(Procedure entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Number);

        }
    }
}
