using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario_Status
{
    public class SimulatorScenario_Status_VM
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public SimulatorScenario_Status_VM(int id, string status)
        {
            Id = id;
            Status = status;
        }

        public SimulatorScenario_Status_VM() { }
    }
}
