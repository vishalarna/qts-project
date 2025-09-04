using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_PhasesSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_PhasesValidation : Validation<InstructorWorkbook_Phases>, IInstructorWorkbook_PhasesValidation
    {
        public InstructorWorkbook_PhasesValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_Phases>(new InstructorWorkbook_PhasesCoursePhaseDescriptionRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_PhasesCoursePhaseDescriptionRequired"]));

        }
    }
}
