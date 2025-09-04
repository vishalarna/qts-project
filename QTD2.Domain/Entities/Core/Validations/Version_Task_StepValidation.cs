using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_Task_StepSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_Task_StepValidation : Validation<Version_Task_Step>, IVersion_Task_StepValidation
    {
        public Version_Task_StepValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Task_Step>(new VTS_VersionTaskIdRequiredSpec(), _validationStringLocalizer["VTS_VersionTaskIdRequired"]));
            AddRule(new ValidationRule<Version_Task_Step>(new VTS_TaskStepIdRequiredSpec(), _validationStringLocalizer["VTS_TaskStepIdRequired"]));

            // AddRule(new ValidationRule<Version_Task_Step>(new VTS_NumberRequiredSpec(), _validationStringLocalizer["VTS_NumberRequired"]));
            AddRule(new ValidationRule<Version_Task_Step>(new VTS_DescriptionRequiredSpec(), _validationStringLocalizer["VTS_DescriptionRequired"]));
        }
    }
}
