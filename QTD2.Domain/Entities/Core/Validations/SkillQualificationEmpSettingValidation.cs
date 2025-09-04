using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.SkillQualificationEmpSettingSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SkillQualificationEmpSettingValidation : Validation<SkillQualificationEmpSetting>, ISkillQualificationEmpSettingValidation
    {
        public SkillQualificationEmpSettingValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SkillQualificationEmpSetting>(new SkillQualificationEmpSetting_SQIdRequiredSpec(), _validationStringLocalizer["SkillQualificationEmpSetting_SQIdRequiredSpec"]));
        }
    }
}
