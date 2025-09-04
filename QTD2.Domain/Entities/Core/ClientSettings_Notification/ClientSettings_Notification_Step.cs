using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_Notification_Step : Common.Entity
    {
        public ClientSettings_Notification_Step() { }

        public ClientSettings_Notification_Step(string template, List<ClientSettings_Notification_Step_CustomSetting> customStepSettings, List<ClientSettings_Notification_Step_AvailableCustomSetting> availableCustomSettings)
        {
            Template = template;
            CustomSettings = customStepSettings;
            AvailableCustomSettings = availableCustomSettings;
        }

        public int ClientSettingsNotificationId { get; set; }
        public string Template { get; set; }
        public int Order { get; set; }

        public virtual List<ClientSettings_Notification_Step_CustomSetting> CustomSettings { get; set; } = new List<ClientSettings_Notification_Step_CustomSetting>();
        public virtual List<ClientSettings_Notification_Step_Recipient> Recipients { get; set; } = new List<ClientSettings_Notification_Step_Recipient>();
        public virtual List<ClientSettings_Notification_Step_AvailableCustomSetting> AvailableCustomSettings { get; set; } = new List<ClientSettings_Notification_Step_AvailableCustomSetting>();

        public virtual ClientSettings_Notification ClientSettings_Notification { get; set; }

        public void UpdateTemplate(string template, string modifiedBy)
        {
            Template = template;
            ModifiedBy = modifiedBy;
            ModifiedDate = DateTime.Now;
        }

        public void AddRecipients(List<int> addRecipients, string modifiedBy)
        {
            if (Recipients == null) Recipients = new List<ClientSettings_Notification_Step_Recipient>();

            foreach (var employeeId in addRecipients)
            {
                Recipients.Add(new ClientSettings_Notification_Step_Recipient(employeeId));
            }

            ModifiedBy = modifiedBy;
            ModifiedDate = DateTime.Now;
        }

        public void RemoveRecipients(List<int> removeRecipients, string modifiedBy)
        {
            if (Recipients == null) Recipients = new List<ClientSettings_Notification_Step_Recipient>();

            foreach (var employeeId in removeRecipients)
            {
                var rec = Recipients.Where(r => r.EmployeeId == employeeId).FirstOrDefault();

                if (rec == null) continue;

                Recipients.Remove(rec);
            }

            ModifiedBy = modifiedBy;
            ModifiedDate = DateTime.Now;
        }

        public void UpdateCustomSetting(string key, string value, string modifiedBy)
        {
            if (AvailableCustomSettings == null) return;

            if (AvailableCustomSettings.Select(r => r.Setting.ToUpper() == key.ToUpper()).Count() == 0) return;

            if (!isValidCustomSettingValue(key, value)) return;

            if (CustomSettings == null)
                CustomSettings = new List<ClientSettings_Notification_Step_CustomSetting>();

            var customSetting = CustomSettings.Where(r => r.Key == key).FirstOrDefault();

            if (customSetting == null)
                CustomSettings.Add(new ClientSettings_Notification_Step_CustomSetting(key, value));

            else
                customSetting.UpdateValue(value, modifiedBy);

            AddDomainEvent(new Events.Core.OnClientSetting_Notification_Step_Changed(this));
        }

        private bool isValidCustomSettingValue(string key, string value)
        {
            //to do
            return true;
        }
    }
}
