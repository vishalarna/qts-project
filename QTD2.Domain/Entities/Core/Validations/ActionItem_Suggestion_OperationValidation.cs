using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_Suggestion_OperationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class ActionItem_Suggestion_OperationValidation : Validation<ActionItem_Suggestion_Operation>, IActionItem_Suggestion_Operationvalidation
    {
        public ActionItem_Suggestion_OperationValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_Suggestion_Operation>(new ActionItem_Suggestion_Operation_ActionItemIDRequiredSpec(), _validationStringLocalizer["ActionItem_Suggestion_Operation_ActionItemIDRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_Suggestion_Operation>(new ActionItem_Suggestion_Operation_OperationTypeIDRequiredSpec(), _validationStringLocalizer["ActionItem_Suggestion_Operation_OperationTypeIDRequiredSpec"]));
            
        }
    }
}