using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_RegulatoryRequirement_OperationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class ActionItem_RegulatoryRequirement_OperationValidation : Validation<ActionItem_RegulatoryRequirement_Operation>, IActionItem_RegulatoryRequirement_OperationValidation
    {
        public ActionItem_RegulatoryRequirement_OperationValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_RegulatoryRequirement_Operation>(new ActionItem_RegulatoryRequirement_OperationActionItemIDRequiredSpec(), _validationStringLocalizer["ActionItem_RegulatoryRequirement_OperationActionItemIDRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_RegulatoryRequirement_Operation>(new ActionItem_RegulatoryRequirement_Operation_OperationTypeIDRequiredSpec(), _validationStringLocalizer["ActionItem_RegulatoryRequirement_Operation_OperationTypeIDRequiredSpec"]));

        }
    }
}