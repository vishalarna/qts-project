using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_SubDuty_OperationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class ActionItem_SubDuty_OperationValidation : Validation<ActionItem_SubDuty_Operation>, IActionItem_SubDuty_OperationValidation
    {
        public ActionItem_SubDuty_OperationValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_SubDuty_Operation>(new ActionItem_SubDuty_Operation_ActionItemIDRequiredSpec(), _validationStringLocalizer["ActionItem_SubDuty_Operation_ActionItemIDRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_SubDuty_Operation>(new ActionItem_SubDuty_Operation_OperationTypeIDRequiredSpec(), _validationStringLocalizer["ActionItem_SubDuty_Operation_OperationTypeIDRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_SubDuty_Operation>(new ActionItem_SubDuty_Operation_SubDutyAreaIDRequiredSpec(), _validationStringLocalizer["ActionItem_SubDuty_Operation_SubDutyAreaIDRequiredSpec"]));
        }
    }
}