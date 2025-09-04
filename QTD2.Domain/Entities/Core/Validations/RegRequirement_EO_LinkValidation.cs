using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.RegRequirement_EO_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class RegRequirement_EO_LinkValidation : Validation<RegRequirement_EO_Link>, IRegRequirement_EO_LinkValidation
    {
        public RegRequirement_EO_LinkValidation(IStringLocalizerFactory localizer)
            : base(localizer)
        {
            AddRule(new ValidationRule<RegRequirement_EO_Link>(new RR_LinkEOIdRequiredSpec(), _validationStringLocalizer["RR_LinkEOIdRequiredSpec"]));
            AddRule(new ValidationRule<RegRequirement_EO_Link>(new RR_LinkRRIdRequiredSpec(), _validationStringLocalizer["RR_LinkRRIdRequiredSpec"]));
        }
    }
}
