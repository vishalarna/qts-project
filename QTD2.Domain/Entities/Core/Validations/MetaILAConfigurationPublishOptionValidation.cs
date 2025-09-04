using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.MetaILAConfigurationPublishOptionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;


namespace QTD2.Domain.Entities.Core.Validations
{
    public class MetaILAConfigurationPublishOptionValidation : Validation<MetaILAConfigurationPublishOption>, IMetaILAConfigurationPublishOptionValidation
    {
        public MetaILAConfigurationPublishOptionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<MetaILAConfigurationPublishOption>(new MetaILAConfigDescRequiredSpec(), _validationStringLocalizer["MetaILAConfigurationPublishOptionDescriptionRequiredSpec"]));
        }
    }
}
