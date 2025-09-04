using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_RegRequirement_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_RegRequirement_LinkValidation : Validation<ILA_RegRequirement_Link>, IILA_RegRequirement_LinkValidation
    {
        public ILA_RegRequirement_LinkValidation(IStringLocalizerFactory localizer)
            : base(localizer)
        {
            AddRule(new ValidationRule<ILA_RegRequirement_Link>(new ILA_RegReq_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_RegReq_LinkILAIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA_RegRequirement_Link>(new ILA_RegReq_LinkRegReqIdRequiredSpec(), _validationStringLocalizer["ILA_RegReq_LinkRegReqIdRequiredSpec"]));
        }
    }
}
