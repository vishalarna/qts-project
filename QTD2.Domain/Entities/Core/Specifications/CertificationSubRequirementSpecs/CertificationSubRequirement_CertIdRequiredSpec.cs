using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CertificationSubRequirementSpecs
{
    public class CertificationSubRequirement_CertIdRequiredSpec : ISpecification<CertificationSubRequirement>
    {
        public bool IsSatisfiedBy(CertificationSubRequirement entity, params object[] args)
        {
            return entity.CertificationId > 0;
        }
    }
}