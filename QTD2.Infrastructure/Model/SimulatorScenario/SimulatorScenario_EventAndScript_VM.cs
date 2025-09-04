using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_EventAndScript_VM
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int InitiatorId { get; set; }
        public List<SimulatorScenario_EventAndScript_Criteria_VM> Criterias { get; set; } = new List<SimulatorScenario_EventAndScript_Criteria_VM>();
        public DateTime? Time { get; set; }
        public SimulatorScenario_EventAndScript_VM(int id, int order, string title, string description, int initiatorId, DateTime? time)
        {
            Id = id;
            Title = title;
            Order = order;
            Description = description;
            InitiatorId = initiatorId;
            Time = time;
        }

       public SimulatorScenario_EventAndScript_VM() { }
    }
}
