using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISimulatorScenarioService : Common.IService<SimulatorScenario>
    {
        System.Threading.Tasks.Task<SimulatorScenario> GetForCopy(int id);
        System.Threading.Tasks.Task<SimulatorScenario> GetSimulatorScenarioWithEventsAndScriptsAsync(int id);
        Task<SimulatorScenario> GetAsync(int id);
    }
}
