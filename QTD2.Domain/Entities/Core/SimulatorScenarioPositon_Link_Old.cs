using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenarioPositon_Link_Old : Entity
    {
        public int SimulatorScenarioID { get; set; }

        public int PositionID { get; set; }

        public virtual SimulatorScenario_Old SimulatorScenario { get; set; }

        public virtual Position Position { get; set; }

        public SimulatorScenarioPositon_Link_Old()
        {
        }

        public SimulatorScenarioPositon_Link_Old(SimulatorScenario_Old simulatorScenario, Position position)
        {
            SimulatorScenario = simulatorScenario;
            Position = position;
            SimulatorScenarioID = simulatorScenario.Id;
            PositionID = position.Id;
        }
    }
}
