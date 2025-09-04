using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DIFSurvey_TaskRepository : Common.Repository<DIFSurvey_Task>, IDIFSurvey_TaskRepository
    {
        public DIFSurvey_TaskRepository(QTDContext context)
            : base(context)
        {
        }
    }
}