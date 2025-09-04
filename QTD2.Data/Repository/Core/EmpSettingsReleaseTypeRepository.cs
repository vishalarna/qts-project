using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class EmpSettingsReleaseTypeRepository : Common.Repository<EmpSettingsReleaseType>, IEmpSettingsReleaseTypeRepository
    {
        public EmpSettingsReleaseTypeRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
