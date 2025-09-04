using System;
using System.Collections.Generic;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_EventAndScript : Entity
    {
        public int SimulatorScenarioId { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int InitiatorId { get; set; }
        public DateTime? Time { get; set; }
        public virtual Position Initiator { get; set; }
        public virtual SimulatorScenario SimulatorScenario { get; set; }
        public virtual List<SimulatorScenario_EventAndScript_Criteria> Criterias { get; set; } = new List<SimulatorScenario_EventAndScript_Criteria>();

        public SimulatorScenario_EventAndScript()
        {

        }

        public SimulatorScenario_EventAndScript(int simulatorScenarioId, int order, string title, string description, int initiatorId, DateTime? time)
        {
            SimulatorScenarioId = simulatorScenarioId;
            Order = order;
            Title = title;
            Description = description;
            InitiatorId = initiatorId;
            Time = time;
        }

        public void SetCriterias(SimulatorScenario_EventAndScript_Criteria criteria)
        {
            criteria.EventAndScriptId = Id;
            Criterias.Add(criteria);
        }

        public void SetOrder(int order)
        {
            Order = order;
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

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as SimulatorScenario_EventAndScript;

            copy.Title = "Copy - " + this.Title;

            foreach (var criteria in Criterias)
            {
                var simScenario_Creteria = criteria.Copy<SimulatorScenario_EventAndScript_Criteria>(createdBy);
                simScenario_Creteria.Id = 0;
                copy.Criterias.Add(simScenario_Creteria);
            }
            return (T)(object)copy;
        }
    }
}
