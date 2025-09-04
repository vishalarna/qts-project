using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Task_QuestionService : Common.Service<Task_Question>, ITask_QuestionService
    {
        public Task_QuestionService(ITask_QuestionRepository repository, ITask_QuestionValidation validation)
            : base(repository, validation)
        {
        }
    }
}
