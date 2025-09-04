using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class DIFSurvey_Employee_StatusService : Common.Service<DIFSurvey_Employee_Status>, IDIFSurvey_Employee_StatusService
    {
        public DIFSurvey_Employee_StatusService(IDIFSurvey_Employee_StatusRepository repository, IDIFSurvey_Employee_StatusValidation validation)
            : base(repository, validation)
        {
        }
    }
}
