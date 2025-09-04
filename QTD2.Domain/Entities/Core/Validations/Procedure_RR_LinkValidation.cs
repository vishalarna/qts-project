using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Procedure_RegRequirement_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Procedure_RR_LinkValidation : Validation<Procedure_RR_Link>, IProcedure_RR_LinkValidation
    {
        public Procedure_RR_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Procedure_RR_Link>(new Proc_RR_LinkProcedureIdRequiredSpec(), _validationStringLocalizer["Proc_SH_LinkProcedureIdRequired"]));
            AddRule(new ValidationRule<Procedure_RR_Link>(new Proc_RR_LinkRegRequirementIdRequiredSpec(), _validationStringLocalizer["Proc_SH_LinkRegRequirementIdRequired"]));
        }
    }
}
