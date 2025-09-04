using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingIssuesSpecs.TrainingIssue_DataElementSpecs;
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
    public class TrainingIssue_DataElementValidation : Validation<TrainingIssue_DataElement>, ITrainingIssue_DataElement_Validation
    {
        public TrainingIssue_DataElementValidation(IStringLocalizerFactory stringLocalizerFactory)
          : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingIssue_DataElement>(new TrainingIssue_DataElement_TrainingIssueIdRequiredSpec(), _validationStringLocalizer["TrainingIssue_DataElement_TrainingIssueIdRequiredSpec"]));
        }
    }
}
