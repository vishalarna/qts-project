namespace QTD2.Domain.Entities.Core.Specifications.ProcedureSpecs
{
    public class ProcedureIssuingAuthorityIdRequiredSpec : Interfaces.Specification.ISpecification<Procedure>
    {
        public bool IsSatisfiedBy(Procedure entity, params object[] args)
        {
            return entity.IssuingAuthorityId > 0;
        }
    }
}
