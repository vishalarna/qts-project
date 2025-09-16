using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISimulatorScenario_EventService : Common.IService<SimulatorScenario_Event>
    {
        public Task<SimulatorScenario_Event> GetEventByIdAsync(int id);
        public Task<SimulatorScenario_Event> GetScriptByIdAsync(int id);
    }
}
