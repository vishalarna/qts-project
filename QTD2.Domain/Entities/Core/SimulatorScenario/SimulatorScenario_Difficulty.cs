using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Difficulty : Entity
    {
        public string Difficulty { get; set; }

        public SimulatorScenario_Difficulty(string difficulty)
        {
            Difficulty = difficulty;
        }
        public SimulatorScenario_Difficulty() { }
    }
}
