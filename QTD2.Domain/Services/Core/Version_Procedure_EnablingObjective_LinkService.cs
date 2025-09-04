using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_Procedure_EnablingObjective_LinkService : Common.Service<Version_Procedure_EnablingObjective_Link>, IVersion_Procedure_EnablingObjective_LinkService
    {
        public Version_Procedure_EnablingObjective_LinkService(
            IVersion_Procedure_EnablingObjective_LinkRepository repository,
            IVersion_Procedure_EnablingObjective_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
