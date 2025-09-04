using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_MetaILASpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_MetaILAValidation : Validation<Version_MetaILA>, IVersion_MetaILAValidation
    {
        public Version_MetaILAValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_MetaILA>(new Version_MetaILANameRequiredSpec(), _validationStringLocalizer["Version_MetaILANameRequiredSpec"]));
            AddRule(new ValidationRule<Version_MetaILA>(new Version_MetaILAStatusRequiredSpec(), _validationStringLocalizer["Version_MetaILAStatusRequiredSpec"]));
        }
    }
}
