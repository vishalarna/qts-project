using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.StudentEvaluationFormSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class StudentEvaluationFormValidation : Validation<StudentEvaluationForm>, IStudentEvaluationFormValidation
    {
        public StudentEvaluationFormValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<StudentEvaluationForm>(new StudentEvalFormNameRequiredSpec(), _validationStringLocalizer["StudentEvalFormNameRequired"]));
            AddRule(new ValidationRule<StudentEvaluationForm>(new StudentEvalFormRatingScaleIdRequiredSpec(), _validationStringLocalizer["StudentEvalFormRatingScaleIdRequired"]));
        }
    }
}
