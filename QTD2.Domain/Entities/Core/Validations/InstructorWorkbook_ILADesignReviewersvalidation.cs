using Microsoft.Extensions.Localization;

using QTD2.Domain.Entities.Core.Specifications.InstructorWorkbook_ILADesignReviewersSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;


namespace QTD2.Domain.Entities.Core.Validations
{
    public class InstructorWorkbook_ILADesignReviewersvalidation : Validation<InstructorWorkbook_ILADesignReviewers>, IInstructorWorkbook_ILADesignReviewersValidation
    {
        public InstructorWorkbook_ILADesignReviewersvalidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<InstructorWorkbook_ILADesignReviewers>(new InstructorWorkbook_ILADesignReviewersReviewerIdsRequiredSpec(), _validationStringLocalizer["InstructorWorkbook_ILADesignReviewersReviewerIdsRequired"]));

        }
    }
}
