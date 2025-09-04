using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ToolGroupSpecs;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ToolGroupValidation : Validation<ToolGroup>, Interfaces.Validation.Core.IToolGroupValidation
    {
        public ToolGroupValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ToolGroup>(new ToolGroupDescriptionRequiredSpec(), _validationStringLocalizer["ToolGroupDescriptionRequired"]));
        }
    }
}
