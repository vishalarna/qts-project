using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_Task_StepService : Common.Service<Version_Task_Step>, IVersion_Task_StepService
    {
        public Version_Task_StepService(IVersion_Task_StepRepository repository, IVersion_Task_StepValidation validation)
            : base(repository, validation)
        {
        }
    }
}
