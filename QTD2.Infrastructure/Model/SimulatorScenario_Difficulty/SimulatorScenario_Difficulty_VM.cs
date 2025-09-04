using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario_Difficulty
{
    public class SimulatorScenario_Difficulty_VM
    {
        public int Id { get; set; }
        public string Difficulty { get; set; }

        public SimulatorScenario_Difficulty_VM(int id, string difficulty)
        {
            Id = id;
            Difficulty = difficulty;
        }

      public SimulatorScenario_Difficulty_VM() { }
    }
}
