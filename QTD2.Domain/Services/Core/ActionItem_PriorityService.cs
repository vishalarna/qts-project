using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_PriorityService : Common.Service<ActionItem_Priority>,
                   IActionItem_PriorityService
    {
        public ActionItem_PriorityService(IActionItem_PriorityRepository repository, IActionItem_PriorityValidation validation)
            : base(repository, validation)
        {
        }
    }
}