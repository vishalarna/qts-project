using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class RatingScaleValidation : Validation.Validation<RatingScale>, IRatingScaleValidation
    {
        public RatingScaleValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
        }
    }
}
