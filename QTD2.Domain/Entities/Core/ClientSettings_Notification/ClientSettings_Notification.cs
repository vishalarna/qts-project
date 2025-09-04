using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_Notification : Common.Entity
    {
        public ClientSettings_Notification()
        {

        }

        public ClientSettings_Notification(string name, bool enabled, string timingText)
        {
            Name = name;
            Enabled = enabled;
            TimingText = timingText;
        }

        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string TimingText { get; set; }

        public virtual List<ClientSettings_Notification_Step> Steps { get; set; }
        public virtual List<ClientSettings_Notification_AvailableCustomSetting> AvailableCustomSettings { get; set; }
        public virtual List<ClientSettings_Notification_CustomSetting> CustomSettings { get; set; }
        public void AddStep(string template, List<ClientSettings_Notification_Step_CustomSetting> customStepSettings, List<ClientSettings_Notification_Step_AvailableCustomSetting> availableCustomSettings)
        {
            if (Steps == null) Steps = new List<ClientSettings_Notification_Step>();

            Steps.Add(new ClientSettings_Notification_Step(template, customStepSettings, availableCustomSettings));
        }

        public void Disable(string userId)
        {
            Enabled = false;
            ModifiedBy = userId;
            ModifiedDate = DateTime.Now;

            AddDomainEvent(new Events.Core.OnClientSetting_Notification_Disabled(this));
        }

        public void Enable(string userId)
        {
            Enabled = true;
            ModifiedBy = userId;
            ModifiedDate = DateTime.Now;

            AddDomainEvent(new Events.Core.OnClientSetting_Notification_Enabled(this));
        }
        public void AddAvailableCustomSetting(string setting)
        {
            if (AvailableCustomSettings == null)
                AvailableCustomSettings = new List<ClientSettings_Notification_AvailableCustomSetting>();

            AvailableCustomSettings.Add(new ClientSettings_Notification_AvailableCustomSetting(setting));
        }

        public void UpdateTemplate(int stepOrder, string template, string modifiedBy)
        {
            var step = Steps.Where(r => r.Order == stepOrder).FirstOrDefault();

            if (step == null) return;

            step.UpdateTemplate(template, modifiedBy);
        }

        public void AddRecipients(int stepOrder, List<int> addRecipients, string modifiedBy)
        {
            var step = Steps.Where(r => r.Order == stepOrder).FirstOrDefault();

            if (step == null) return;

            step.AddRecipients(addRecipients, modifiedBy);
        }

        public void RemoveRecipients(int stepOrder, List<int> removeRecipients, string modifiedBy)
        {
            var step = Steps.Where(r => r.Order == stepOrder).FirstOrDefault();

            if (step == null) return;

            step.RemoveRecipients(removeRecipients, modifiedBy);
        }

        public void AddCustomSetting(string key, string value)
        {
            if (CustomSettings == null) CustomSettings = new List<ClientSettings_Notification_CustomSetting>();

            //check available settings

            CustomSettings.Add(new ClientSettings_Notification_CustomSetting(key, value));
        }

        public void UpdateStepCustomSetting(int stepOrder, string key, string value, string modifiedBy)
        {
            var step = Steps.Where(r => r.Order == stepOrder).FirstOrDefault();

            if (step == null) return;

            step.UpdateCustomSetting(key, value, modifiedBy);
        }

        public void UpdateCustomSetting(string key, string value, string modifiedBy)
        {
            //todo change returns to exceptions for error handling up stream

            if(AvailableCustomSettings == null) return;

            if (AvailableCustomSettings.Select(r => r.Setting.ToUpper() == key.ToUpper()).Count() == 0) return;

            if (!isValidCustomSettingValue(key, value)) return;

            if (CustomSettings == null)
                CustomSettings = new List<ClientSettings_Notification_CustomSetting>();

            var customSetting = CustomSettings.Where(r => r.Key == key).FirstOrDefault();

            if (customSetting == null)
                CustomSettings.Add(new ClientSettings_Notification_CustomSetting(key, value));

            else
                customSetting.UpdateValue(value, modifiedBy);
        }

        private bool isValidCustomSettingValue(string key, string value)
        {
            //to do
            return true;
        }
    }
}
