using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class SaftyHazard_ControlService : Common.Service<Entities.Core.SaftyHazard_Control>, Interfaces.Service.Core.ISaftyHazard_ControlService
    {
        public SaftyHazard_ControlService(ISaftyHazard_ControlRepository saftyHazard_ControlRepository, ISaftyHazard_ControlValidation saftyHazard_ControlValidation)
            : base(saftyHazard_ControlRepository, saftyHazard_ControlValidation)
        {
        }
    }
}
