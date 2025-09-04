using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_EnablingObjective_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_EnablingObjective_LinkValidation : Validation<ILA_EnablingObjective_Link>, IILA_EnablingObjective_LinkValidation
    {
        public ILA_EnablingObjective_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_EnablingObjective_Link>(new ILA_EO_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_EO_LinkILAIdRequired"]));
            AddRule(new ValidationRule<ILA_EnablingObjective_Link>(new ILA_EO_LinkEOIdRequiredSpec(), _validationStringLocalizer["ILA_EO_LinkEOIdRequired"]));
        }
    }
}
