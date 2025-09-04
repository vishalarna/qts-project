using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.TestItem_HistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class TestItem_HistoryValidation : Validation<TestItem_History>, ITestItem_HistoryValidation
    {
        public TestItem_HistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<TestItem_History>(new TestItem_HistoryTestItemIdRequiredSpec(), _validationStringLocalizer["TestItem_HistoryTestItemIdRequiredSpec"]));
        }
    }
}
