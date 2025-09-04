namespace QTD2.Domain.Entities.Core.Specifications.ProcedureSpecs
{
    public class ProcedureTitleRequiredSpec : Interfaces.Specification.ISpecification<Procedure>
    {
        public bool IsSatisfiedBy(Procedure entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
