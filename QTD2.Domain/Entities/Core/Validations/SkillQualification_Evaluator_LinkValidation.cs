using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SkillQualification_Evaluator_LinkSpecs;
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
    public class SkillQualification_Evaluator_LinkValidation : Validation<SkillQualification_Evaluator_Link>, ISkillQualification_Evaluator_LinkValidation
    {
        public SkillQualification_Evaluator_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SkillQualification_Evaluator_Link>(new SkillQualification_Evaluator_LinkEvaluatorIdRequiredSpec(), _validationStringLocalizer["SkillQualification_Evaluator_LinkEvaluatorIdRequiredSpec"]));
            AddRule(new ValidationRule<SkillQualification_Evaluator_Link>(new SkillQualification_Evaluator_LinkSQIdRequiredSpec(), _validationStringLocalizer["SkillQualification_Evaluator_LinkSQIdRequiredSpec"]));
        }
    }
}
