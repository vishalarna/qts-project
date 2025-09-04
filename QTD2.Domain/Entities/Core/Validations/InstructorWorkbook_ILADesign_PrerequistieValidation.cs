using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_PrerequistieSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILADesign_PrerequistieValidation : Validation<InstructorWorkbook_ILADesign_Prerequistie>, IInstructorWorkbook_ILADesign_PrerequistieValidation
    {
        public InstructorWorkbook_ILADesign_PrerequistieValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_Prerequistie>(new InstructorWorkbook_ILADesign_PrerequistieRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesign_DelieveryMethodsInstructorEmailRequired"]));
        }
    }
}
