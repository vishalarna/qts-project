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

namespace QTD2.Domain.Services.Core
{
    public class SafetyHazard_CategoryHistoryService : Common.Service<SafetyHazard_CategoryHistory>, ISafetyHazard_CategoryHistoryService
    {
        public SafetyHazard_CategoryHistoryService(ISafetyHazard_CategoryHistoryRepository repository, ISafetyHazard_CategoryHistoryValidation validation)
            : base(repository, validation)
        {
        }
    }
}
