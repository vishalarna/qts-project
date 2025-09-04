using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_StepSepcs;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_StepValidation : Validation<Task_Step>, Interfaces.Validation.Core.ITask_StepValidation
    {
        public Task_StepValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_Step>(new Task_StepDescriptionRequiredSpec(), _validationStringLocalizer["Task_StepDescriptionRequired"]));
            AddRule(new ValidationRule<Task_Step>(new Task_StepNumberRequiredSpec(), _validationStringLocalizer["Task_StepNumberInvalid"]));
            AddRule(new ValidationRule<Task_Step>(new Task_StepTaskIdRequiredSpec(), _validationStringLocalizer["Task_StepTaskIdRequired"]));
        }
    }
}
