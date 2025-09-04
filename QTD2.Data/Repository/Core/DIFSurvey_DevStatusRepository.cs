using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DIFSurvey_DevStatusRepository : Common.Repository<DIFSurvey_DevStatus>, IDIFSurvey_DevStatusRepository
    {
        public DIFSurvey_DevStatusRepository(QTDContext context)
            : base(context)
        {
        }
    }
}