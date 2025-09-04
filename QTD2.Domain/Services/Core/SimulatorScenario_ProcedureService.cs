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
    public class SimulatorScenario_ProcedureService : Common.Service<SimulatorScenario_Procedure>, ISimulatorScenario_ProcedureService
    {
        public SimulatorScenario_ProcedureService(ISimulatorScenario_ProcedureRepository repository, ISimulatorScenario_ProcedureValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<SimulatorScenario_Procedure>> GetProcedureBySimulatorIdAsync(int simulatorScenarioId)
        {
            var simulatorScenario_Procedure = await FindWithIncludeAsync(x => x.SimulatorScenarioId == simulatorScenarioId, new string[] { "Procedure" });
            return simulatorScenario_Procedure.ToList();
        }

    }
}
