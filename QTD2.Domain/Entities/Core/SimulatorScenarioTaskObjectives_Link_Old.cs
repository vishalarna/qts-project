using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenarioTaskObjectives_Link_Old : Entity
    {
        public int SimulatorScenarioID { get; set; }

        public int TaskID { get; set; }

        public virtual SimulatorScenario_Old SimulatorScenario { get; set; }

        public virtual Task Task { get; set; }

        public SimulatorScenarioTaskObjectives_Link_Old()
        {
        }

        public SimulatorScenarioTaskObjectives_Link_Old(SimulatorScenario_Old simulatorScenario, Task task)
        {
            SimulatorScenario = simulatorScenario;
            Task = task;
            SimulatorScenarioID = simulatorScenario.Id;
            TaskID = task.Id;
        }
    }
}
