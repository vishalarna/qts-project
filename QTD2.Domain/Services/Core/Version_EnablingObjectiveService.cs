using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_EnablingObjectiveService : Common.Service<Version_EnablingObjective>, IVersion_EnablingObjectiveService
    {
        public Version_EnablingObjectiveService(IVersion_EnablingObjectiveRepository repository, IVersion_EnablingObjectiveValidation validation)
            : base(repository, validation)
        {
        }
    }
}
