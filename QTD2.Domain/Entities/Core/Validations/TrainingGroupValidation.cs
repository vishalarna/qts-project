using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TrainingGroupSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TrainingGroupValidation : Validation<TrainingGroup>, ITrainingGroupValidation
    {
        public TrainingGroupValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TrainingGroup>(new TrainingGroupCategoryIdRequiredSpec(), _validationStringLocalizer["TrainingGroupCategoryIdRequiredSpec"]));
            AddRule(new ValidationRule<TrainingGroup>(new TrainingGroupNameRequiredSpec(), _validationStringLocalizer["TrainingGroupNameRequiredSpec"]));
            AddRule(new ValidationRule<TrainingGroup>(new TrainingGroupNumberRequiredSpec(), _validationStringLocalizer["TrainingGroupNumberRequiredSpec"]));
        }
    }
}
