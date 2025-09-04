using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TestItemMatchSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TestItemMatchValidation : Validation<TestItemMatch>, ITestItemMatchValidation
    {
        public TestItemMatchValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TestItemMatch>(new TestItemMatch_ChoicDescRequiredSpec(), _validationStringLocalizer["TestItemMatch_ChoicDescRequired"]));
            AddRule(new ValidationRule<TestItemMatch>(new TestItemMatch_MatchDescRequiredSpec(), _validationStringLocalizer["TestItemMatch_MatchDescRequired"]));
            AddRule(new ValidationRule<TestItemMatch>(new TestItemMatch_MatchValueRequiredSpec(), _validationStringLocalizer["TestItemMatch_MatchValueRequired"]));
        }
    }
}
