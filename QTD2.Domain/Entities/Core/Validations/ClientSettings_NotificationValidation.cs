using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClientSettings_NotificationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClientSettings_NotificationValidation : Validation<ClientSettings_Notification>, IClientSettings_NotificationValidation
    {
        public ClientSettings_NotificationValidation(IStringLocalizerFactory stringLocalizerFactory)
          : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClientSettings_Notification>(new ClientSettings_Notification_NameRequiredSpec(), _validationStringLocalizer["NameRequired"]));

            AddRule(new ValidationRule<ClientSettings_Notification>(new ClientSettings_Notification_Step_OrderRequiredSpec(), _validationStringLocalizer["OrderRequired"]));
            AddRule(new ValidationRule<ClientSettings_Notification>(new ClientSettings_Notification_Step_TemplateRequiredSpec(), _validationStringLocalizer["TemplateRequired"]));

            AddRule(new ValidationRule<ClientSettings_Notification>(new ClientSettings_Notification_Step_Recipient_EmployeeIdRequiredSpec(), _validationStringLocalizer["EmployeeRequired"]));

            AddRule(new ValidationRule<ClientSettings_Notification>(new ClientSettings_Notification_Step_CustomSetting_KeyRequiredSpec(), _validationStringLocalizer["KeyRequired"]));
            AddRule(new ValidationRule<ClientSettings_Notification>(new ClientSettings_Notification_Step_CustomSetting_ValueRequiredSpec(), _validationStringLocalizer["ValueRequired"]));

            AddRule(new ValidationRule<ClientSettings_Notification>(new ClientSettings_Notification_Step_AvailableCustomSetting_SettingRequiredSpec(), _validationStringLocalizer["SettingRequired"]));

            AddRule(new ValidationRule<ClientSettings_Notification>(new ClientSettings_Notification_CustomSetting_KeyRequiredSpec(), _validationStringLocalizer["KeyRequired"]));
            AddRule(new ValidationRule<ClientSettings_Notification>(new ClientSettings_Notification_CustomSetting_ValueRequiredSpec(), _validationStringLocalizer["ValueRequired"]));

            AddRule(new ValidationRule<ClientSettings_Notification>(new ClientSettings_Notification_AvailableCustomSetting_SettingRequiredSpec(), _validationStringLocalizer["SettingRequired"]));
        }
    }
}
