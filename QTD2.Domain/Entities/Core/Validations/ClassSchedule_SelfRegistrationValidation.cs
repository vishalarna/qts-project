using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_SelfRegistrationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_SelfRegistrationValidation : Validation<ClassSchedule_SelfRegistrationOptions>, IClassSchedule_SelfRegistrationValidation
    {
        public ClassSchedule_SelfRegistrationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_SelfRegistrationOptions>(new ClassSchedule_SelfRegistrationOptionsClassScheduleIdRequiredSpec(), _validationStringLocalizer["classSchedule_SelfRegistrationClassIdRequiredSpec"]));
        }
    }
}
