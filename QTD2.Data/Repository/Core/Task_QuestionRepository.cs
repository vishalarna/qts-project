using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Task_QuestionRepository : Common.Repository<Task_Question>, ITask_QuestionRepository
    {
        public Task_QuestionRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
