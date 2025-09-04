using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_TaskSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_TaskValidation : Validation<Version_Task>, IVersion_TaskValidation
    {
        public Version_TaskValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Task>(new VersionTask_TaskIdRequiredSpec(), _validationStringLocalizer["VersionTaskIdRequired"]));
            AddRule(new ValidationRule<Version_Task>(new VersionTask_MajorVersionRequiredSpec(), _validationStringLocalizer["VersionTask_MajorVersionRequired"]));
            AddRule(new ValidationRule<Version_Task>(new VersionTask_MinorVersionRequiredSpec(), _validationStringLocalizer["VersionTask_MinorVersionRequired"]));
        }
    }
}
