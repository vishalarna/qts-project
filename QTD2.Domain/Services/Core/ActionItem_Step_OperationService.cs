using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_Step_OperationService : Common.Service<ActionItem_Step_Operation>,
                      IActionItem_Step_OperationService
    {
        public ActionItem_Step_OperationService(IActionItem_Step_OperationRepository repository, IActionItem_Step_OperationValidation
            validation)
            : base(repository, validation)
        {
        }
    }
}