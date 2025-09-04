using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_SubDuty_OperationService : Common.Service<ActionItem_SubDuty_Operation>,
                    IActionItem_SubDuty_OperationService
    {
        public ActionItem_SubDuty_OperationService(IActionItem_SubDuty_OperationRepository repository, IActionItem_SubDuty_OperationValidation
            validation)
            : base(repository, validation)
        {
        }
    }
}