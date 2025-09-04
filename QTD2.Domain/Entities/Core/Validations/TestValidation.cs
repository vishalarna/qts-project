using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TestSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TestValidation : Validation<Test>, ITestValidation
    {
        public TestValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Test>(new TestStatusIDRequiredSpec(), _validationStringLocalizer["TestStatusIDRequiredSpec"]));
            AddRule(new ValidationRule<Test>(new TestTitleRequiredSpec(), _validationStringLocalizer["TestTitleRequiredSpec"]));
        }
    }
}
