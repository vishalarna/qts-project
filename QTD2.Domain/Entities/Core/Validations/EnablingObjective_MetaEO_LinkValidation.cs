using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_MetaEO_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_MetaEO_LinkValidation : Validation<EnablingObjective_MetaEO_Link>, IEnablingObjective_MetaEO_LinkValidation
    {
        public EnablingObjective_MetaEO_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_MetaEO_Link>(new EO_MetaEO_LinkEOIdRequiredSpec(), _validationStringLocalizer["EO_MetaEO_LinkEOIdRequiredSpec"]));
            AddRule(new ValidationRule<EnablingObjective_MetaEO_Link>(new EO_MetaEO_LinkMetaEOIdRequiredSpec(), _validationStringLocalizer["EO_MetaEO_LinkMetaEOIdRequiredSpec"]));
        }
    }
}
