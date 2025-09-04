using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TestStatusSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TestStatusValidation : Validation<TestStatus>, ITestStatusValidation
    {
        public TestStatusValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TestStatus>(new TestStatusDescRequiredSpec(), _validationStringLocalizer["TestStatusDescRequiredSpec"]));
        }
    }
}
