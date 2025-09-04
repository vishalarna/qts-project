using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_SaftyHazard_AbatementService : Common.Service<Version_SaftyHazard_Abatement>, IVersion_SaftyHazard_AbatementService
    {
        public Version_SaftyHazard_AbatementService(IVersion_SaftyHazard_AbatementRepository repository, IVersion_SaftyHazard_AbatementValidation validation)
            : base(repository, validation)
        {
        }
    }
}
