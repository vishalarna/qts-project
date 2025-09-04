using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILATopicSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_TopicValidation : Validation.Validation<ILA_Topic>, IILA_TopicValidation
    {
        public ILA_TopicValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_Topic>(new ILATopicNameRequiredSpec(), _validationStringLocalizer["ILATopicNameRequiredSpec"]));
        }
    }
}
