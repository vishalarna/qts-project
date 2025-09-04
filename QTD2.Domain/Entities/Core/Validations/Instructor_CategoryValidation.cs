using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Instructor_CategorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Instructor_CategoryValidation : Validation<Instructor_Category>, IInstructor_CategoryValidation
    {
        public Instructor_CategoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Instructor_Category>(new Instructor_CategoryTitleRequiredSpec(), _validationStringLocalizer["Instructor_CategoryTitleRequired"]));
            //AddRule(new ValidationRule<Instructor_Category>(new Instructor_CategoryDescRequiredSpec(), _validationStringLocalizer["Instructor_CategoryDescRequired"]));
        }
    }
}
