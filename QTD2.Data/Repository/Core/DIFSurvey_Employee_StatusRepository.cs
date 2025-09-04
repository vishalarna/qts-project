using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DIFSurvey_Employee_StatusRepository : Common.Repository<DIFSurvey_Employee_Status>, IDIFSurvey_Employee_StatusRepository
    {
        public DIFSurvey_Employee_StatusRepository(QTDContext context)
            : base(context)
        {
        }
    }
}