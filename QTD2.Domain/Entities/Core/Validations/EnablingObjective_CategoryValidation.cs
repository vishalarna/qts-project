using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_CategorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_CategoryValidation : Validation<EnablingObjective_Category>, IEnablingObjective_CategoryValidation
    {
        public EnablingObjective_CategoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_Category>(new EO_CategoryNumberRequiredSpec(), _validationStringLocalizer["EO_Cat_NumberRequired"]));
            AddRule(new ValidationRule<EnablingObjective_Category>(new EO_CategoryTitleRequiredSpec(), _validationStringLocalizer["EO_Cat_TitleRequired"]));
        }
    }
}
