using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_NERCAudience_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_NERCAudience_LinkValidation : Validation<ILA_NERCAudience_Link>, IILA_NERCAudience_LinkValidation
    {
        public ILA_NERCAudience_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_NERCAudience_Link>(new ILA_NERCAudience_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_NERCAudience_LinkILAIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA_NERCAudience_Link>(new ILA_NERCAudience_LinkNERCAudienceIdRequiredSpec(), _validationStringLocalizer["ILA_NERCAudience_LinkNERCAudienceIdRequiredSpec"]));
        }
    }
}
