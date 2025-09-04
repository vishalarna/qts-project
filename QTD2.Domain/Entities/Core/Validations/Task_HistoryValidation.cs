using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_HistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_HistoryValidation : Validation<Task_History>, ITask_HistoryValidation
    {
        public Task_HistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_History>(new Task_HistoryTaskIdRequiredSpec(), _validationStringLocalizer["Task_HistoryTaskIdRequiredSpec"]));
        }
    }
}
