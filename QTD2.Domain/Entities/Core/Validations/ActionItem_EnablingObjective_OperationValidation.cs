using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_EnablingObjective_OperationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ActionItem_EnablingObjective_OperationValidation : Validation<ActionItem_EnablingObjective_Operation>, IActionItem_EnablingObjective_OperationValidation
    {
        public ActionItem_EnablingObjective_OperationValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_EnablingObjective_Operation>(new ActionItem_EnablingObjective_OperationActionItemIDRequiredSpec(), _validationStringLocalizer["ActionItem_EnablingObjective_OperationActionItemIDRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_EnablingObjective_Operation>(new ActionItem_EnablingObjective_Operation_OperationTypeIDRequiredSpec(), _validationStringLocalizer["ActionItem_EnablingObjective_Operation_OperationTypeIDRequiredSpec"]));

        }
    }
}