using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TaskQualificationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskQualificationValidation : Validation<TaskQualification>, ITaskQualificationValidation
    {
        public TaskQualificationValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TaskQualification>(new TaskQualificationTaskIdRequiredSpec(), _validationStringLocalizer["TaskQualificationTaskIdRequiredSpec"]));
            //AddRule(new ValidationRule<TaskQualification>(new TaskQualificationEvaluationIdRequiredSpec(), _validationStringLocalizer["TaskQualificationEvaluationIdRequiredSpec"]));
            AddRule(new ValidationRule<TaskQualification>(new TaskQualificationEmpIdRequiredSpec(), _validationStringLocalizer["TaskQualificationEmpIdRequiredSpec"]));
        }
    }
}
