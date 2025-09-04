using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_Suggestion_OperationService : Common.Service<ActionItem_Suggestion_Operation>,
                         IActionItem_Suggestion_OperationService
    {
        public ActionItem_Suggestion_OperationService(IActionItem_Suggestion_OperationRepository repository, IActionItem_Suggestion_Operationvalidation
            validation)
            : base(repository, validation)
        {
        }
    }
}