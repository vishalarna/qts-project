using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_Reference_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_Reference_LinkValidation : Validation<Task_Reference_Link>, ITask_Reference_LinkValidation
    {
        public Task_Reference_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_Reference_Link>(new Task_Reference_LinkTaskRefIdRequiredSpec(), _validationStringLocalizer["Task_Reference_LinkTaskRefIdRequiredSpec"]));
            AddRule(new ValidationRule<Task_Reference_Link>(new Task_Reference_LinkTaskIdRequiredSpec(), _validationStringLocalizer["Task_Reference_LinkTaskIdRequiredSpec"]));
        }
    }
}
