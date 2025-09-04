using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSetting_GeneralSetting
{
    public class ClientSetting_GeneralSetting_ClassStartEndTimeFormatSpec : ISpecification<ClientSettings_GeneralSettings>
    {
        public bool IsSatisfiedBy(ClientSettings_GeneralSettings entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.ClassStartEndTimeFormat);
        }
    }
}
