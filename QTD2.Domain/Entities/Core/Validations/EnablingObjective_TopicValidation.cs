using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_TopicSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_TopicValidation : Validation<EnablingObjective_Topic>, IEnablingObjective_TopicValidation
    {
        public EnablingObjective_TopicValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_Topic>(new EO_TopicTitleRequiredSpec(), _validationStringLocalizer["EO_TopicDescRequired"]));
            AddRule(new ValidationRule<EnablingObjective_Topic>(new EO_TopicNumberRequiredSpec(), _validationStringLocalizer["EO_TopicNumberInvalid"]));
            AddRule(new ValidationRule<EnablingObjective_Topic>(new EO_TopicSubCategoryIdRequiredSpec(), _validationStringLocalizer["EO_TopicSubCatIdRequired"]));
        }
    }
}
