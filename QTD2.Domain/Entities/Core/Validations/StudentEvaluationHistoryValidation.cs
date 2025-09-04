using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.StudentEvaluationHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class StudentEvaluationHistoryValidation : Validation<StudentEvaluationHistory>, IStudentEvaluationHistoryValidation
    {
        public StudentEvaluationHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<StudentEvaluationHistory>(new StudentEvaluationHistoryStudentEvaluationIdRequiredSpec(), _validationStringLocalizer["studentEvaluationIdRequired"]));
          
        }
    }
}