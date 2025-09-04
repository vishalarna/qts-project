using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CBTScormRegistrationSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
  public  class CBT_ScormRegistrationValidation : Validation<CBT_ScormRegistration>, ICBT_ScormRegistrationValidation
    {
        public CBT_ScormRegistrationValidation(IStringLocalizerFactory stringLocalizerFactory)
          : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<CBT_ScormRegistration>(new CBTScormRegistration_EmployeeIdRequiredSpec(), _validationStringLocalizer["CBTScormRegistration_RegistrationIdRequiredSpec"]));
            //AddRule(new ValidationRule<CBT_ScormRegistration>(new CBTScormRegistration_LaunchLinkRequiredSpecs(), _validationStringLocalizer["CBTScormRegistration_LaunchLinkRequiredSpec"]));
        }
    }
}
