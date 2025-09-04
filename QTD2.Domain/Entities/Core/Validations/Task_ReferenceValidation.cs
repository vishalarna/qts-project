using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_ReferenceSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_ReferenceValidation : Validation<Task_Reference>, ITask_ReferenceValidation
    {
        public Task_ReferenceValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_Reference>(new Task_ReferenceDisplayNameRequiredSpec(), _validationStringLocalizer["Task_ReferenceDisplayNameRequiredSpec"]));
            AddRule(new ValidationRule<Task_Reference>(new Task_ReferenceDescriptionRequiredSpec(), _validationStringLocalizer["Task_ReferenceDescriptionRequiredSpec"]));
        }
    }
}
