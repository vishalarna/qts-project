using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.RatingScaleExpandedSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class RatingScaleExpandedValidation : Validation<RatingScaleExpanded>, IRatingScaleExpandedValidation
    {
        public RatingScaleExpandedValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<RatingScaleExpanded>(new RatingScaleExpandedSpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
        }
    }

}
