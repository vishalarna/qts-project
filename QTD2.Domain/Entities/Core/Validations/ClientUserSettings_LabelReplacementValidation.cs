using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClientSettings_LabelReplacementSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;


namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClientUserSettings_LabelReplacementValidation : Validation<ClientSettings_LabelReplacement>, IClientUserSettings_LabelReplacementValidation
    {
        public ClientUserSettings_LabelReplacementValidation(IStringLocalizerFactory stringLocalizerFactory)
        : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClientSettings_LabelReplacement>(new ClientSettings_LabelReplacement_DefaultLabelRequiredSpec(), _validationStringLocalizer["DefaultLabelRequired"]));
        }
    }
}
