using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CertificationSpecs;
using QTD2.Domain.Entities.Core.Specifications.EnablingObjective_ToolSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class EnablingObjective_ToolValidation: Validation<EnablingObjective_Tool>, IEnablingObjective_ToolValidation
    {
        public EnablingObjective_ToolValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<EnablingObjective_Tool>(new EnablingObjective_Tool_ToolIdRequiredSpec(), _validationStringLocalizer["EnablingObjective_Tool_ToolIdRequiredSpec"]));
            AddRule(new ValidationRule<EnablingObjective_Tool>(new EnablingObjective_Tool_EOIdRequiredSpec(), _validationStringLocalizer["EnablingObjective_Tool_EOIdRequiredSpec"]));
        }
    }
}
