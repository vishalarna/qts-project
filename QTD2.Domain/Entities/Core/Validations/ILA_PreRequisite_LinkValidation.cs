using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_PreRequisite_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_PreRequisite_LinkValidation : Validation<ILA_PreRequisite_Link>, IILA_PreRequisite_LinkValidation
    {
        public ILA_PreRequisite_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_PreRequisite_Link>(new ILA_PR_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_PR_LinkILAIdRequired"]));
            AddRule(new ValidationRule<ILA_PreRequisite_Link>(new ILA_PR_LinkPreReqIdRequiredSpec(), _validationStringLocalizer["ILA_PR_LinkPreReqIdRequired"]));
        }
    }
}
