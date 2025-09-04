using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.Tool_StatusHistorySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class Tool_StatusHistoryValidation : Validation<Tool_StatusHistory>, ITool_StatusHistoryValidation
    {
        public Tool_StatusHistoryValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<Tool_StatusHistory>(new Tool_HistoryToolIdRequiredSpec(), _validationStringLocalizer["Tool_HistoryToolIdRequired"]));
            //AddRule(new ValidationRule<Tool_StatusHistory>(new Tool_HistoryNotesRequiredSpec(), _validationStringLocalizer["Tool_HistoryNotesRequired"]));
            AddRule(new ValidationRule<Tool_StatusHistory>(new Tool_HistoryEffectiveDateRequiredSpec(), _validationStringLocalizer["Tool_HistoryEffectiveDateRequired"]));
        }
    }
}
