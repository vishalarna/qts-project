using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.ILA_StudentEvaluation_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ILA_StudentEvaluation_LinkValidation : Validation<ILA_StudentEvaluation_Link>, IILA_StudentEvaluation_LinkValidation
    {
        public ILA_StudentEvaluation_LinkValidation(IStringLocalizerFactory localizer)
           : base(localizer)
        {
            AddRule(new ValidationRule<ILA_StudentEvaluation_Link>(new ILA_SE_Link_stdEvalAvIdRequiredSpec(), _validationStringLocalizer["ILA_SE_Link_stdEvalAvIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA_StudentEvaluation_Link>(new ILA_SE_Link_stdEvalAudIdRequiredSpec(), _validationStringLocalizer["ILA_SE_Link_stdEvalAudIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA_StudentEvaluation_Link>(new ILA_SE_Link_ILAIdRequiredSpec(), _validationStringLocalizer["ILA_SE_Link_ILAIdRequiredSpec"]));
            AddRule(new ValidationRule<ILA_StudentEvaluation_Link>(new ILA_SE_Link_stEvalFormIDRequiredSpec(), _validationStringLocalizer["ILA_SE_Link_stEvalFormIDRequiredSpec"]));
        }
    }
}
