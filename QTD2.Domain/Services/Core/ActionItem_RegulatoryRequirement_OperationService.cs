using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_RegulatoryRequirement_OperationService : Common.Service<ActionItem_RegulatoryRequirement_Operation>,
                              IActionItem_RegulatoryRequirement_OperationService
    {
        public ActionItem_RegulatoryRequirement_OperationService(IActionItem_RegulatoryRequirement_OperationRepository repository, IActionItem_RegulatoryRequirement_OperationValidation
            validation)
            : base(repository, validation)
        {
        }
    }
}