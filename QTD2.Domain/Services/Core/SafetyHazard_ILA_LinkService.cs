using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class SafetyHazard_ILA_LinkService : Common.Service<SafetyHazard_ILA_Link>, ISafetyHazard_ILA_LinkService
    {
        public SafetyHazard_ILA_LinkService(ISafetyHazard_ILA_LinkRepository safetyHazard_ILA_LinkRepository, ISafetyHazard_ILA_LinkValidation safetyHazard_ILA_LinkValidation)
            : base(safetyHazard_ILA_LinkRepository, safetyHazard_ILA_LinkValidation)
        {
        }
    }
}
