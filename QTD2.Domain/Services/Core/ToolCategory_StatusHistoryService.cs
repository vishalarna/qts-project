using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ToolCategory_StatusHistoryService : Common.Service<ToolCategory_StatusHistory>, IToolCategory_StatusHistoryService
    {
        public ToolCategory_StatusHistoryService(IToolCategory_StatusHistoryRepository repository, IToolCategory_StatusHistoryValidation validation)
            : base(repository, validation)
        {
        }
    }
}
