using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Procedure_StatusHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Procedure_StatusHistoryValidation : Validation<Procedure_StatusHistory>, IProcedure_StatusHistoryValidation
    {
        public Procedure_StatusHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Procedure_StatusHistory>(new Proc_StatusHistoryProcIdRequiredSpec(), _validationStringLocalizer["Proc_StatusHistoryProcIdRequiredSpec"]));
        }
    }
}
