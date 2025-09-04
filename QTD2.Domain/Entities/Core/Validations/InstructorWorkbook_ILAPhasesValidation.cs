using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_ILAPhasesValidation : Validation<InstructorWorkbook_ILAPhases>, IInstructorWorkbook_ILAPhasesValidation
    {
        public InstructorWorkbook_ILAPhasesValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {

        }
    }
}
