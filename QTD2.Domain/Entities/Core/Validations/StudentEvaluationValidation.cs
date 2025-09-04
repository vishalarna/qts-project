using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.StudentEvaluationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class StudentEvaluationValidation : Validation<StudentEvaluation>, IStudentEvaluationValidation
    {
        public StudentEvaluationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<StudentEvaluation>(new StudentEvaluationTitleRequiredSpec(), _validationStringLocalizer["studentEvaluationTitleRequired"]));
            AddRule(new ValidationRule<StudentEvaluation>(new StudentEvaluationRatingScaleIdRequiredSpec(), _validationStringLocalizer["studentEvaluationRatingScaleIdRequired"]));
        }
    }
}
