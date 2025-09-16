using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISimulatorScenario_ScriptService : Common.IService<SimulatorScenario_Script>
    {
        public System.Threading.Tasks.Task<List<SimulatorScenario_Script>> GetScriptsByEventIdAsync(int eventId);
    }
}
