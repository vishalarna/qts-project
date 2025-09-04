using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class ActionItem_OperationTypesValidation : Validation<ActionItem_OperationTypes>, IActionItem_OperationTypesValidation
    {
        public ActionItem_OperationTypesValidation(IStringLocalizerFactory stringLocalizerFactory)
           : base(stringLocalizerFactory)
        {
         
        }
    }
}