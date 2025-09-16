using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SkillQualificationEmp_SignOffSpecs;
using QTD2.Domain.Entities.Core.Specifications.SkillReQualificationEmp_QuestionAnswerSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SkillReQualificationEmp_QuestionAnswerValidation : Validation<SkillReQualificationEmp_QuestionAnswer>, ISkillReQualificationEmp_QuestionAnswerValidation
    {
        public SkillReQualificationEmp_QuestionAnswerValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SkillReQualificationEmp_QuestionAnswer>(new SkillReQualificationEmp_QuestionAnswerSQIdRequiredSpec(), _validationStringLocalizer["SkillReQualificationEmp_QuestionAnswerSQIdRequiredSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_QuestionAnswer>(new SkillRequalEmp_QA_SkillQuestionIdReqSpec(), _validationStringLocalizer["SkillRequalEmp_QA_SkillQuestionIdReqSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_QuestionAnswer>(new SkillRequalEmp_QA_EvaluatorIdReqSpec(), _validationStringLocalizer["SkillRequalEmp_QA_EvaluatorIdReqSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_QuestionAnswer>(new SkillRequalEmp_QA_TraineeIdReqSpec(), _validationStringLocalizer["SkillRequalEmp_QA_TraineeIdReqSpec"]));
            AddRule(new ValidationRule<SkillReQualificationEmp_QuestionAnswer>(new SkillRequalEmp_QA_CommentDateReqSpec(), _validationStringLocalizer["SkillRequalEmp_QA_CommentDateReqSpec"]));
        }
    }
}
