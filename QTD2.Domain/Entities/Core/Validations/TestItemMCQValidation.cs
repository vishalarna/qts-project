using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TestItemMCQSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TestItemMCQValidation : Validation<TestItemMCQ>, ITestItemMCQValidation
    {
        public TestItemMCQValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TestItemMCQ>(new TestItemMCQ_ChoiceDescRequiredSpec(), _validationStringLocalizer["TestItemMCQ_ChoiceDescRequired"]));
        }
    }
}
