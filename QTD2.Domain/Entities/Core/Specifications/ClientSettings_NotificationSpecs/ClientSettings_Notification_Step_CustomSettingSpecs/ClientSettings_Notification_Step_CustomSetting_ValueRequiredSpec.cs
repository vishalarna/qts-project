using QTD2.Domain.Interfaces.Specification;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core.Specifications.ClientSettings_NotificationSpecs
{
    public class ClientSettings_Notification_Step_CustomSetting_ValueRequiredSpec : ISpecification<ClientSettings_Notification>
    {
        List<string> allowEmpties = new List<string>()
        {
            "SEND TO OTHERS"
        };

        public bool IsSatisfiedBy(ClientSettings_Notification entity, params object[] args)
        {
            foreach (var step in entity.Steps)
            {
                foreach (var customSetting in step.CustomSettings)
                {
                    if (!allowEmpties.Contains(customSetting.Key.ToUpper()))
                    {
                        if (string.IsNullOrEmpty(customSetting.Value)) return false;
                    }
                }
            }

            return true;
        }
    }
}
