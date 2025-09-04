using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_TQEMPSettingsSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_TQEMPSettingsValidation : Validation<ClassSchedule_TQEMPSetting>, IClassSchedule_TQEMPSettingsValidation
    {
        public ClassSchedule_TQEMPSettingsValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_TQEMPSetting>(new ClassSchedule_TQEMPSettingsClassScheduleIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_TQEMPSettingsClassScheduleIdRequiredSpec"]));
        }
    }
}