using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_ILA_VM
    {
        public int Id { get; set; }
        public int ILAId { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }

        public SimulatorScenario_ILA_VM(int id, int ilaId, string number, string description)
        {
            Id = id;
            ILAId = ilaId;
            Number = number;
            Description = description;
        }

       public SimulatorScenario_ILA_VM() { }
    }
}
