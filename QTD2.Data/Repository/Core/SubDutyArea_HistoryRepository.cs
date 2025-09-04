using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class SubDutyArea_HistoryRepository : Common.Repository<SubDutyArea_History>, ISubDutyArea_HistoryRepository
    {
        public SubDutyArea_HistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
