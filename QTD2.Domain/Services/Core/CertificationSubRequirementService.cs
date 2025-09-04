using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class CertificationSubRequirementService : Common.Service<CertificationSubRequirement>, ICertificationSubRequirementService
    {
        public CertificationSubRequirementService(ICertificationSubRequirementRepository repository, ICertificationSubRequirementValidation validation)
            : base(repository, validation)
        {
        }
    }
}
