using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CertifyingBodySpecs
{
    public class CertifyingBodyNameRequiredSpec : ISpecification<CertifyingBody>
    {
        public bool IsSatisfiedBy(CertifyingBody entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
