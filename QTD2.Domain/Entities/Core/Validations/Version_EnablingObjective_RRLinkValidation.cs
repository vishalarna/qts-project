using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_RRLinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_EnablingObjective_RRLinkValidation : Validation<Version_EnablingObjective_RRLink>, IVersion_EnablingObjective_RRLinkValidation
    {
        public Version_EnablingObjective_RRLinkValidation(IStringLocalizerFactory stringLocalizerFactory) 
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_EnablingObjective_RRLink>(new Version_EnablingObjective_RRLinkEOIdRequiredSpec(), _validationStringLocalizer["Version_EnablingObjective_RRLinkEOIdRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective_RRLink>(new Version_EnablingObjective_RRLinkRRIdRequiredSpec(), _validationStringLocalizer["Version_EnablingObjective_RRLinkRRIdRequiredSpec"]));
        }
    }
}
