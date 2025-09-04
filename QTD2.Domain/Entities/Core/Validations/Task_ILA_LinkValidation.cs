using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_ILA_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_ILA_LinkValidation : Validation<Task_ILA_Link>, ITask_ILA_LinkValidation
    {
        public Task_ILA_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_ILA_Link>(new Task_ILA_LinkILAIdRequiredSpec(), _validationStringLocalizer["Task_ILA_LinkILAIdRequiredSpec"]));
            AddRule(new ValidationRule<Task_ILA_Link>(new Task_ILA_LinkTaskIdRequiredSpec(), _validationStringLocalizer["Task_ILA_LinkTaskIdRequiredSpec"]));
        }
    }
}
