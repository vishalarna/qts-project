using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_Task_ILASpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_Task_ILA_LinkValidation : Validation<Version_Task_ILA_Link>, IVersion_Task_ILA_LinkValidation
    {
        public Version_Task_ILA_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_Task_ILA_Link>(new Version_Task_ILA_Link_VILAIdRequiredSpec(), _validationStringLocalizer["VersionILAIdRequired"]));
            AddRule(new ValidationRule<Version_Task_ILA_Link>(new Version_Task_ILA_Link_VTaskIdRequiredSpec(), _validationStringLocalizer["VersionILA_TaskIdRequired"]));
        }
    }
}
