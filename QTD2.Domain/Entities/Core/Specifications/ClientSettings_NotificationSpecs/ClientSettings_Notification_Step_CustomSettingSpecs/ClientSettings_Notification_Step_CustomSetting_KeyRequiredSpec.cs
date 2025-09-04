using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSettings_NotificationSpecs
{
    public class ClientSettings_Notification_Step_CustomSetting_KeyRequiredSpec : ISpecification<ClientSettings_Notification>
    {
        public bool IsSatisfiedBy(ClientSettings_Notification entity, params object[] args)
        {
           foreach(var step in entity.Steps)
            {
                foreach(var customSetting in step.CustomSettings)
                {
                    if (string.IsNullOrEmpty(customSetting.Key)) return false;
                }
            }

            return true;
        }
    }
}
