using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulationScenarioSpecLookUp_Old :Entity
    {
        public string SimScenarioSpecHeading { get; set; }

        public SimulationScenarioSpecLookUp_Old(string smScenarioSpecHeading)
        {
            SimScenarioSpecHeading = smScenarioSpecHeading;
        }

        public SimulationScenarioSpecLookUp_Old()
        {
        }
    }
}
