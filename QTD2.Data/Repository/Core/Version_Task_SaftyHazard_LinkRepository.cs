using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_Task_SaftyHazard_LinkRepository : Common.Repository<Version_Task_SaftyHazard_Link>, IVersion_Task_SaftyHazard_LinkRepository
    {
        public Version_Task_SaftyHazard_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
