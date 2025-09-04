using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILA_ImplementSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILA_ImplementValidation : Validation<InstructorWorkbook_ILA_Implement>, IInstructorWorkbook_ILA_ImplementValidation
    {
        public InstructorWorkbook_ILA_ImplementValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILA_Implement>(new InstructorWorkbook_ILA_ImplementImplementResultRequiredSpecs(), _validationStringLocalizer["InstructorWorkbook_ILA_ImplementImplementResultRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Implement>(new InstructorWorkbook_ILA_ImplementInstructorCommentsRequiredSpecs(), _validationStringLocalizer["InstructorWorkbook_ILA_ImplementInstructorCommentsRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Implement>(new InstructorWorkbook_ILA_ImplementReviewerCommentsRequiredSpecs(), _validationStringLocalizer["InstructorWorkbook_ILA_ImplementReviewerCommentsRequired"]));
        }
    }
}
