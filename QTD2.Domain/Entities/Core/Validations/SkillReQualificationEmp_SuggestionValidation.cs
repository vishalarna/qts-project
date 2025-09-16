using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SkillQualificationEmp_SignOffSpecs;
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
    public class SkillReQualificationEmp_SuggestionValidation : Validation<SkillReQualificationEmp_Suggestion>, ISkillReQualificationEmp_SuggestionValidation
    {
        public SkillReQualificationEmp_SuggestionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SkillReQualificationEmp_Suggestion>(new SkillReQualificationEmp_SuggestionSQIdRequiredSpec(), _validationStringLocalizer["SkillReQualificationEmp_SuggestionSQIdRequiredSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_Suggestion>(new SkillRequalEmp_SkillSuggestionIdReqSpec(), _validationStringLocalizer["SkillRequalEmp_SkillSuggestionIdReqSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_Suggestion>(new SkillRequalEmp_SuggestionEvaluatorIdReqSpec(), _validationStringLocalizer["SkillRequalEmp_SuggestionEvaluatorIdReqSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_Suggestion>(new SkillRequalEmp_SuggestionTraineeIdReqSpec(), _validationStringLocalizer["SkillRequalEmp_SuggestionTraineeIdReqSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_Suggestion>(new SkillRequalEmp_SuggestionCommentDateReqSpec(), _validationStringLocalizer["SkillRequalEmp_SuggestionCommentDateReqSpec"]));
        }
    }
}
