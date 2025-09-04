using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SaftyHazard_CategorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SaftyHazard_CategoryValidation : Validation<SaftyHazard_Category>, ISaftyHazard_CategoryValidation
    {
        public SaftyHazard_CategoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SaftyHazard_Category>(new SaftyHazard_CategoryTitleRequiredSpec(), _validationStringLocalizer["SaftyHazard_CategoryTitleRequiredSpec"]));
        }
    }
}
