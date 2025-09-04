using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CertificationSpecs
{
    public class CertifyingBodyIdRequiredSpec : ISpecification<Certification>
    {
        public bool IsSatisfiedBy(Certification entity, params object[] args)
        {
            return entity.CertifyingBodyId > 0;
        }
    }
}
