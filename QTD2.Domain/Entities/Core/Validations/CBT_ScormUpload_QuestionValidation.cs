using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CBT_ScormRegistration_ResponseSpecs;
using QTD2.Domain.Entities.Core.Specifications.CBT_ScormUpload_QuestionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class CBT_ScormUpload_QuestionValidation : Validation<CBT_ScormUpload_Question>, ICBT_ScormUpload_QuestionValidation
    {
        public CBT_ScormUpload_QuestionValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<CBT_ScormUpload_Question>(new CBT_ScormUpload_QuestionCbtScormUploadIdRequiredSpec(), _validationStringLocalizer["CBT_ScormUpload_QuestionCbtScormUploadIdRequiredSpec"]));
            AddRule(new ValidationRule<CBT_ScormUpload_Question>(new CBT_ScormUpload_QuestionDescriptionRequiredSpec(), _validationStringLocalizer["CBT_ScormUpload_QuestionDescriptionRequiredSpec"]));

        }
    }
}
