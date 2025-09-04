using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SaftyHazard_ControlSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SaftyHazard_ControlValidation : Validation<SaftyHazard_Control>, ISaftyHazard_ControlValidation
    {
        public SaftyHazard_ControlValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SaftyHazard_Control>(new SaftyHazard_ControlDescriptionRequiredSpec(), _validationStringLocalizer["SaftyHazard_ControlDescriptionRequired"]));
            AddRule(new ValidationRule<SaftyHazard_Control>(new SaftyHazard_ControlNumberRequiredSpec(), _validationStringLocalizer["SaftyHazard_ControlNumberInvalid"]));
            AddRule(new ValidationRule<SaftyHazard_Control>(new SaftyHazard_ControlSaftyHazardIdRequiredSpec(), _validationStringLocalizer["SaftyHazard_ControlSaftyHazardIdRequired"]));
        }
    }
}
