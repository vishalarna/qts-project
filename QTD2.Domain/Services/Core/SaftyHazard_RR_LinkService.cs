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
    public class SaftyHazard_RR_LinkService : Common.Service<SaftyHazard_RR_Link>, ISaftyHazard_RR_LinkService
    {
        public SaftyHazard_RR_LinkService(ISaftyHazard_RR_LinkRepository repository, ISaftyHazard_RR_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
