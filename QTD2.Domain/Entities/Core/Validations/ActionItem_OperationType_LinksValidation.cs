using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_OperationType_LinksSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class ActionItem_OperationType_LinksValidation : Validation<ActionItem_OperationType_Links>, IActionItem_OperationType_LinksValidation
    {
        public ActionItem_OperationType_LinksValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_OperationType_Links>(new ActionItem_OperationType_Links_ActionItemOperationNameRequiredSpec(), _validationStringLocalizer["ActionItem_OperationType_Links_ActionItemOperationNameRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_OperationType_Links>(new ActionItem_OperationType_Links_OperationTypeIDRequiredSpec(), _validationStringLocalizer["ActionItem_OperationType_Links_OperationTypeIDRequiredSpec"]));

        }
    }
}