using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILAImplementReviewersSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILAImplementReviewersValidation : Validation<InstructorWorkbook_ILAImplementReviewers>, IInstructorWorkbook_ILAImplementReviewersValidation
    {
        public InstructorWorkbook_ILAImplementReviewersValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILAImplementReviewers>(new InstructorWorkbook_ILAImplementReviewersReviewerIdsRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILAImplementReviewersReviewerIdsRequired"]));

        }
    }
}