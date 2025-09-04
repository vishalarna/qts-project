using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenarioCreateOptions
    {
        public int SimScenarioDiffID { get; set; }

        public string SimScenarioTitle { get; set; }

        public string SimScenarioDesc { get; set; }

        public int SimScenarioDurationHours { get; set; }

        public int SimScenarioDurationMins { get; set; }

        public byte[] SimScenarioUpload { get; set; }
    }
}
