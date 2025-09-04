using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CertificationSpecs
{
    public class CertificationNameRequiredSpec : ISpecification<Certification>
    {
        public bool IsSatisfiedBy(Certification entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
