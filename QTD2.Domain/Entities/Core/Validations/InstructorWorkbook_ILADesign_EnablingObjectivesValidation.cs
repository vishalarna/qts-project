using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_EnablingObjectivesSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILADesign_EnablingObjectivesValidation : Validation<InstructorWorkbook_ILADesign_EnablingObjectives>, IInstructorWorkbook_ILADesign_EnablingObjectivesValidation
    {
        public InstructorWorkbook_ILADesign_EnablingObjectivesValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_EnablingObjectives>(new InstructorWorkbook_ILADesign_EnablingObjectivesEnablingObjectivesDescriptionRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesign_EnablingObjectivesEnablingObjectivesDescriptionRequired"]));
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_EnablingObjectives>(new IWB_ILADesign_EnablingObjectivesILAObjectiveOrderRequiredSpec(), _validationStringLocalizer["IWB_ILADesign_EnablingObjectivesILAObjectiveOrderRequiredSpec"]));
        }
    }
}
