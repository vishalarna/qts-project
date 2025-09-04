using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_Tool_OperationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ActionItem_Tool_OperationValidation : Validation<ActionItem_Tool_Operation>, IActionItem_Tool_OperationValidation
    {
        public ActionItem_Tool_OperationValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_Tool_Operation>(new ActionItem_Tool_Operation_ActionItemIdRequiredSpec(), _validationStringLocalizer["ActionItem_Tool_Operation_ActionItemIdRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_Tool_Operation>(new ActionItem_Tool_Operation_OperationTypeIDRequiredSpec(), _validationStringLocalizer["ActionItem_Tool_Operation_OperationTypeIDRequiredSpec"]));

        }
    }
}