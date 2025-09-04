using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Version_ProcedureService : Common.Service<Version_Procedure>, IVersion_ProcedureService
    {
        public Version_ProcedureService(IVersion_ProcedureRepository repository, IVersion_ProcedureValidation validation)
            : base(repository, validation)
        {
        }
    }
}
