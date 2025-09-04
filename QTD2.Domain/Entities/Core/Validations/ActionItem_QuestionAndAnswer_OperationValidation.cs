using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_QuestionAndAnswer_OperationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ActionItem_QuestionAndAnswer_OperationValidation : Validation<ActionItem_QuestionAndAnswer_Operation>, IActionItem_QuestionAndAnswer_OperationValidation
    {
        public ActionItem_QuestionAndAnswer_OperationValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ActionItem_QuestionAndAnswer_Operation>(new ActionItem_QuestionAndAnswer_Operation_ActionItemIDRequiredSpec(), _validationStringLocalizer["ActionItem_QuestionAndAnswer_Operation_ActionItemIDRequiredSpec"]));
            AddRule(new ValidationRule<ActionItem_QuestionAndAnswer_Operation>(new ActionItem_QuestionAndAnswer_Operation_OperationTypeIDRequiredSpec(), _validationStringLocalizer["ActionItem_QuestionAndAnswer_Operation_OperationTypeIDRequiredSpec"]));
        }
    }
}