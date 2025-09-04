using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingProgramReview_Employee_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingProgramReview_Employee_LinkValidation : Validation<TrainingProgramReview_Employee_Link>, ITrainingProgramReview_Employee_LinkValidation
    {
        public TrainingProgramReview_Employee_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingProgramReview_Employee_Link>(new TrainingProgramReview_Employee_Link_TrainingProgramReviewIdRequiredSpec(), _validationStringLocalizer["TrainingProgramReview_Employee_Link_TrainingProgramReviewIdRequired"]));
            AddRule(new ValidationRule<TrainingProgramReview_Employee_Link>(new TrainingProgramReview_Employee_Link_EmployeeIdRequiredSpec(), _validationStringLocalizer["TrainingProgramReview_Employee_Link_EmployeeIdRequired"]));
        }
    }
}
