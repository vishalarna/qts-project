using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClientSettings
{
    public class ClientSettings_NotificationUpdateOptions
    {
        public List<CustomSetting_Notification_TemplateEdit> UpdateTemplates { get; set; }
        public List<CustomSetting_Notification_Recipients> AddRecipients { get; set; }
        public List<CustomSetting_Notification_Recipients> RemoveRecipients { get; set; }
        public List<CustomSetting_Notification_Setting> NotificationCustomSettings { get; set; }
        public List<StepCustomSetting> NotificationStepCustomSettings { get; set; }
        public bool Disable { get; set; }
        public bool Enable { get; set; }
    }

    public class CustomSetting_Notification_Setting
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class StepCustomSetting
    {
        public int StepOrder { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class CustomSetting_Notification_TemplateEdit
    {
        public int Order { get; set; }
        public string Template { get; set; }
    }

    public class CustomSetting_Notification_Recipients
    {
        public int Order { get; set; }
        public int EmployeeId { get; set; }
    }
}
