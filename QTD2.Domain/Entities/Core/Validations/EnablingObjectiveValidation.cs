using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjectiveSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjectiveValidation : Validation<EnablingObjective>, IEnablingObjectiveValidation
    {
        public EnablingObjectiveValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective>(new EONumberRequiredSpec(), _validationStringLocalizer["EONumInvalid"]));
            AddRule(new ValidationRule<EnablingObjective>(new EOCategoryIdRequiredSpec(), _validationStringLocalizer["EOCategoryIdRequiredSpec"]));
            AddRule(new ValidationRule<EnablingObjective>(new EOSubCategoryIdRequiredSpec(), _validationStringLocalizer["EOSubCategoryIdRequiredSpec"]));
            AddRule(new ValidationRule<EnablingObjective>(new EODescriptionRequiredSpec(), _validationStringLocalizer["EOStatementRequiredSpec"]));
        }
    }
}
