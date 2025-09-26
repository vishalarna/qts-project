using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public  class SimulatorScenario_Script_VM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? InitiatorId { get; set; }
        public int EventId { get; set; }
        public bool InitiatorOther { get; set; }
        public bool InitiatorInstructor { get; set; }
        public List<SimulatorScenario_Script_Criteria_VM> Criterias { get; set; } = new List<SimulatorScenario_Script_Criteria_VM>();
        public DateTime? Time { get; set; }
        public SimulatorScenario_Script_VM(int id, string title, string description, int? initiatorId, DateTime? time, int eventId, bool initiatorOther, bool initiatorInstructor)
        {
            Id = id;
            Title = title;
            Description = description;
            InitiatorId = initiatorId;
            Time = time;
            EventId = eventId;
            InitiatorOther = initiatorOther;
            InitiatorInstructor = initiatorInstructor;
        }

        public SimulatorScenario_Script_VM() { }
    }
}
