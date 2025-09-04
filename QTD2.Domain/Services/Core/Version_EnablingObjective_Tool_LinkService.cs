using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_EnablingObjective_Tool_LinkService : Common.Service<Version_EnablingObjective_Tool_Link>, IVersion_EnablingObjective_Tool_LinkService
    {
        public Version_EnablingObjective_Tool_LinkService(
            IVersion_EnablingObjective_Tool_LinkRepository repository,
            IVersion_EnablingObjective_Tool_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
