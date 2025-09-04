using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_DelieveryMethodsSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_ILADesign_DelieveryMethodsvalidation : Validation<InstructorWorkbook_ILADesign_DelieveryMethods>, IInstructorWorkbook_ILADesign_DelieveryMethodsValidation
    {
        public InstructorWorkbook_ILADesign_DelieveryMethodsvalidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_DelieveryMethods>(new InstructorWorkbook_ILADesign_DelieveryMethodsInstructorEmailRequiredSpecs(), _validationStringLocalizer["InstructorWorkbook_ILADesign_DelieveryMethodsInstructorEmailRequired"]));
        }
    }
}
