using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISimulatorScenario_ILAService : Common.IService<SimulatorScenario_ILA>
    {
        public Task<List<SimulatorScenario_ILA>> GetILABySimulatorIdAsync(int simScenarioId);
        public Task<List<SimulatorScenario_ILA>> GetSimulatorScenarioILAByILAIdAsync(int ilaId);
    }
}
