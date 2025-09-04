using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_OperationType_LinksService : Common.Service<ActionItem_OperationType_Links>,
                            IActionItem_OperationType_LinksService
    {
        public ActionItem_OperationType_LinksService(IActionItem_OperationType_LinksRepository repository, IActionItem_OperationType_LinksValidation
            validation)
            : base(repository, validation)
        {
        }
    }
}