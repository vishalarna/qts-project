using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_SafetyHazard_OperationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ActionItem_SafetyHazard_OperationValidation : Validation<ActionItem_SafetyHazard_Operation>, IActionItem_SafetyHazard_OperationValidation
    {
        public ActionItem_SafetyHazard_OperationValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_SafetyHazard_Operation>(new ActionItem_SafetyHazard_Operation_ActionItemIDRequiredSpec(), _validationStringLocalizer["ActionItem_SafetyHazard_Operation_ActionItemIDRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_SafetyHazard_Operation>(new ActionItem_SafetyHazard_Operation_OperationTypeIDRequiredSpec(), _validationStringLocalizer["ActionItem_SafetyHazard_Operation_OperationTypeIDRequiredSpec"]));

        }
    }
}