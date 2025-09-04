using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_EnablingObjective : Entity
    {
        public int SimulatorScenarioID { get; set; }

        public int EnablingObjectiveID { get; set; }

        public virtual SimulatorScenario SimulatorScenario { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public SimulatorScenario_EnablingObjective()
        {
        }

        public SimulatorScenario_EnablingObjective(SimulatorScenario simulatorScenario, EnablingObjective enablingObjective)
        {
            SimulatorScenario = simulatorScenario;
            EnablingObjective = enablingObjective;
            SimulatorScenarioID = simulatorScenario.Id;
            EnablingObjectiveID = enablingObjective.Id;
        }
    }
}
