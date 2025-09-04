using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Test_Item_LinkSpecs;
using QTD2.Domain.Entities.Core.Specifications.TrainingProgramReview_TrainingIssue_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingProgramReview_TrainingIssue_LinkValidation : Validation<TrainingProgramReview_TrainingIssue_Link>, ITrainingProgramReview_TrainingIssue_LinkValidation
    {
        public TrainingProgramReview_TrainingIssue_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory) 
        {
            AddRule(new ValidationRule<TrainingProgramReview_TrainingIssue_Link>(new TrainingProgramReview_TrainingIssue_LinkTrainingProgramIdSpec(), _validationStringLocalizer["TrainingProgramReview_TrainingIssue_LinkTrainingProgramIdSpec"]));
            AddRule(new ValidationRule<TrainingProgramReview_TrainingIssue_Link>(new TrainingProgramReview_TrainingIssue_LinkTrainingIssueIdSpec(), _validationStringLocalizer["TrainingProgramReview_TrainingIssue_LinkTrainingIssueIdSpec"]));
        }
    }
}
