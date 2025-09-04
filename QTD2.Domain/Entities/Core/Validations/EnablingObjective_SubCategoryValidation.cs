using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_SubCategorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_SubCategoryValidation : Validation<EnablingObjective_SubCategory>, IEnablingObjective_SubCategoryValidation
    {
        public EnablingObjective_SubCategoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_SubCategory>(new EO_SubCategoryCategoryIdRequiredSpec(), _validationStringLocalizer["EO_SubCat_CategoryIdRequired"]));
            AddRule(new ValidationRule<EnablingObjective_SubCategory>(new EO_SubCategoryNumberRequiredSpec(), _validationStringLocalizer["EO_SubCat_NumberInvalid"]));
            AddRule(new ValidationRule<EnablingObjective_SubCategory>(new EO_SubCategoryTitleRequiredSpec(), _validationStringLocalizer["EO_SubCategoryTitleRequiredSpec"]));
        }
    }
}
