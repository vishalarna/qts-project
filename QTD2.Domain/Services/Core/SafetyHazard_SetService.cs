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
    public class SafetyHazard_SetService : Common.Service<SafetyHazard_Set>, ISafetyHazard_SetService
    {
        public SafetyHazard_SetService(ISafetyHazard_SetRepository repository, ISafetyHazard_SetValidation validation)
            : base(repository, validation)
        {
        }
    }
}
