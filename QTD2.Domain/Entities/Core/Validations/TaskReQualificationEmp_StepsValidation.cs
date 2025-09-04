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
    public class TaskReQualificationEmp_StepsValidation : Validation<TaskReQualificationEmp_Steps>, ITaskReQualificationEmp_StepsValidation
    {
        public TaskReQualificationEmp_StepsValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<TaskReQualificationEmp_Steps>(new TaskReQualificationEmp_StepsSpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
        }
    }

}
