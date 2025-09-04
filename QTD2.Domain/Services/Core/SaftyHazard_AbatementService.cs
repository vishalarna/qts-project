using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class SaftyHazard_AbatementService : Common.Service<Entities.Core.SaftyHazard_Abatement>, Interfaces.Service.Core.ISaftyHazard_AbatementService
    {
        public SaftyHazard_AbatementService(ISaftyHazard_AbatementRepository saftyHazard_AbatementRepository, ISaftyHazard_AbatementValidation saftyHazard_AbatementValidation)
            : base(saftyHazard_AbatementRepository, saftyHazard_AbatementValidation)
        {
        }
    }
}
