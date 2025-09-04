using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_SaftyHazardService : Common.Service<Version_SaftyHazard>, IVersion_SaftyHazardService
    {
        public Version_SaftyHazardService(IVersion_SaftyHazardRepository repository, IVersion_SaftyHazardValidation validation)
            : base(repository, validation)
        {
        }
    }
}
