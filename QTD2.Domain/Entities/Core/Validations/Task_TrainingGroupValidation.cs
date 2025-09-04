using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Task_TrainingGroupSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Task_TrainingGroupValidation : Validation<Task_TrainingGroup>, ITask_TrainingGroupValidation
    {
        public Task_TrainingGroupValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Task_TrainingGroup>(new Task_TrainingGroupTaskIdRequiredSpec(), _validationStringLocalizer["Task_TrainingGroupTaskIdRequiredSpec"]));
            AddRule(new ValidationRule<Task_TrainingGroup>(new Task_TrainingGroupTrainingGroupIdRequiredSpec(), _validationStringLocalizer["Task_TrainingGroupTrainingGroupIdRequiredSpec"]));
        }
    }
}
