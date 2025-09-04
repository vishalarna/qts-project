using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CertificationSubRequirementSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class CertificationSubRequirementValidation : Validation<CertificationSubRequirement>, ICertificationSubRequirementValidation
    {
        public CertificationSubRequirementValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<CertificationSubRequirement>(new CertificationSubRequirement_CertIdRequiredSpec(), _validationStringLocalizer["CertificationSubRequirement_CertIdRequiredSpec"]));
        }
    }
}
