using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_ILALinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_EnablingObjective_ILALinkValidation : Validation<Version_EnablingObjective_ILALink>, IVersion_EnablingObjective_ILALinkValidation
    {
        public Version_EnablingObjective_ILALinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_EnablingObjective_ILALink>(new Version_EnablingObjective_ILALinkEOIdRequiredSpec(), _validationStringLocalizer["Version_EnablingObjective_ILALinkEOIdRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective_ILALink>(new Version_EnablingObjective_ILALinkIlaIdRequiredSpec(), _validationStringLocalizer["Version_EnablingObjective_ILALinkIlaIdRequiredSpec"]));
        }
    }
}
