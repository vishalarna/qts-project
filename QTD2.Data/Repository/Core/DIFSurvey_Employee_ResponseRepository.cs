using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DIFSurvey_Employee_ResponseRepository : Common.Repository<DIFSurvey_Employee_Response>, IDIFSurvey_Employee_ResponseRepository
    {
        public DIFSurvey_Employee_ResponseRepository(QTDContext context)
            : base(context)
        {
        }
    }
}