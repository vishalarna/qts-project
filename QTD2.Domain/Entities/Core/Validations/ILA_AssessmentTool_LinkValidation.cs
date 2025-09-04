using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_AssessmentTool_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_AssessmentTool_LinkValidation : Validation<ILA_AssessmentTool_Link>, IILA_AssessmentTool_LinkValidation
    {
        public ILA_AssessmentTool_LinkValidation(IStringLocalizerFactory localizer)
            : base(localizer)
        {
            AddRule(new ValidationRule<ILA_AssessmentTool_Link>(new ILA_AT_LinkILAIdRequiredSpec(), _validationStringLocalizer["ILA_AT_LinkILAIdRequired"]));
            AddRule(new ValidationRule<ILA_AssessmentTool_Link>(new ILA_AT_LinkAssessmentToolIdRequiredSpec(), _validationStringLocalizer["ILA_AT_LinkAssessmentToolIdRequired"]));
        }
    }
}
