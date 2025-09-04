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
    public class ProcedureReview_EmployeeValidation : Validation<ProcedureReview_Employee>, IProcedureReview_EmployeeValidation
    {
        public ProcedureReview_EmployeeValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<ProcedureReview_Employee>(new ProcedureReview_EmployeeSpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
        }
    }

}
