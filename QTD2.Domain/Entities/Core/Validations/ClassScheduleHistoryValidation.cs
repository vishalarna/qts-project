using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClassScheduleHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassScheduleHistoryValidation : Validation<ClassScheduleHistory>, IClassScheduleHistoryValidation
    {
        public ClassScheduleHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassScheduleHistory>(new ClassScheduleHistoryClassIdRequiredSpec(), _validationStringLocalizer["ClassIdRequired"]));
        }
    }
}
