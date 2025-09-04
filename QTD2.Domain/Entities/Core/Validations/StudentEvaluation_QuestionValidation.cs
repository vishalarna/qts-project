using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.StudentEvaluation_QuestionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class StudentEvaluation_QuestionValidation : Validation<StudentEvaluation_Question>, IStudentEvaluation_QuestionValidation
    {
        public StudentEvaluation_QuestionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<StudentEvaluation_Question>(new StudentEvaluation_QuestionStudentEvaluationIdRequiredSpec(), _validationStringLocalizer["studentEvaluationIdRequired"]));
            AddRule(new ValidationRule<StudentEvaluation_Question>(new StudentEvaluation_QuestionQuestionIdRequiredSpec(), _validationStringLocalizer["studentEvaluationQuestionIdRequired"]));
        }
    }
}