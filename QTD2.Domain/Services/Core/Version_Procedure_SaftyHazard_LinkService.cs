using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_Procedure_SaftyHazard_LinkService : Common.Service<Version_Procedure_SaftyHazard_Link>, IVersion_Procedure_SaftyHazard_LinkService
    {
        public Version_Procedure_SaftyHazard_LinkService(IVersion_Procedure_SaftyHazard_LinkRepository repository, IVersion_Procedure_SaftyHazard_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
