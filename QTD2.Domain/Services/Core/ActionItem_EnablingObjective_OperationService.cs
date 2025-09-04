using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_EnablingObjective_OperationService : Common.Service<ActionItem_EnablingObjective_Operation>,
                            IActionItem_EnablingObjective_OperationService
    {
        public ActionItem_EnablingObjective_OperationService(IActionItem_EnablingObjective_OperationRepository repository, IActionItem_EnablingObjective_OperationValidation
            validation)
            : base(repository, validation)
        {
        }
    }
}