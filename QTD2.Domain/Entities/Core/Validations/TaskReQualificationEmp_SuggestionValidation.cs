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
    public class TaskReQualificationEmp_SuggestionValidation : Validation<TaskReQualificationEmp_Suggestion>, ITaskReQualificationEmp_SuggestionValidation
    {
        public TaskReQualificationEmp_SuggestionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<TaskReQualificationEmp_Suggestion>(new TaskReQualificationEmp_SuggestionSpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
        }
    }

}
