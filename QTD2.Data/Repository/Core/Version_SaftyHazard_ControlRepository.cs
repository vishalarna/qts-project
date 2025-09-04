using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_SaftyHazard_ControlRepository : Common.Repository<Version_SaftyHazard_Control>, IVersion_SaftyHazard_ControlRepository
    {
        public Version_SaftyHazard_ControlRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
