using Microsoft.Extensions.Localization;

using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILADesign_SafetyHazardsValidation : Validation<InstructorWorkbook_ILADesign_SafetyHazards>, IInstructorWorkbook_ILADesign_SafetyHazardsValidation
    {
        public InstructorWorkbook_ILADesign_SafetyHazardsValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {

        }
    }
}
