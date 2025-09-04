namespace QTD2.Domain.Entities.Core.Specifications.Procedure_IssuingAuthoritySpecs
{
    public class Procedure_IssuingAuthorityDescriptionRequiredSpec : Interfaces.Specification.ISpecification<Procedure_IssuingAuthority>
    {
        public bool IsSatisfiedBy(Procedure_IssuingAuthority entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
