using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_Tool_OperationService : Common.Service<ActionItem_Tool_Operation>,
                          IActionItem_Tool_OperationService
    {
        public ActionItem_Tool_OperationService(IActionItem_Tool_OperationRepository repository, IActionItem_Tool_OperationValidation
            validation)
            : base(repository, validation)
        {
        }
    }
}