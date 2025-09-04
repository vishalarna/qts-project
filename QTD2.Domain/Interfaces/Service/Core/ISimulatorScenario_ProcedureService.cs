using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISimulatorScenario_ProcedureService : Common.IService<SimulatorScenario_Procedure>
    {
        public Task<List<SimulatorScenario_Procedure>> GetProcedureBySimulatorIdAsync(int simScenarioId);
    }
}
