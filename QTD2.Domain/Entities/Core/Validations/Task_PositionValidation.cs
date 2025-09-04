using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_PositionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_PositionValidation : Validation<Task_Position>, ITask_PositionValidation
    {
        public Task_PositionValidation(IStringLocalizerFactory factory)
            : base(factory)
        {
            AddRule(new ValidationRule<Task_Position>(new Task_Position_TaskIdRequiredSpec(), _validationStringLocalizer["Task_Position_TaskIdRequired"]));
            AddRule(new ValidationRule<Task_Position>(new Task_Position_PositionIdRequiredSpec(), _validationStringLocalizer["Task_Position_PositionIdRequired"]));
        }
    }
}
