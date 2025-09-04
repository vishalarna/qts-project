using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TaskReQualificationEmp_QuestionAnswerValidation : Validation<TaskReQualificationEmp_QuestionAnswer>, ITaskReQualificationEmp_QuestionAnswerValidation
    {
        public TaskReQualificationEmp_QuestionAnswerValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<TaskReQualificationEmp_QuestionAnswer>(new TaskReQualificationEmp_QuestionAnswerSpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
        }
    }

}
