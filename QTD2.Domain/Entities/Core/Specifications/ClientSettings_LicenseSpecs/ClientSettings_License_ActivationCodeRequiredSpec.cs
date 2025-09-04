using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSettings_LicenseSpecs
{
    public class ClientSettings_License_ActivationCodeRequiredSpec : ISpecification<ClientSettings_License>
    {
        public bool IsSatisfiedBy(ClientSettings_License entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.ActivationCode);
        }
    }
}

