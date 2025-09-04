using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_ToolSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_ToolValidation : Validation<Version_Tool>, IVersion_ToolValidation
    {
        public Version_ToolValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Tool>(new VT_ToolIdRequiredSpec(), _validationStringLocalizer["VT_ToolIdRequired"]));
            AddRule(new ValidationRule<Version_Tool>(new VT_DescriptionRequiredSpec(), _validationStringLocalizer["VT_DescriptionRequired"]));
            AddRule(new ValidationRule<Version_Tool>(new VT_MinorVersionRequiredSpec(), _validationStringLocalizer["VT_MinorVersionRequired"]));
            AddRule(new ValidationRule<Version_Tool>(new VT_MajorVersionRequiredSpec(), _validationStringLocalizer["VT_MajorVersionRequired"]));
        }
    }
}
