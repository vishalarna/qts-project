using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_Task_SaftyHazard_LinkService : Common.Service<Version_Task_SaftyHazard_Link>, IVersion_Task_SaftyHazard_LinkService
    {
        public Version_Task_SaftyHazard_LinkService(IVersion_Task_SaftyHazard_LinkRepository repository, IVersion_Task_SaftyHazard_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
