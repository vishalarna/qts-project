using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.MetaILASpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class MetaILAValidation : Validation<MetaILA>, IMetaILAValidation
    {
        public MetaILAValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<MetaILA>(new MetaILANameRequiredSpec(), _validationStringLocalizer["MetaILANameRequiredSpec"]));
            AddRule(new ValidationRule<MetaILA>(new MetaILAStatusIdRequiredSpec(), _validationStringLocalizer["MetaILAStatusIdRequiredSpec"]));
        }
    }
}
