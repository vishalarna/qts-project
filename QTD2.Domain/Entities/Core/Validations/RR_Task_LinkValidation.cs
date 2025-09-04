using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.RR_Task_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class RR_Task_LinkValidation : Validation<RR_Task_Link>, IRR_Task_LinkValidation
    {
        public RR_Task_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<RR_Task_Link>(new RR_Task_LinkTaskIdRequiredSpec(), _validationStringLocalizer["RR_Task_LinkTaskIdRequired"]));
            AddRule(new ValidationRule<RR_Task_Link>(new RR_Task_LinkRRIdRequiredSpec(), _validationStringLocalizer["RR_Task_LinkRRIdRequireds"]));
        }
    }
}
