using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_ResourcesSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_ILADesign_ResourcesValidation : Validation<InstructorWorkbook_ILADesign_Resources>, IInstructorWorkbook_ILADesign_ResourcesValidation
    {
        public InstructorWorkbook_ILADesign_ResourcesValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_Resources>(new InstructorWorkbook_ILADesign_ResourcesResourceNameRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesign_ResourcesRequired"]));
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_Resources>(new InstructorWorkbook_ILADesign_ResourcesResourcePathRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesign_ResourcesResourcePathRequired"]));

        }
    }
}
