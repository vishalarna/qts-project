using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SaftyHazard_RR_LinkSpecs;
using QTD2.Domain.Entities.Core.Specifications.SkillQualificationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SkillQualificationValidation : Validation<SkillQualification>, ISkillQualificationValidation
    {
        public SkillQualificationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SkillQualification>(new SkillQualification_EnablingObjectiveIdRequiredSpec(), _validationStringLocalizer["SkillQualification_EnablingObjectiveIdRequiredSpec"]));
            AddRule(new ValidationRule<SkillQualification>(new SkillQualificationEmployeeIdRequiredSpec(), _validationStringLocalizer["SkillQualificationEmployeeIdRequiredSpec"]));
        }
    }
}
