using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_ToolSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_ToolValidation : Validation<Task_Tool>, ITask_ToolValidation
    {
        public Task_ToolValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_Tool>(new Task_ToolTaskIdRequiredSpec(), _validationStringLocalizer["Task_ToolTaskIdRequired"]));
            AddRule(new ValidationRule<Task_Tool>(new Task_ToolToolIdRequiredSpec(), _validationStringLocalizer["Task_ToolToolIdRequired"]));
        }
    }
}
