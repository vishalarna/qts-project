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
    public class ILA_Procedure_LinkService : Common.Service<ILA_Procedure_Link>, IILA_Procedure_LinkService
    {
        public ILA_Procedure_LinkService(IILA_Procedure_LinkRepository repository, IILA_Procedure_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
