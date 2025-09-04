using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DIFSurvey_Task_StatusRepository : Common.Repository<DIFSurvey_Task_Status>, IDIFSurvey_Task_StatusRepository
    {
        public DIFSurvey_Task_StatusRepository(QTDContext context)
            : base(context)
        {
        }
    }
}