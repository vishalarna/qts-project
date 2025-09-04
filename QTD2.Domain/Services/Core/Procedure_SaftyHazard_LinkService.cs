using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Procedure_SaftyHazard_LinkService : Common.Service<Entities.Core.Procedure_SaftyHazard_Link>, Interfaces.Service.Core.IProcedure_SaftyHazard_LinkService
    {
        public Procedure_SaftyHazard_LinkService(IProcedure_SaftyHazard_LinkRepository procedure_SaftyHazard_LinkRepository, IProcedure_SaftyHazard_LinkValidation procedure_SaftyHazard_LinkValidation)
            : base(procedure_SaftyHazard_LinkRepository, procedure_SaftyHazard_LinkValidation)
        {
        }
    }
}
