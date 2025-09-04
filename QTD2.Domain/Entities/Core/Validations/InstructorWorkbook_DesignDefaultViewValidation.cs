using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_DesignDefaultViewValidation : Validation<InstructorWorkbook_DesignDefaultView>, IInstructorWorkbook_DesignDefaultViewValidation
    {
        public InstructorWorkbook_DesignDefaultViewValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_DesignDefaultView>(new InstructorWorkbookDesignDefaultViewRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_DesignDefaultViewRequired"]));

        }
    }
}
