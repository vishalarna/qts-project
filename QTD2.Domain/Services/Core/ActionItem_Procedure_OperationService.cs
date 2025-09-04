using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_Procedure_OperationService : Common.Service<ActionItem_Procedure_Operation>,
                             IActionItem_Procedure_OperationService
    {
        public ActionItem_Procedure_OperationService(IActionItem_Procedure_OperationRepository repository, IActionItem_Procedure_OperationValidation
            validation)
            : base(repository, validation)
        {
        }
    }
}