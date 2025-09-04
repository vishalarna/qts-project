using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ToolCategory_StatusHistoryValidation : Validation<ToolCategory_StatusHistory>, IToolCategory_StatusHistoryValidation
    {
        public ToolCategory_StatusHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            //AddRule(new ValidationRule<ToolCategory_StatusHistory>(new ToolCategory_StatusHistorySpec(), _validationStringLocalizer["RS_DescriptionRequired"]));
        }
    }

}
