using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_EnablingObjective_OperationSpecs;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_Procedure_OperationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ActionItem_Procedure_OperationValidation : Validation<ActionItem_Procedure_Operation>, IActionItem_Procedure_OperationValidation
    {
        public ActionItem_Procedure_OperationValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_Procedure_Operation>(new ActionItem_Procedure_Operation_ActionItemIDRequiredSpec(), _validationStringLocalizer["ActionItem_Procedure_Operation_ActionItemIDRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_Procedure_Operation>(new ActionItem_Procedure_Operation_OperationTypeIDRequiredSpec(), _validationStringLocalizer["ActionItem_Procedure_Operation_OperationTypeIDRequiredSpec"]));

        }
    }
}