using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.AssessmentToolSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class AssessmentToolValidation : Validation<AssessmentTool>, IAssessmentToolValidation
    {
        public AssessmentToolValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<AssessmentTool>(new AssessmentToolNameRequiredSpec(), _validationStringLocalizer["AssessmentToolNameRequiredSpec"]));
        }
    }
}
