using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_TasksSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_ILADesign_TasksValidation : Validation<InstructorWorkbook_ILADesign_Tasks>, IInstructorWorkbook_ILADesign_TasksValidation
    {
        public InstructorWorkbook_ILADesign_TasksValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_Tasks>(new InstructorWorkbook_ILADesign_TasksValidationILAobjectiveorderRequiredSpecs(), _validationStringLocalizer["InstructorWorkbook_ILADesign_TasksValidationILAobjectiveorderRequiredSpecs"]));
        }
    }
}
