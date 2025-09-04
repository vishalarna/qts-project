using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.CBT_ScormUpload_Question_ChoiceSpecs;
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
    public class CBT_ScormUpload_Question_ChoiceValidation : Validation<CBT_ScormUpload_Question_Choice>, ICBT_ScormUpload_Question_ChoiceValidation
    {
        public CBT_ScormUpload_Question_ChoiceValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<CBT_ScormUpload_Question_Choice>(new CBT_ScormUpload_Question_ChoiceCBTScormUploadQuestionIdRequiredSpec(), _validationStringLocalizer["CBT_ScormUpload_Question_ChoiceCBTScormUploadQuestionIdRequiredSpec"]));
            AddRule(new ValidationRule<CBT_ScormUpload_Question_Choice>(new CBT_ScormUpload_Question_ChoiceChoiceRequiredSpec(), _validationStringLocalizer["CBT_ScormUpload_Question_ChoiceChoiceRequiredSpec"]));
        }
    }
}
