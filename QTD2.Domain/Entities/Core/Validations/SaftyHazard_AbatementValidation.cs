using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SaftyHazard_AbatementSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SaftyHazard_AbatementValidation : Validation<SaftyHazard_Abatement>, ISaftyHazard_AbatementValidation
    {
        public SaftyHazard_AbatementValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SaftyHazard_Abatement>(new SaftyHazard_AbatementDescriptionRequiredSpec(), _validationStringLocalizer["SaftyHazard_AbatementDescriptionRequired"]));
            AddRule(new ValidationRule<SaftyHazard_Abatement>(new SaftyHazard_AbatementNumberRequiredSpec(), _validationStringLocalizer["SaftyHazard_AbatementNumberInvalid"]));
            AddRule(new ValidationRule<SaftyHazard_Abatement>(new SaftyHazard_AbatementSaftyHazardIdRequiredSpec(), _validationStringLocalizer["SaftyHazard_AbatementSaftyHazardIdRequired"]));
        }
    }
}
