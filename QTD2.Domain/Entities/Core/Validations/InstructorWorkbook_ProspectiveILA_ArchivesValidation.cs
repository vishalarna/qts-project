using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ProspectiveILA_ArchivesSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
  public  class InstructorWorkbook_ProspectiveILA_ArchivesValidation : Validation<InstructorWorkbook_ProspectiveILA_Archives>, IInstructorWorkbook_ProspectiveILA_ArchivesValidation
    {
        public InstructorWorkbook_ProspectiveILA_ArchivesValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ProspectiveILA_Archives>(new InstructorWorkbook_ProspectiveILA_ArchivesILAIdRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ProspectiveILA_ArchivesILAIdRequired"]));

        }
    }
}
