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
  public  class SimulatorScenario_PositionService : Common.Service<SimulatorScenario_Position>, ISimulatorScenario_PositionService
    {
        public SimulatorScenario_PositionService(ISimulatorScenario_PositionRepository repository, ISimulatorScenario_PositonValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<SimulatorScenario_Position>> GetSimulatorScenarioBySimulatorIdAsync(int simulatorScenarioId)
        {
            var simulatorScenario_Position = await FindWithIncludeAsync(x => x.SimulatorScenarioID == simulatorScenarioId, new string[] { "Position" });
            return simulatorScenario_Position.ToList();
        }

    }
}
