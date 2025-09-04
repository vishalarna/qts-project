using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_EnablingObjective_LinkSpecs;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_EnablingObjective_LinkValidation : Validation<Task_EnablingObjective_Link>, Interfaces.Validation.Core.ITask_EnablingObjective_LinkValidation
    {
        public Task_EnablingObjective_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_EnablingObjective_Link>(new Task_EO_LinkEnablingObjectiveIdRequiredSpec(), _validationStringLocalizer["Task_EO_LinkEnablingObjectiveIdRequired"]));
            AddRule(new ValidationRule<Task_EnablingObjective_Link>(new Task_EO_LinkTaskIdRequiredSpec(), _validationStringLocalizer["Task_EO_LinkTaskIdRequired"]));
        }
    }
}
