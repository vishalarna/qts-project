using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItemSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ActionItemValidation : Validation<ActionItem>, IActionItemValidation
    {
        public ActionItemValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem>(new ActionItem_TaskReviewIdRequiredSpec(), _validationStringLocalizer["ActionItem_TaskReviewIdRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem>(new ActionItem_PriorityIdRequiredSpec(), _validationStringLocalizer["ActionItem_PriorityIdRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem>(new ActionItem_AssignedDateRequiredSpec(), _validationStringLocalizer["ActionItem_AssignedDateRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem>(new ActionItem_DueDateRequiredSpec(), _validationStringLocalizer["ActionItem_DueDateRequiredSpec"]));
        }
    }
}