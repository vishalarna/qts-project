using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SaftyHazardSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SaftyHazardValidation : Validation<SaftyHazard>, ISaftyHazardValidation
    {
        public SaftyHazardValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SaftyHazard>(new SaftyHazardNumberRequiredSpec(), _validationStringLocalizer["SaftyHazardNumberInvalid"]));
            AddRule(new ValidationRule<SaftyHazard>(new SaftyHazardTitleRequiredSpec(), _validationStringLocalizer["SaftyHazardTitleRequired"]));
            AddRule(new ValidationRule<SaftyHazard>(new SH_SaftHazardCategoryIdRequiredSpec(), _validationStringLocalizer["SH_SaftHazardCategoryIdRequired"]));
        }
    }
}
