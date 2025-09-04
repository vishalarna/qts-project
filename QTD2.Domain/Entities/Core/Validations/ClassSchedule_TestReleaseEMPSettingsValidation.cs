using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_TestReleaseEMPSettingsSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_TestReleaseEMPSettingsValidation : Validation<ClassSchedule_TestReleaseEMPSetting>, IClassSchedule_TestReleaseEMPSettingsValidation
    {
        public ClassSchedule_TestReleaseEMPSettingsValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_TestReleaseEMPSetting>(new ClassSchedule_TestReleaseEMPSettingsClassScheduleIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_TestReleaseEMPSettingsClassScheduleIdRequiredSpec"]));
        }
    }
}