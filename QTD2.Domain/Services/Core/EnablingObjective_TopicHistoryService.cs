using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class EnablingObjective_TopicHistoryService : Common.Service<EnablingObjective_TopicHistory>, IEnablingObjective_TopicHistoryService
    {
        public EnablingObjective_TopicHistoryService(IEnablingObjective_TopicHistoryRepository repository, IEnablingObjective_TopicHistoryValidation validation)
            : base(repository, validation)
        {
        }
    }
}
