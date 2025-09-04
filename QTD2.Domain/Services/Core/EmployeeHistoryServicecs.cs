using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class EmployeeHistoryServicecs : Common.Service<EmployeeHistory>, IEmployeeHistoryService
    {
        public EmployeeHistoryServicecs(IEmployeeHistoryRepository repository, IEmployeeHistoryValidation validation)
            : base(repository, validation)
        {
        }
    }
}
