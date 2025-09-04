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
    public class TaskReQualificationEmp_SignOffValidation : Validation<TaskReQualificationEmp_SignOff>, ITaskReQualificationEmp_SignOffValidation
    {
        public TaskReQualificationEmp_SignOffValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<TaskReQualificationEmp_SignOff>(new TaskReQualificationEmp_SignOffSpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
        }
    }

}
