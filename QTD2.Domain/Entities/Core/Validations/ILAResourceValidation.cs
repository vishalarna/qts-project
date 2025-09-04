using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using QTD2.Domain.Entities.Core.Specifications.ILA_ResourceSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILAResourceValidation : Validation<ILA_Resource>, IILAResourceValidation
    {
        public ILAResourceValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_Resource>(new ILAResourceILAIdRequiredSpec(), _validationStringLocalizer["ILAResourceILAIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA_Resource>(new ILAResourceTitleRequiredSpec(), _validationStringLocalizer["ILAResourceTitleRequiredSpec"]));;
        }
    }
}

