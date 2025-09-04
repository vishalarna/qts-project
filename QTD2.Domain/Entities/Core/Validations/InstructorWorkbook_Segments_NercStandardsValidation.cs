using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_Segments_NercStandardsValidation : Validation<InstructorWorkbook_Segments_NercStandards>, IInstructorWorkbook_Segments_NercStandardsValidation
    {
        public InstructorWorkbook_Segments_NercStandardsValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
        }
    }
}
