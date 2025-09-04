using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class EnablingObjective_CategoryHistoryService : Common.Service<EnablingObjective_CategoryHistory>, IEnablingObjective_CategoryHistoryService
    {
        public EnablingObjective_CategoryHistoryService(IEnablingObjective_CategoryHistoryRepository repository, IEnablingObjective_CategoryHistoryValidation validation)
            : base(repository, validation)
        {
        }
    }
}
