using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_Notification_CustomSetting : Common.Entity
    {
        public int ClientSettingsNotificationId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public ClientSettings_Notification_CustomSetting() { }

        public ClientSettings_Notification_CustomSetting(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public void UpdateValue(string value, string modifiedBy)
        {
            Value = value;
            ModifiedBy = modifiedBy;
            ModifiedDate = DateTime.Now;
        }
    }
}
