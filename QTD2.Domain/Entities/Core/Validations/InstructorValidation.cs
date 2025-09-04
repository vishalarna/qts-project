using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.InstructorSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorValidation : Validation<Instructor>, IInstructor_Validation
    {
        public InstructorValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Instructor>(new Instructor_NameRequiredSpec(), _validationStringLocalizer["Instructor_NameRequired"]));
            AddRule(new ValidationRule<Instructor>(new Instructor_CatIdRequiredSpec(), _validationStringLocalizer["Instructor_CatIdRequired"]));
            AddRule(new ValidationRule<Instructor>(new Instructor_NumberRequiredSpec(), _validationStringLocalizer["Instructor_NumberRequired"]));
        }
    }
}
