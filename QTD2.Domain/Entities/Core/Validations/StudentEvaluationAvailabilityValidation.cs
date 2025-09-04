using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.StudentEvaluationAvailabilitySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class StudentEvaluationAvailabilityValidation : Validation<StudentEvaluationAvailability>, IStudentEvaluationAvailabilityValidation
    {
        public StudentEvaluationAvailabilityValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<StudentEvaluationAvailability>(new StudentEvaluationAvailabilityNameRequiredSpec(), _validationStringLocalizer["StudentEvaluationAvailabilityNameRequiredSpec"]));
        }
    }
}
