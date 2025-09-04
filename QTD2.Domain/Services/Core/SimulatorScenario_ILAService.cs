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
 public   class SimulatorScenario_ILAService : Common.Service<SimulatorScenario_ILA>, ISimulatorScenario_ILAService
    {
        public SimulatorScenario_ILAService(ISimulatorScenario_ILARepository repository, ISimulatorScenario_ILAValidation validation)
            : base(repository, validation)
        {
        }


        public async Task<List<SimulatorScenario_ILA>> GetILABySimulatorIdAsync(int simulatorScenarioId)
        {
            var simulatorScenario_ILa = await FindWithIncludeAsync(x => x.SimulatorScenarioID == simulatorScenarioId, new string[] { "ILA" });
            return simulatorScenario_ILa.ToList();
        }

    }
}
