using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Event : Entity
    {
        public int SimulatorScenarioId { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public virtual SimulatorScenario SimulatorScenario { get; set; }
        public virtual List<SimulatorScenario_Script> Scripts { get; set; } = new List<SimulatorScenario_Script>();

        public SimulatorScenario_Event()
        {

        }

        public SimulatorScenario_Event(int simulatorScenarioId, int order, string title, string description)
        {
            SimulatorScenarioId = simulatorScenarioId;
            Order = order;
            Title = title;
            Description = description;
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

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as SimulatorScenario_Event;

            copy.Title = "Copy - " + this.Title;
            return (T)(object)copy;
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnSimulatorScenario_Event_Deleted(this));
        }
    }
}
