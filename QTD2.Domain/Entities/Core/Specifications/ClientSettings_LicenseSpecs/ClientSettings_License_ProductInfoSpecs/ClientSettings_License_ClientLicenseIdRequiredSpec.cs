using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSettings_LicenseSpecs
{
    public class ClientSettings_License_ClientLicenseIdRequiredSpec : ISpecification<ClientSettings_License>
    {
        public bool IsSatisfiedBy(ClientSettings_License entity, params object[] args)
        {
            bool valid = true;
            foreach (var product in entity.Products)
            {
                valid = valid && product.ClientSettings_LicenseId > 0;
            }

            return valid;

        }
    }
}
