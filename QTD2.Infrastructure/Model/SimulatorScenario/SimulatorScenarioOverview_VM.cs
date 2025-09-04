using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenarioOverview_VM
    {
        public List<SimulatorScenarioOverview_SimulatorScenario_VM> SimulatorScenarios { get; set; } = new List<SimulatorScenarioOverview_SimulatorScenario_VM>();
    }
}
