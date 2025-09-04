using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.QuestionBankHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class QuestionBankHistoryValidation : Validation<QuestionBankHistory>, IQuestionBankHistoryValidation
    {
        public QuestionBankHistoryValidation(IStringLocalizerFactory localizer)
            : base(localizer)
        {
            AddRule(new ValidationRule<QuestionBankHistory>(new QuestionBankHistoryQuestionBankIdRequiredSpec(), _validationStringLocalizer["QuestionbankIdRequiredSpec"]));

        }
    }
}
