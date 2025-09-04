using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_TargetAudienceSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILADesign_TargetAudienceValidation : Validation<InstructorWorkbook_ILADesign_TargetAudience>, IInstructorWorkbook_ILADesign_TargetAudienceValidation
    {
        public InstructorWorkbook_ILADesign_TargetAudienceValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_TargetAudience>(new InstructorWorkbook_ILADesign_TargetAudienceInstructorEmailRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesign_TargetAudienceInstructorEmailRequired"]));

        }
    }
}
