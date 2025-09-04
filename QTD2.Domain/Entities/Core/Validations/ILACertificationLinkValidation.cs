using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILACertificationLinkValidation : Validation<ILACertificationLink>, IILACertificationLinkValidation
    {
        public ILACertificationLinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<ILACertificationLink>(new ILACertificationLinkSpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
        }
    }

}
