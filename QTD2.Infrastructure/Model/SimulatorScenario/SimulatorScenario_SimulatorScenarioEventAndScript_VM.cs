using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_SimulatorScenarioEventAndScript_VM
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public SimulatorScenario_SimulatorScenarioEventAndScript_VM(int id, int order, string title, string description)
        {
            Id = id;
            Order = order;
            Title = title;
            Description = description;
        }
        public SimulatorScenario_SimulatorScenarioEventAndScript_VM() { }
    }
}
