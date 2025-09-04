using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_MetaEOLinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_EnablingObjective_MetaEOLinkValidation : Validation<Version_EnablingObjective_MetaEOLink>, IVersion_EnablingObjective_MetaEOLinkValidation
    {
        public Version_EnablingObjective_MetaEOLinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_EnablingObjective_MetaEOLink>(new Version_EnablingObjective_MetaEOLinkEOIdRequiredSpec(), _validationStringLocalizer["Version_EnablingObjective_MetaEOLinkEOIdRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective_MetaEOLink>(new Version_EnablingObjective_MetaEOLinkMetaEOIdRequiredSpec(), _validationStringLocalizer["Version_EnablingObjective_MetaEOLinkMetaEOIdRequiredSpec"]));
        }
    }
}
