using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClientSettings_LicenseSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;


namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClientUserSettings_LicenseValidation : Validation<ClientSettings_License>, IClientUserSettings_LicenseValidation
    {
        public ClientUserSettings_LicenseValidation(IStringLocalizerFactory stringLocalizerFactory)
        : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClientSettings_License>(new ClientSettings_License_ActivationCodeRequiredSpec(), _validationStringLocalizer["ActivationCodeRequired"]));
            AddRule(new ValidationRule<ClientSettings_License>(new ClientSettings_License_ActivationCodeFormatSpec(), _validationStringLocalizer["ActivationCodeFormat"]));
            AddRule(new ValidationRule<ClientSettings_License>(new ClientSettings_License_ClientIdRequiredSpec(), _validationStringLocalizer["ClientIdRequired"]));
        }
    }
}
