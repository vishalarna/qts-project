using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Test_Item_LinkSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Test_Item_LinkValidation : Validation<Test_Item_Link>, ITest_Item_LinkValidation
    {
        public Test_Item_LinkValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Test_Item_Link>(new Test_Item_LinkTestIdRequiredSpec(), _validationStringLocalizer["Test_Item_LinkTestIdRequiredSpec"]));
            AddRule(new ValidationRule<Test_Item_Link>(new Test_Item_LinkTestItemIdRequiredSpec(), _validationStringLocalizer["Test_Item_LinkTestItemIdRequiredSpec"]));
        }
    }
}
