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
    public class ILACertificationSubRequirementLinkValidation : Validation<ILACertificationSubRequirementLink>, IILACertificationSubRequirementLinkValidation
    {
        public ILACertificationSubRequirementLinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<ILACertificationSubRequirementLink>(new ILACertificationSubRequirementLinkSpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
        }
    }

}
