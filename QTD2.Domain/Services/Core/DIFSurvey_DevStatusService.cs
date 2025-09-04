using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class DIFSurvey_DevStatusService : Common.Service<DIFSurvey_DevStatus>, IDIFSurvey_DevStatusService
    {
        public DIFSurvey_DevStatusService(IDIFSurvey_DevStatusRepository repository, IDIFSurvey_DevStatusValidation validation)
            : base(repository, validation)
        {
        }
    }
}
