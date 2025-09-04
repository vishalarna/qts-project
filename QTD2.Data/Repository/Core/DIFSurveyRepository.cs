using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class DIFSurveyRepository : Common.Repository<DIFSurvey>, IDIFSurveyRepository
    {
        public DIFSurveyRepository(QTDContext context)
            : base(context)
        {
        }
    }
}