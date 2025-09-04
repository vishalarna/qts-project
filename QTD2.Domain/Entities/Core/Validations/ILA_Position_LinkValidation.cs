using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_Position_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_Position_LinkValidation : Validation<ILA_Position_Link>, IILA_Position_LinkValidation
    {
        public ILA_Position_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_Position_Link>(new ILA_Position_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_Position_LinkILAIdRequired"]));
            AddRule(new ValidationRule<ILA_Position_Link>(new ILA_Position_LinkPositionIdRequiredSpec(), _validationStringLocalizer["ILA_Position_LinkPositionIdRequired"]));
        }
    }
}
