using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenarioDifficultyLevel_Old : Entity
    {
        public string SimulatorScenarioDiffLevel { get; set; }

        public virtual ICollection<SimulatorScenario_Old> SimulatorScenarios { get; set; } = new List<SimulatorScenario_Old>();

        public SimulatorScenarioDifficultyLevel_Old()
        {
        }

        public SimulatorScenarioDifficultyLevel_Old(string simulatorScenarioDiffLevel)
        {
            SimulatorScenarioDiffLevel = simulatorScenarioDiffLevel;
        }
    }
}
