using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Procedure_ILA_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Procedure_ILA_LinkValidation : Validation<Procedure_ILA_Link>, IProcedure_ILA_LinkValidation
    {
        public Procedure_ILA_LinkValidation(IStringLocalizerFactory localizer)
            : base(localizer)
        {
            AddRule(new ValidationRule<Procedure_ILA_Link>(new Procedure_ILA_LinkProcedureIdRequiredSpec(), _validationStringLocalizer["Procedure_ILA_LinkProcedureIdRequiredSpec"]));
            AddRule(new ValidationRule<Procedure_ILA_Link>(new Procedure_ILA_LinkILAIdRequiredSpec(), _validationStringLocalizer["Procedure_ILA_LinkILAIdRequiredSpec"]));
        }
    }
}
