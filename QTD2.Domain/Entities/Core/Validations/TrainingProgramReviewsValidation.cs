using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingProgramReviewsSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingProgramReviewsValidation : Validation<TrainingProgramReview>, ITrainingProgramReviewsValidation
    {
        public TrainingProgramReviewsValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingProgramReview>(new TrainingProgramReviews_TrainingProgramIdRequiredSpec(), _validationStringLocalizer["TrainingProgramReviews_TrainingProgramIdRequired"]));
        }
    }
}
