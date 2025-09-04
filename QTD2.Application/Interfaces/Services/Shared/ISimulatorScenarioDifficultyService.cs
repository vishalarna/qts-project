using QTD2.Infrastructure.Model.SimulatorScenario_Difficulty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISimulatorScenarioDifficultyService
    {
        public Task<List<SimulatorScenario_Difficulty_VM>> GetAllAsync();
    }
}
