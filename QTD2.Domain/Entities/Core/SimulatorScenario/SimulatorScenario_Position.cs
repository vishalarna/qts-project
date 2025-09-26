using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Position : Entity
    {
        public int SimulatorScenarioID { get; set; }

        public int PositionID { get; set; }

        public virtual SimulatorScenario SimulatorScenario { get; set; }

        public virtual Position Position { get; set; }
        public virtual ICollection<SimulatorScenario_Script> SimulatorScenario_Scripts { get; set; } = new List<SimulatorScenario_Script>();

        public SimulatorScenario_Position()
        {
        }

        public SimulatorScenario_Position(SimulatorScenario simulatorScenario, Position position)
        {
            SimulatorScenario = simulatorScenario;
            Position = position;
            SimulatorScenarioID = simulatorScenario.Id;
            PositionID = position.Id;
        }
    }
}
