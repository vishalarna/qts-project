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
  public  class SimulatorScenario_PrerequisiteService : Common.Service<SimulatorScenario_Prerequisite>, ISimulatorScenario_PrerequisiteService
    {
        public SimulatorScenario_PrerequisiteService(ISimulatorScenario_PrerequisiteRepository repository, ISimulatorScenario_PrerequisiteValidation validation)
            : base(repository, validation)
        {
        }
        public async Task<List<SimulatorScenario_Prerequisite>> GetPrerequisiteBySimulatorIdAsync(int simulatorScenarioId)
        {
            var simulatorScenario_Prerequisite = await FindWithIncludeAsync(x => x.SimulatorScenarioId == simulatorScenarioId, new string[] { "Prerequisite" });
            return simulatorScenario_Prerequisite.ToList();
        }

    }
}