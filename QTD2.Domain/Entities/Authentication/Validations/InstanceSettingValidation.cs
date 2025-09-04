using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication.Specifications.InstanceSettingSpecs;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Authentication.Validations
{
    public class InstanceSettingValidation : Validation<InstanceSetting>, Interfaces.Validation.Authentication.IInstanceSettingValidation
    {
        public InstanceSettingValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstanceSetting>(new DatabaseNameRequiredSpec(), _validationStringLocalizer["DBNameRequried"]));
            AddRule(new ValidationRule<InstanceSetting>(new DatabaseNameDifferSpec(), _validationStringLocalizer["DBNameMatchesClient"]));
            AddRule(new ValidationRule<InstanceSetting>(new InstanceIdRequiredSpec(), _validationStringLocalizer["ClientIdRequried"]));
        }
    }
}
