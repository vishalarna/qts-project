using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TestItemFillBlankSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TestItemFillBlankValidation : Validation<TestItemFillBlank>, ITestItemFillBlankValidation
    {
        public TestItemFillBlankValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TestItemFillBlank>(new TestItemFillBlankCorrectIndexRequired(), _validationStringLocalizer["TestItemFillBlankCorrectIndexRequired"]));
            AddRule(new ValidationRule<TestItemFillBlank>(new TestItemFillBlankCorrectRequired(), _validationStringLocalizer["TestItemFillBlankCorrectRequired"]));
        }
    }
}
