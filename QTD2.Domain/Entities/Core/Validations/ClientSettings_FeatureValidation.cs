using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ClientSettings_FeatureSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ClientSettings_FeatureValidation: Validation<ClientSettings_Feature>, IClientSettings_FeatureValidation
    {
        public ClientSettings_FeatureValidation(IStringLocalizerFactory stringLocalizerFactory)
        : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ClientSettings_Feature>(new ClientSettings_Feature_FeatureRequiredSpec(), _validationStringLocalizer["FeatureRequired"]));
        }
    }
}
