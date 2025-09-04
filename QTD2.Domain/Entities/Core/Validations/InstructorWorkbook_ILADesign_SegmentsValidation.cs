using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesign_SegmentsSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_ILADesign_SegmentsValidation : Validation<InstructorWorkbook_ILADesign_Segments>, IInstructorWorkbook_ILADesign_SegmentsValidation
    {
        public InstructorWorkbook_ILADesign_SegmentsValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_Segments>(new InstructorWorkbook_ILADesign_SegmentsSegmentTextRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesign_SegmentsSegmentTextRequired"]));
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_Segments>(new InstructorWorkbook_ILADesign_SegmentsSegmentIdRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesign_SegmentsSegmentIdRequired"]));
            AddRule(new ValidationRule<InstructorWorkbook_ILADesign_Segments>(new InstructorWorkbook_ILADesign_SegmentsSegmentTitleRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesign_SegmentsSegmentTitleRequiredSpec"]));

        }
    }
}
