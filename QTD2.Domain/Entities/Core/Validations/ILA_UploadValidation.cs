using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_UploadSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_UploadValidation : Validation<ILA_Upload>, IILA_UploadValidation
    {
        public ILA_UploadValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_Upload>(new ILA_UploadIlaIdRequiredSpec(), _validationStringLocalizer["ILA_UploadIlaIdRequired"]));
        }
    }
}
