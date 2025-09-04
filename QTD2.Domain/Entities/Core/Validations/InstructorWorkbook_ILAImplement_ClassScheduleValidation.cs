using Microsoft.Extensions.Localization;

using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILAImplement_ClassScheduleValidation : Validation<InstructorWorkbook_ILAImplement_ClassSchedule>, IInstructorWorkbook_ILAImplement_ClassScheduleValidation
    {
        public InstructorWorkbook_ILAImplement_ClassScheduleValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {

        }
    }
}
