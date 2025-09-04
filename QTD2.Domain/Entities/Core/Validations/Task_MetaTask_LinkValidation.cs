using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_MetaTask_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_MetaTask_LinkValidation : Validation<Task_MetaTask_Link> , ITask_MetaTask_LinkValidation
    {
        public Task_MetaTask_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_MetaTask_Link>(new Task_MetaTask_LinkTaskIdRequiredSpec(), _validationStringLocalizer["Task_MetaTask_LinkTaskIdRequiredSpec"]));
            AddRule(new ValidationRule<Task_MetaTask_Link>(new Task_MetaTask_LinkMetaTaskIdRequiredSpec(), _validationStringLocalizer["Task_MetaTask_LinkMetaTaskIdRequiredSpec"]));
        }
    }
}
