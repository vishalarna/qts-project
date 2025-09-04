using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_ILASpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_ILAValidation : Validation<Version_ILA>, IVersion_ILAValidation
    {
        public Version_ILAValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_ILA>(new Version_ILA_ILAIdRequiredSpec(), _validationStringLocalizer["VersionILAIdRequired"]));
            AddRule(new ValidationRule<Version_ILA>(new Version_ILA_MajorVersionRequiredSpec(), _validationStringLocalizer["VersionILA_MajorVersionRequired"]));
            AddRule(new ValidationRule<Version_ILA>(new Version_ILA_MinorVersionRequiredSpec(), _validationStringLocalizer["VersionILA_MinorVersionRequired"]));
            AddRule(new ValidationRule<Version_ILA>(new Version_ILA_VersionNumberRequiredSpec(), _validationStringLocalizer["Version_ILA_VersionNumberRequiredSpec"]));
        }
    }
}
