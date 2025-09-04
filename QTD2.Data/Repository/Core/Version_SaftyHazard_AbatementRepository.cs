using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_SaftyHazard_AbatementRepository : Common.Repository<Version_SaftyHazard_Abatement>, IVersion_SaftyHazard_AbatementRepository
    {
        public Version_SaftyHazard_AbatementRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
