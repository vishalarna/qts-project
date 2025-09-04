using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSetting_GeneralSetting
{
    public class ClientSetting_GeneralSetting_CompanyLogoSpec : ISpecification<ClientSettings_GeneralSettings>
    {
        public bool IsSatisfiedBy(ClientSettings_GeneralSettings entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.CompanyLogo);
        }
    }
}
