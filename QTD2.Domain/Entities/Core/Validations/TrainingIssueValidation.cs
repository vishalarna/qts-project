using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssueSpecs;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingIssueValidation : Validation<TrainingIssue>, ITrainingIssueValidation
    {
        public TrainingIssueValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingIssue>(new TrainingIssue_IssueIDRequiredSpec(), _validationStringLocalizer["TrainingIssue_IssueIDRequiredSpec"]));
            AddRule(new ValidationRule<TrainingIssue>(new TrainingIssue_IssueTitleRequiredSpec(), _validationStringLocalizer["TrainingIssue_IssueTitleRequiredSpec"]));
            AddRule(new ValidationRule<TrainingIssue>(new TrainingIssue_DueDateRequiredSpec(), _validationStringLocalizer["TrainingIssue_DueDateRequiredSpec"]));
            AddRule(new ValidationRule<TrainingIssue>(new TrainingIssue_TrainingIssueCreatedDateRequiredSpec(), _validationStringLocalizer["TrainingIssue_TrainingIssueCreatedDateRequiredSpec"]));
            AddRule(new ValidationRule<TrainingIssue>(new TrainingIssue_StatusIdRequiredSpec(), _validationStringLocalizer["TrainingIssue_StatusIdRequiredSpec"]));
            AddRule(new ValidationRule<TrainingIssue>(new TrainingIssue_SeverityIdRequiredSpec(), _validationStringLocalizer["TrainingIssue_SeverityIdRequiredSpec"]));
        }
    }
}
