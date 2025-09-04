using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ActionItem_QuestionAndAnswer_OperationService : Common.Service<ActionItem_QuestionAndAnswer_Operation>,
                       IActionItem_QuestionAndAnswer_OperationService
    {
        public ActionItem_QuestionAndAnswer_OperationService(IActionItem_QuestionAndAnswer_OperationRepository repository, IActionItem_QuestionAndAnswer_OperationValidation
            validation)
            : base(repository, validation)
        {
        }
    }
}