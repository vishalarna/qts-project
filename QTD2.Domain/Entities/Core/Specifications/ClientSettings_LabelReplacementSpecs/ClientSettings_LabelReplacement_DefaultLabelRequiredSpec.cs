using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSettings_LabelReplacementSpecs
{
    public class ClientSettings_LabelReplacement_DefaultLabelRequiredSpec : ISpecification<ClientSettings_LabelReplacement>
    {
        public bool IsSatisfiedBy(ClientSettings_LabelReplacement entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.DefaultLabel);
        }
    }
}
