using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ToolSpecs;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ToolValidation : Validation<Tool>, Interfaces.Validation.Core.IToolValidation
    {
        public ToolValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Tool>(new ToolDescriptionRequiredSpec(), _validationStringLocalizer["ToolDescriptionRequired"]));
        }
    }
}
