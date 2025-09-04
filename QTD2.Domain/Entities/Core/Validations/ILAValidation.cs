using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILASpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILAValidation : Validation<ILA>, IILAValidation
    {
        public ILAValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA>(new ILANameRequiredSpec(), _validationStringLocalizer["ILANameRequiredSpec"]));
            //AddRule(new ValidationRule<ILA>(new ILADescriptionRequiredSpec(), _validationStringLocalizer["ILADescriptionRequiredSpec"]));
            AddRule(new ValidationRule<ILA>(new ILAProviderIdRequiredSpec(), _validationStringLocalizer["ILAProviderIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA>(new ILATopicIDRequiredSpec(), _validationStringLocalizer["ILATopicIDRequiredSpec"]));
           // AddRule(new ValidationRule<ILA>(new ILADeliveryMethodIdRequiredSpec(), _validationStringLocalizer["ILADeliveryMethodIdRequiredSpec"]));
        }
    }
}
