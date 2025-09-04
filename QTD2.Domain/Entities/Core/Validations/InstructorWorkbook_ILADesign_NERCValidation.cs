using Microsoft.Extensions.Localization;

using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILADesign_NERCValidation : Validation<InstructorWorkbook_ILADesign_NERC>, IInstructorWorkbook_ILADesign_NERCValidation
    {
        public InstructorWorkbook_ILADesign_NERCValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {

        }
    }
}