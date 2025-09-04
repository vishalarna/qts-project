using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_SuggestionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_SuggestionValidation : Validation<Task_Suggestion>, ITask_SuggestionValidation
    {
        public Task_SuggestionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_Suggestion>(new Task_SuggestionTaskIdRequiredSpec(), _validationStringLocalizer["Task_SuggestionTaskIdRequiredSpec"]));
            AddRule(new ValidationRule<Task_Suggestion>(new Task_SuggestionTaskSuggestionTypeIdRequiredSpec(), _validationStringLocalizer["Task_SuggestionTaskSuggestionTypeIdRequiredSpec"]));
        }
    }
}
