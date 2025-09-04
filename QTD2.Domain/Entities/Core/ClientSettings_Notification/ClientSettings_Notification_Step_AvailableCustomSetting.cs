using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_Notification_Step_AvailableCustomSetting : Common.Entity
    {
        public int ClientSettingsNotificationStepId { get; set; }
        public string Setting { get; set; }

        public ClientSettings_Notification_Step_AvailableCustomSetting() { }

        public ClientSettings_Notification_Step_AvailableCustomSetting(string setting)

        {
            Setting = setting;

        }
    }
}
