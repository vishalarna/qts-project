using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_PrioritySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ActionItem_PriorityValidation : Validation<ActionItem_Priority>, IActionItem_PriorityValidation
    {
        public ActionItem_PriorityValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_Priority>(new ActionItem_Priority_TypeRequiredSpec(), _validationStringLocalizer["ActionItem_Priority_TypeRequiredSpec"]));
        }
    }
}