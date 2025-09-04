using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class EmpSettingsReleaseTypeService : Common.Service<EmpSettingsReleaseType>, IEmpSettingsReleaseTypeService
    {
        public EmpSettingsReleaseTypeService(IEmpSettingsReleaseTypeRepository repository, IEmpSettingsReleaseTypeValidation validation)
            : base(repository, validation)
        {
        }
    }

}
