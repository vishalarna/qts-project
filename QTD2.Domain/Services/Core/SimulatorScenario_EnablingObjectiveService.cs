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
    public class SimulatorScenario_EnablingObjectiveService : Common.Service<SimulatorScenario_EnablingObjective>, ISimulatorScenario_EnablingObjectiveService
    {
        public SimulatorScenario_EnablingObjectiveService(ISimulatorScenario_EnablingObjectiveRepository repository, ISimulatorScenario_EnablingObjectiveValidation validation)
            : base(repository, validation)
        {
        }
        public async Task<List<SimulatorScenario_EnablingObjective>> GetEnablingObjectiveBySimulatorIdAsync(int simulatorScenarioId)
        {
            var simulatorScenario_EnablingObjective = await FindWithIncludeAsync(x => x.SimulatorScenarioID == simulatorScenarioId, new string[] { "EnablingObjective.EnablingObjective_Category", "EnablingObjective.EnablingObjective_SubCategory", "EnablingObjective.EnablingObjective_Topic" });
            return simulatorScenario_EnablingObjective.ToList();
        }

    }
}
