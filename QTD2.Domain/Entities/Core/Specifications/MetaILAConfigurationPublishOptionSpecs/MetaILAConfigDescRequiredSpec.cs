using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.MetaILAConfigurationPublishOptionSpecs
{
    public class MetaILAConfigDescRequiredSpec : ISpecification<MetaILAConfigurationPublishOption>
    {
        public bool IsSatisfiedBy(MetaILAConfigurationPublishOption entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
