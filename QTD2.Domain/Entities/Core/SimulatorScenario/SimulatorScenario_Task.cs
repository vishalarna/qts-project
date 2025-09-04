using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Task : Common.Entity
    {
        public int SimulatorScenarioId { get; set; }
        public int TaskId { get; set; }
        public SimulatorScenario SimulatorScenario { get; set; }
        public Task Task { get; set; }

        public SimulatorScenario_Task(SimulatorScenario simulatorScenario, Task task)
        {
            SimulatorScenario = simulatorScenario;
            Task = task;
            SimulatorScenarioId = simulatorScenario.Id;
            TaskId = task.Id;
        }

        public SimulatorScenario_Task() { }
    }
}
