using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_Evaluator_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_Evaluator_LinkValidation : Validation<ILA_Evaluator_Link>, IILA_Evaluator_LinkValidation
    {
        public ILA_Evaluator_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<ILA_Evaluator_Link>(new ILA_Evaluator_LinkIlaIdRequiredSpec(), _validationStringLocalizer["ILA_Evaluator_LinkIlaIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA_Evaluator_Link>(new ILA_Evaluator_LinkEvalIdRequiredSpec(), _validationStringLocalizer["ILA_Evaluator_LinkEvalIdRequiredSpec"]));
        }
    }
}
