using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Test_HistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Test_HistoryValidation : Validation<Test_History>, ITest_HistoryValidation
    {
        public Test_HistoryValidation(IStringLocalizerFactory stringLocalizerFactory) : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Test_History>(new Test_HistoryTestIdRequiredSpec(), _validationStringLocalizer["Test_HistoryTestIdRequiredSpec"]));
        }
    }
}
