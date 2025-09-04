using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassScheduleSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassScheduleValidation : Validation<ClassSchedule>, IClassScheduleValidation
    {
        public ClassScheduleValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule>(new ClassScheduleIdRequiredSpec(), _validationStringLocalizer["IdRequired"]));
        }
    }
}
