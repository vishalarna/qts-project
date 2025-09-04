using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CBT_ScormRegistration_ResponseSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class CBT_ScormRegistration_ResponseValidation : Validation<CBT_ScormRegistration_Response>, ICBT_ScormRegistration_ResponseValidation
    {
        public CBT_ScormRegistration_ResponseValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<CBT_ScormRegistration_Response>(new CBT_ScormRegistration_ResponseCBTScormRegistrationIdRequiredSpec(), _validationStringLocalizer["CBT_ScormRegistration_ResponseCBTScormRegistrationIdRequiredSpec"]));
            AddRule(new ValidationRule<CBT_ScormRegistration_Response>(new CBT_ScormUpload_Question_CBTScormUploadQuestionResponseIdRequiredSpec(), _validationStringLocalizer["CBT_ScormUpload_Question_CBTScormUploadQuestionResponseIdRequiredSpec"]));

        }
    }
}
