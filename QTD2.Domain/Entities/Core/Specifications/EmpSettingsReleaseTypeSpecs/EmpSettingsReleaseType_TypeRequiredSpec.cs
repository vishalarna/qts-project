using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EmpSettingsReleaseTypeSpecs
{
    public class EmpSettingsReleaseType_TypeRequiredSpec : ISpecification<EmpSettingsReleaseType>
    {
        public bool IsSatisfiedBy(EmpSettingsReleaseType entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Type);
        }
    }
}
