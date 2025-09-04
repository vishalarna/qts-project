using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_Segments_LinkObjectivesSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class InstructorWorkbook_Segments_LinkObjectivesValidation : Validation<InstructorWorkbook_Segments_LinkObjectives>, IInstructorWorkbook_Segments_LinkObjectivesValidation
    {
        public InstructorWorkbook_Segments_LinkObjectivesValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_Segments_LinkObjectives>(new InstructorWorkbook_Segments_LinkObjectivesNumberRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_Segments_LinkObjectivesNumberRequired"]));
            AddRule(new ValidationRule<InstructorWorkbook_Segments_LinkObjectives>(new InstructorWorkbook_Segments_LinkObjectivesDescriptionRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_Segments_LinkObjectivesDescriptionRequired"]));
            AddRule(new ValidationRule<InstructorWorkbook_Segments_LinkObjectives>(new InstructorWorkbook_Segments_LinkObjectivesOrderRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_Segments_LinkObjectivesOrderRequiredSpec"]));
            
        }
    }
}