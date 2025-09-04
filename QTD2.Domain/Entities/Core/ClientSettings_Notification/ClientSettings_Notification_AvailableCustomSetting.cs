using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_Notification_AvailableCustomSetting : Common.Entity
    {
        public ClientSettings_Notification_AvailableCustomSetting() { }
        public ClientSettings_Notification_AvailableCustomSetting(string setting)
        {
            Setting = setting;
        }

        public int ClientSettingsNotificationId { get; set; }
        public string Setting { get; set; }
    }
}
