using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class DIFSurvey_Employee_ResponseService : Common.Service<DIFSurvey_Employee_Response>, IDIFSurvey_Employee_ResponseService
    {
        public DIFSurvey_Employee_ResponseService(IDIFSurvey_Employee_ResponseRepository repository, IDIFSurvey_Employee_ResponseValidation validation)
            : base(repository, validation)
        {
        }
    }
}
