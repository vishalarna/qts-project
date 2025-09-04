using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Procedure_Task_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Procedure_Task_LinkValidation : Validation<Procedure_Task_Link>, IProcedure_Task_LinkValidation
    {
        public Procedure_Task_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Procedure_Task_Link>(new Procedure_Task_LinkProcIdRequiredSpec(), _validationStringLocalizer["Procedure_Task_LinkProcIdRequiredSpec"]));
            AddRule(new ValidationRule<Procedure_Task_Link>(new Procedure_Task_LinkTaskIdRequiredSpec(), _validationStringLocalizer["Procedure_Task_LinkTaskIdRequiredSpec"]));
        }
    }
}
