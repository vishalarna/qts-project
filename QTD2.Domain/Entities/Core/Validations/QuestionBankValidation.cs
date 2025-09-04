using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.QuestionBankSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class QuestionBankValidation : Validation<QuestionBank>, IQuestionBankValidation
    {
        public QuestionBankValidation(IStringLocalizerFactory localizer)
            : base(localizer)
        {
            AddRule(new ValidationRule<QuestionBank>(new QuestionBankStemRequiredSpec(), _validationStringLocalizer["QuestionbankStemRequiredSpec"]));
            
        }
    }
}