using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADevelopReviewersSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILADevelopReviewersValidation : Validation<InstructorWorkbook_ILADevelopReviewers>, IInstructorWorkbook_ILADevelopReviewersValidation
    {
        public InstructorWorkbook_ILADevelopReviewersValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILADevelopReviewers>(new InstructorWorkbook_ILADevelopReviewersReviewerIdsRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesignReviewersReviewerIdsRequired"]));

        }
    }
}
