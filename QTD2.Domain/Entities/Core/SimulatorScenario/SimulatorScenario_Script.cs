using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Script : Entity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int InitiatorId { get; set; }
        public DateTime? Time { get; set; }
        public int EventId { get; set; }
        public virtual Position Initiator { get; set; }
        public virtual SimulatorScenario_Event  SimulatorScenario_Event { get; set; }
        public virtual List<SimulatorScenario_Script_Criteria> Criterias { get; set; } = new List<SimulatorScenario_Script_Criteria>();

        public SimulatorScenario_Script()
        {

        }

        public SimulatorScenario_Script(string title, string description, int initiatorId, DateTime? time, int eventId)
        {
            Title = title;
            Description = description;
            InitiatorId = initiatorId;
            Time = time;
            EventId = eventId;
        }

        public void SetCriterias(SimulatorScenario_Script_Criteria criteria)
        {
            criteria.ScriptId = Id;
            Criterias.Add(criteria);
        }

        public void SetTitle(string title)
        {
            Title = title;
        }
        public void SetDescription(string description)
        {
            Description = description;
        }
        public void SetTime(DateTime? time)
        {
            Time = time;

        }
        public void SetInitiatorId(int initiatorId)
        {
            InitiatorId = initiatorId;
        }
        public void SetEventId(int eventId)
        {
            EventId = eventId;
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as SimulatorScenario_Script;
            foreach (var criteria in Criterias)
            {
                var simScenario_Creteria = criteria.Copy<SimulatorScenario_Script_Criteria>(createdBy);
                simScenario_Creteria.Id = 0;
                copy.Criterias.Add(simScenario_Creteria);
            }
            copy.Title = "Copy - " + this.Title;
            return (T)(object)copy;
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnSimulatorScenario_Script_Deleted(this));
        }
    }
}