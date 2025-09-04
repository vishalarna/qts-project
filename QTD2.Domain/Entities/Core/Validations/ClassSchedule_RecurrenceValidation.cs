using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_RecurrenceSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_RecurrenceValidation : Validation<ClassSchedule_Recurrence>, IClassSchedule_RecurrenceValidation
    {
        public ClassSchedule_RecurrenceValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_Recurrence>(new ClassSchedule_RecurrenceClassIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_RecurrenceClassIdRequiredSpec"]));
        }
    }
}
