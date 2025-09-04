using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_TaskSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Version_EnablingObjective_TaskValidation : Validation<Version_EnablingObjective_Task>, IVersion_EnablingObjective_TaskValidation
    {
        public Version_EnablingObjective_TaskValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Version_EnablingObjective_Task>(new Version_EnablingObjective_TaskVersionEOIdRequiredSpec(), _validationStringLocalizer["Version_EnablingObjective_TaskVersionEOIdRequiredSpec"]));
            AddRule(new ValidationRule<Version_EnablingObjective_Task>(new Version_EnablingObjective_Task_TaskIdRequiredSpec(), _validationStringLocalizer["Version_EnablingObjective_Task_TaskIdRequiredSpec"]));
        }
    }
}
