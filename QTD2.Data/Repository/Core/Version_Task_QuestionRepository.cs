using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_Task_QuestionRepository : Common.Repository<Version_Task_Question>, IVersion_Task_QuestionRepository
    {
        public Version_Task_QuestionRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
