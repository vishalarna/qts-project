using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Employee_ILASpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class IDPValidation : Validation<IDP>, Interfaces.Validation.Core.IIDPValidation
    {
        public IDPValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<IDP>(new IDPIlaIdRequiredSpec(), _validationStringLocalizer["Employee_ILAIlaIdRequiredSpec"]));
            AddRule(new ValidationRule<IDP>(new IDPEmpIdRequiredSpec(), _validationStringLocalizer["Employee_ILAEmpIdRequiredSpec"]));
        }
    }
}
