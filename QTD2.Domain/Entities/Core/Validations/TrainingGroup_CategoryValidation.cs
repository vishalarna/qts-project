using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingGroup_CategorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingGroup_CategoryValidation : Validation<TrainingGroup_Category>, ITrainingGroup_CategoryValidation
    {
        public TrainingGroup_CategoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingGroup_Category>(new TrainingGroup_CategoryTitleRequiredSpec(), _validationStringLocalizer["TrainingGroup_CategoryTitleRequiredSpec"]));
        }
    }
}
