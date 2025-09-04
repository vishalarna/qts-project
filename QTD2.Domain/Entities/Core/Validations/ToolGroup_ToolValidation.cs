using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ToolGroup_ToolSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ToolGroup_ToolValidation : Validation<ToolGroup_Tool>, IToolGroup_ToolValidation
    {
        public ToolGroup_ToolValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ToolGroup_Tool>(new ToolGroup_ToolToolGroupIdRequiredSpec(), _validationStringLocalizer["ToolGroup_ToolToolGroupIdRequired"]));
            AddRule(new ValidationRule<ToolGroup_Tool>(new ToolGroup_ToolToolIdRequiredSpec(), _validationStringLocalizer["ToolGroup_ToolToolIdRequired"]));
        }
    }
}
