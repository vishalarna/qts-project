using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_TopicLinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_Topic_LinkValidation : Validation<ILA_Topic_Link>, IILA_Topic_LinkValidation
    {
        public ILA_Topic_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_Topic_Link>(new ILA_Topic_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_Topic_LinkILAIdRequired"]));
            AddRule(new ValidationRule<ILA_Topic_Link>(new ILA_Topic_LinkILATopicIdRequiredSpec(), _validationStringLocalizer["ILA_Topic_LinkILATopicIdRequired"]));
        }
    }
}
