using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TaskQualification_Evaluator_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskQualification_Evaluator_LinkValidation : Validation<TaskQualification_Evaluator_Link>, ITaskQualification_Evaluator_LinkValidation
    {
        public TaskQualification_Evaluator_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TaskQualification_Evaluator_Link>(new TaskQualification_Evaluator_LinkEvalIdRequiredSpec(), _validationStringLocalizer["TaskQualification_Evaluator_LinkEvalIdRequiredSpec"]));
            AddRule(new ValidationRule<TaskQualification_Evaluator_Link>(new TaskQualification_Evaluator_LinkTaskQualIdRequiredSpec(), _validationStringLocalizer["TaskQualification_Evaluator_LinkTaskQualIdRequiredSpec"]));
        }
    }
}
