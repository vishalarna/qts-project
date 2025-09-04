using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenarioILA_Link_Old : Entity
    {
        public int SimulatorScenarioID { get; set; }

        public int ILAID { get; set; }

        public virtual SimulatorScenario_Old SimulatorScenario { get; set; }

        public virtual ILA ILA { get; set; }

        public SimulatorScenarioILA_Link_Old()
        {
        }

        public SimulatorScenarioILA_Link_Old(ILA ila, SimulatorScenario_Old simulatorScenario)
        {
            SimulatorScenario = simulatorScenario;
            ILA = ila;
            SimulatorScenarioID = simulatorScenario.Id;
            ILAID = ila.Id;
        }
    }
}
