using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class DIFSurvey_Task_StatusService : Common.Service<DIFSurvey_Task_Status>, IDIFSurvey_Task_StatusService
    {
        public DIFSurvey_Task_StatusService(IDIFSurvey_Task_StatusRepository repository, IDIFSurvey_Task_StatusValidation validation)
            : base(repository, validation)
        {
        }
    }
}