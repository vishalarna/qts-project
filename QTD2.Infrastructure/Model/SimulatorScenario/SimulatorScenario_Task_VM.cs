using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_Task_VM
    {
        public int? Id { get; set; }
        public int TaskId { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string? Type { get; set; }
        public SimulatorScenario_Task_VM(int id, int taskId, string number, string description, string? type)
        {
            Id = id;
            TaskId = taskId;
            Number = number;
            Description = description;
            Type = type;
        }
        public SimulatorScenario_Task_VM() { }
    }
}
