using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SkillQualificationEmp_SignOffSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SkillQualificationEmp_SignOffValidation : Validation<SkillQualificationEmp_SignOff>, ISkillQualificationEmp_SignOffValidation
    {
        public SkillQualificationEmp_SignOffValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SkillQualificationEmp_SignOff>(new SkillQualificationEmp_SignOffSQIdRequiredSpec(), _validationStringLocalizer["SkillQualificationEmp_SignOffSQIdRequiredSpec"]));
            AddRule(new ValidationRule<SkillQualificationEmp_SignOff>(new SkillQualificationEmp_SignOffEvaluatorIdRequiredSpec(), _validationStringLocalizer["SkillQualificationEmp_SignOffEvaluatorIdRequiredSpec"]));
            AddRule(new ValidationRule<SkillQualificationEmp_SignOff>(new SkillQualificationEmp_SignOffTraineeIdRequiredSpec(), _validationStringLocalizer["SkillQualificationEmp_SignOffTraineeIdRequiredSpec"]));
        }
    }
}
