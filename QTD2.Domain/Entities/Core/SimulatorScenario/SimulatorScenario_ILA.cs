using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_ILA : Entity
    {
        public int SimulatorScenarioID { get; set; }

        public int ILAID { get; set; }

        public virtual SimulatorScenario SimulatorScenario { get; set; }

        public virtual ILA ILA { get; set; }

        public SimulatorScenario_ILA()
        {
        }

        public SimulatorScenario_ILA(ILA ila, SimulatorScenario simulatorScenario)
        {
            SimulatorScenario = simulatorScenario;
            ILA = ila;
            SimulatorScenarioID = simulatorScenario.Id;
            ILAID = ila.Id;
        }
    }
}
