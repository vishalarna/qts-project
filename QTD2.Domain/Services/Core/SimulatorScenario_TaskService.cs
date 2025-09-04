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
  public  class SimulatorScenario_TaskService : Common.Service<SimulatorScenario_Task>, ISimulatorScenario_TaskService
    {
        public SimulatorScenario_TaskService(ISimulatorScenario_TaskRepository repository, ISimulatorScenario_TaskValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<SimulatorScenario_Task>> GetTasksBySimulatorIdAsync(int simulatorScenarioId)
        {
            var simulatorScenario_Task = await FindWithIncludeAsync(x => x.SimulatorScenarioId == simulatorScenarioId, new string[] { "Task.SubdutyArea.DutyArea" });
            return simulatorScenario_Task.ToList();
        }

    }
}
