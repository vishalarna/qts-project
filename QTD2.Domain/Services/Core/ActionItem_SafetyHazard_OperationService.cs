using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_SafetyHazard_OperationService : Common.Service<ActionItem_SafetyHazard_Operation>,IActionItem_SafetyHazard_OperationService
    {
        public ActionItem_SafetyHazard_OperationService(IActionItem_SafetyHazard_OperationRepository repository, IActionItem_SafetyHazard_OperationValidation validation): base(repository, validation)
        {
        }
    }
}