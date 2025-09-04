using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_EventsAndScriptsOrder_VM
    {
        public int EventAndScriptId { get; set; }
        public int Order { get; set; }

        public SimulatorScenario_EventsAndScriptsOrder_VM(int eventAndScriptId, int order)
        {
            EventAndScriptId = eventAndScriptId;
            Order = order;
        }

        public SimulatorScenario_EventsAndScriptsOrder_VM() { }
    }
}
