using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_Step_OperationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class ActionItem_Step_OperationValidation : Validation<ActionItem_Step_Operation>, IActionItem_Step_OperationValidation
    {
        public ActionItem_Step_OperationValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_Step_Operation>(new ActionItem_Step_Operation_ActionItemIDRequiredSpec(), _validationStringLocalizer["ActionItem_Step_Operation_ActionItemIDRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_Step_Operation>(new ActionItem_Step_Operation_OperationTypeIDRequiredSpec(), _validationStringLocalizer["ActionItem_Step_Operation_OperationTypeIDRequiredSpec"]));
        }
    }
}