using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Procedure_RR_LinkService : Common.Service<Entities.Core.Procedure_RR_Link>, Interfaces.Service.Core.IProcedure_RR_LinkService
    {
        public Procedure_RR_LinkService(IProcedure_RR_LinkRepository procedure_RegRequirement_LinkRepository, IProcedure_RR_LinkValidation procedure_RegRequirement_LinkValidation)
            : base(procedure_RegRequirement_LinkRepository, procedure_RegRequirement_LinkValidation)
        {
        }
    }
}
