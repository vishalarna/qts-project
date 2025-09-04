using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_SaftyHazard_ControlService : Common.Service<Version_SaftyHazard_Control>, IVersion_SaftyHazard_ControlService
    {
        public Version_SaftyHazard_ControlService(IVersion_SaftyHazard_ControlRepository repository, IVersion_SaftyHazard_ControlValidation validation)
            : base(repository, validation)
        {
        }
    }
}
