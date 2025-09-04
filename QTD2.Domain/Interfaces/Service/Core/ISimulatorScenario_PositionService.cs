using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISimulatorScenario_PositionService : Common.IService<SimulatorScenario_Position>
    {
        public Task<List<SimulatorScenario_Position>> GetSimulatorScenarioBySimulatorIdAsync(int simScenarioId);
    }
}
