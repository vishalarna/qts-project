using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Services.Common;

namespace QTD2.Domain.Services.Core
{
    public class Procedure_StatusHistoryService : Service<Procedure_StatusHistory>, IProcedure_StatusHistoryService
    {
        public Procedure_StatusHistoryService(IProcedure_StatusHistoryRepository repository, IProcedure_StatusHistoryValidation validation)
            : base(repository, validation)
        {
        }
    }
}
