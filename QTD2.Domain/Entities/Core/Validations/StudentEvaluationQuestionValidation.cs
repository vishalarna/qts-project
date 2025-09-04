using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.StudentEvaluationQuestionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class StudentEvaluationQuestionValidation : Validation<StudentEvaluationQuestion>, IStudentEvaluationQuestionValidation
    {
        public StudentEvaluationQuestionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<StudentEvaluationQuestion>(new StudentEvalQuestionEvalFormIDRequiredSpec(), _validationStringLocalizer["StudentEvalQuestionEvalFormIDRequiredSpec"]));
            AddRule(new ValidationRule<StudentEvaluationQuestion>(new StudentEvalQuestionNumberRequiredSpec(), _validationStringLocalizer["StudentEvalQuestionNumberRequiredSpec"]));
            AddRule(new ValidationRule<StudentEvaluationQuestion>(new StudentEvalQuestionTextRequiredSpec(), _validationStringLocalizer["StudentEvalQuestionTextRequiredSpec"]));
        }
    }
}
