using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TimesheetRepository : Common.Repository<Timesheet>, ITimesheetRepository
    {
        public TimesheetRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
