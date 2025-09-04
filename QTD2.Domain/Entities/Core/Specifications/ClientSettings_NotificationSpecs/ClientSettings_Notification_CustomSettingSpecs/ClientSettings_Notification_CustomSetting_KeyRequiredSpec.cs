using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSettings_NotificationSpecs
{
    public class ClientSettings_Notification_CustomSetting_KeyRequiredSpec : ISpecification<ClientSettings_Notification>
    {
        public bool IsSatisfiedBy(ClientSettings_Notification entity, params object[] args)
        {
            foreach (var setting in entity.CustomSettings)
            {
                if (string.IsNullOrEmpty(setting.Key)) return false;
            }

            return true;
        }
    }
}
