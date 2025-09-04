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
    public class ILA_RegRequirement_LinkService : Common.Service<ILA_RegRequirement_Link>, IILA_RegRequirement_LinkService
    {
        public ILA_RegRequirement_LinkService(IILA_RegRequirement_LinkRepository repository, IILA_RegRequirement_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
