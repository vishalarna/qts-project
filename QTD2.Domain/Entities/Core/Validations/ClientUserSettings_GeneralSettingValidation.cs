using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClientSetting_GeneralSetting;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClientUserSettings_GeneralSettingValidation : Validation<ClientSettings_GeneralSettings>, IClientUserSettings_GeneralSettingValidation
    {
        public ClientUserSettings_GeneralSettingValidation(IStringLocalizerFactory stringLocalizerFactory)
         : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClientSettings_GeneralSettings>(new ClientSetting_GeneralSetting_CompanyNameSpec(), _validationStringLocalizer["CompanyNameRequired"]));

            AddRule(new ValidationRule<ClientSettings_GeneralSettings>(new ClientSetting_GeneralSetting_CompanyLogoSpec(), _validationStringLocalizer["CompanyLogoRequired"]));
            AddRule(new ValidationRule<ClientSettings_GeneralSettings>(new ClientSetting_GeneralSetting_ClassStartEndTimeFormatSpec(), _validationStringLocalizer["ClassStartEndTimeFormatRequired"]));

            AddRule(new ValidationRule<ClientSettings_GeneralSettings>(new ClientSetting_GeneralSetting_CompanySpecificCoursePassingScoreSpec(), _validationStringLocalizer["CompanySpecificCoursePassingScoreRequired"]));

            AddRule(new ValidationRule<ClientSettings_GeneralSettings>(new ClientSetting_GeneralSetting_DateFormatSpec(), _validationStringLocalizer["DateFormatRequired"]));
        }
    }
}
