using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClientUserSettings_DashboardSettingSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClientUserSettings_Dashboard_SettingValidation : Validation<ClientUserSettings_DashboardSetting>, IClientUserSettings_Dashboard_SettingValidation
    {
        public ClientUserSettings_Dashboard_SettingValidation(IStringLocalizerFactory stringLocalizerFactory)
          : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClientUserSettings_DashboardSetting>(new ClientUserSettings_DashboardSettingSpecs_ClientUserIdRequiredSpec(), _validationStringLocalizer["ClientUserIdRequired"]));
            AddRule(new ValidationRule<ClientUserSettings_DashboardSetting>(new ClientUserSettings_DashboardSettingSpecs_SettingIdRequiredSpec(), _validationStringLocalizer["SettingIdRequired"]));
        }
    }
}
