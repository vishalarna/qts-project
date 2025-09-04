using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Procedure_EnablingObjective_LinkService : Common.Service<Entities.Core.Procedure_EnablingObjective_Link>, Interfaces.Service.Core.IProcedure_EnablingObjective_LinkService
    {
        public Procedure_EnablingObjective_LinkService(IProcedure_EnablingObjective_LinkRepository procedure_EnablingObjective_LinkRepository, IProcedure_EnablingObjective_LinkValidation procedure_EnablingObjective_LinkValidation)
            : base(procedure_EnablingObjective_LinkRepository, procedure_EnablingObjective_LinkValidation)
        {
        }
    }
}
