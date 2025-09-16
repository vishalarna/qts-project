using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class SimulatorScenario_ScriptService : Common.Service<SimulatorScenario_Script>, ISimulatorScenario_ScriptService
    {
        public SimulatorScenario_ScriptService(ISimulatorScenario_ScriptRepository repository, ISimulatorScenario_ScriptValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<SimulatorScenario_Script>> GetScriptsByEventIdAsync(int eventId)
        {
            var simulatorScenario_Scripts = await FindAsync(r => r.EventId == eventId);
            return simulatorScenario_Scripts.ToList();
        }
    }
}