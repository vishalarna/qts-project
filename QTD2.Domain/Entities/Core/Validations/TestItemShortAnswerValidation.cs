using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TestItemShortAnswerSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TestItemShortAnswerValidation : Validation<TestItemShortAnswer>, ITestItemShortAnswerValidation
    {
        public TestItemShortAnswerValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TestItemShortAnswer>(new TestItemShortAnswerResponsesRequiredSpec(), _validationStringLocalizer["TestItemShortAnswerResponsesRequiredSpec"]));
            AddRule(new ValidationRule<TestItemShortAnswer>(new TItemShAnsAcceptableResponsesRequiredSpec(), _validationStringLocalizer["TItemShAnsAcceptableResponsesRequiredSpec"]));
            AddRule(new ValidationRule<TestItemShortAnswer>(new TestItemShAnsTestItemIdRequiredSpec(), _validationStringLocalizer["TestItemShAnsTestItemIdRequiredSpec"]));
        }
    }
}
