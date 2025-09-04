using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class EmployeeDocumentService : Common.Service<EmployeeDocument>, IEmployeeDocumentService
    {
        public EmployeeDocumentService(IEmployeeDocumentRepository repository, IEmployeeDocumentValidation validation)
            : base(repository, validation)
        {
        }
    }
}
