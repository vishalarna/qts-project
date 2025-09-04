using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILA_DevelopSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILA_DevelopValidation : Validation<InstructorWorkbook_ILA_Develop>, IInstructorWorkbook_ILA_DevelopValidation
    {
        public InstructorWorkbook_ILA_DevelopValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILA_Develop>(new InstructorWorkbook_ILA_DevelopResultRequiredSpecs(), _validationStringLocalizer["InstructorWorkbook_ILA_DesignDevelopResultRequired"]));

            AddRule(new ValidationRule<InstructorWorkbook_ILA_Develop>(new InstructorWorkbook_ILA_DevelopInstructorCommentsRequiredSpecs(), _validationStringLocalizer["InstructorWorkbook_ILA_DevelopInstructorCommentsRequired"]));
           
            AddRule(new ValidationRule<InstructorWorkbook_ILA_Develop>(new InstructorWorkbook_ILA_DevelopReviewerCommentsRequiredSpecs(), _validationStringLocalizer["InstructorWorkbook_ILA_DevelopReviewerCommentsRequired"]));
        }
    }
}
