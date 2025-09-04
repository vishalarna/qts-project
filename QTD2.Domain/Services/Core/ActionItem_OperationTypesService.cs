using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
  public  class ActionItem_OperationTypesService : Common.Service<ActionItem_OperationTypes>,
                            IActionItem_OperationTypesService
    {
        public ActionItem_OperationTypesService(IActionItem_OperationTypesRepository repository, IActionItem_OperationTypesValidation
            validation)
            : base(repository, validation)
        {
        }
    }
}