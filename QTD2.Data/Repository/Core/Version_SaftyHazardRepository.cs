using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_SaftyHazardRepository : Common.Repository<Version_SaftyHazard>, IVersion_SaftyHazardRepository
    {
        public Version_SaftyHazardRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
