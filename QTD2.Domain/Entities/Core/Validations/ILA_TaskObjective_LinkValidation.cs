using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_TaskObjective_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_TaskObjective_LinkValidation : Validation<ILA_TaskObjective_Link>, IILA_TaskObjective_LinkValidation
    {
        public ILA_TaskObjective_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_TaskObjective_Link>(new ILA_TO_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_TO_LinkILAIdRequired"]));
            AddRule(new ValidationRule<ILA_TaskObjective_Link>(new ILA_TO_Link_TaskIdRequiredSpec(), _validationStringLocalizer["ILA_TO_Link_TaskIdRequired"]));
        }
    }
}
