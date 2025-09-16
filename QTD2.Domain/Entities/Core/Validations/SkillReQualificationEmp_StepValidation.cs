using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SkillReQualificationEmp_StepSpec;
using QTD2.Domain.Entities.Core.Specifications.SkillReQualificationEmp_SuggestionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SkillReQualificationEmp_StepValidation : Validation<SkillReQualificationEmp_Step>, ISkillReQualificationEmp_StepValidation
    {
        public SkillReQualificationEmp_StepValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SkillReQualificationEmp_Step>(new SkillReQualificationEmp_StepSQIdRequiredSpec(), _validationStringLocalizer["SkillReQualificationEmp_StepSQIdRequiredSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_Step>(new SkillReQualificationEmp_StepSkillStepIDRequiredSpec(), _validationStringLocalizer["SkillReQualificationEmp_StepSkillStepIDRequiredSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_Step>(new SkillRequalEmp_StepEvalIdReqSpec(), _validationStringLocalizer["SkillRequalEmp_StepEvalIdReqSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_Step>(new SkillRequalEmp_StepTraineeIdReqSpec(), _validationStringLocalizer["SkillRequalEmp_StepTraineeIdReqSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_Step>(new SkillRequalEmp_StepCommentDateReqSpec(), _validationStringLocalizer["SkillRequalEmp_StepCommentDateReqSpec"]));
        }
    }
}
