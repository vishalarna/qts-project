using QTD2.Infrastructure.Model.SimulatorScenario_Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISimulatorScenarioStatusService
    {
        public Task<List<SimulatorScenario_Status_VM>> GetAllAsync();
    }
}
