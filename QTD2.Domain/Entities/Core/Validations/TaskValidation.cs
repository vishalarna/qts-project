using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TaskSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskValidation : Validation<Task>, ITaskValidation
    {
        public TaskValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task>(new TaskDescriptionRequiredSpec(), _validationStringLocalizer["TaskDescRequired"]));
            AddRule(new ValidationRule<Task>(new TaskNumberRequiredSpec(), _validationStringLocalizer["TaskNumRequired"]));
            AddRule(new ValidationRule<Task>(new TaskSubdutyAreaIdRequiredSpec(), _validationStringLocalizer["TaskSubdutyAreaIdRequired"]));
        }
    }
}
