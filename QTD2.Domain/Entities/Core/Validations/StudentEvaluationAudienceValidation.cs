using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.StudentEvaluationAudienceSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class StudentEvaluationAudienceValidation : Validation<StudentEvaluationAudience>, IStudentEvaluationAudienceValidation
    {
        public StudentEvaluationAudienceValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<StudentEvaluationAudience>(new StudentEvaluationAudienceNameRequiredSpec(), _validationStringLocalizer["StudentEvaluationAudienceNameRequiredSpec"]));
        }
    }
}
