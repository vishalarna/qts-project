using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSettings_NotificationSpecs
{
    public class ClientSettings_Notification_Step_AvailableCustomSetting_SettingRequiredSpec : ISpecification<ClientSettings_Notification>
    {
        public bool IsSatisfiedBy(ClientSettings_Notification entity, params object[] args)
        {
            foreach(var step in entity.Steps)
            {
                foreach(var setting in step.AvailableCustomSettings)
                {
                    if (string.IsNullOrEmpty(setting.Setting)) return false;
                }
            }

            return true;
        }
    }
}
