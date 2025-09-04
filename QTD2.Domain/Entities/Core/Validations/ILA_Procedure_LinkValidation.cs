using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_Procedure_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_Procedure_LinkValidation : Validation<ILA_Procedure_Link>, IILA_Procedure_LinkValidation
    {
        public ILA_Procedure_LinkValidation(IStringLocalizerFactory localizer)
            : base(localizer)
        {
            AddRule(new ValidationRule<ILA_Procedure_Link>(new ILA_Procedure_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_Procedure_LinkILAIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA_Procedure_Link>(new ILA_Procedure_LinkProcedureIdRequiredSpec(), _validationStringLocalizer["ILA_Procedure_LinkProcedureIdRequiredSpec"]));
        }
    }
}
