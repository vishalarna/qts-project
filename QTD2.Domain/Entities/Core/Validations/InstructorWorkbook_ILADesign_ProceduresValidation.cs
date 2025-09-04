using Microsoft.Extensions.Localization;

using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_ILADesign_ProceduresValidation : Validation<InstructorWorkbook_ILADesign_Procedures>, IInstructorWorkbook_ILADesign_ProceduresValidation
    {
        public InstructorWorkbook_ILADesign_ProceduresValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {

        }
    }
}
