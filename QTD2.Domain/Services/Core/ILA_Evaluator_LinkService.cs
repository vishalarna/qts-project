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
    public class ILA_Evaluator_LinkService : Common.Service<ILA_Evaluator_Link>, IILA_Evaluator_LinkService
    {
        public ILA_Evaluator_LinkService(IILA_Evaluator_LinkRepository repository, IILA_Evaluator_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
