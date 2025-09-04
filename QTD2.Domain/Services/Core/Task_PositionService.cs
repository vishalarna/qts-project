using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Task_PositionService : Common.Service<Task_Position>, ITask_PositionService
    {
        public Task_PositionService(ITask_PositionRepository repository, ITask_PositionValidation validation)
            : base(repository, validation)
        {
        }
    }
}
