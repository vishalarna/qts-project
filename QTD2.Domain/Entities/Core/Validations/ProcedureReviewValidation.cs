using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ProcedureReviewValidation : Validation<ProcedureReview>, IProcedureReviewValidation
    {
        public ProcedureReviewValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<ProcedureReview>(new ProcedureReviewSpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
        }
    }

}
