using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.ClassSchedule_TestReleaseEMPSettingsSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClassSchedule_TestReleaseEMPSetting_Retake_LinksValidation : Validation<ClassSchedule_TestReleaseEMPSetting_Retake_Link>, IClassSchedule_TestReleaseEMPSetting_Retake_LinksValidation
    {
        public ClassSchedule_TestReleaseEMPSetting_Retake_LinksValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClassSchedule_TestReleaseEMPSetting_Retake_Link>(new ClassSchedule_TestReleaseEMPSetting_Retake_LinksClassSchedule_TestReleaseSettingIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_TestReleaseEMPSetting_Retake_LinksClassSchedule_TestReleaseSettingIdRequiredSpec"]));
            AddRule(new ValidationRule<ClassSchedule_TestReleaseEMPSetting_Retake_Link>(new ClassSchedule_TestReleaseEMPSetting_Retake_LinksReTakeTestIdRequiredSpec(), _validationStringLocalizer["ClassSchedule_TestReleaseEMPSetting_Retake_LinksReTakeTestIdRequiredSpec"]));
        }
    }
}