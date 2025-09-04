using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Task_Criteria: Entity
    {
        public int SimulatorScenarioId { get; set; }
        public int TaskId { get; set; }
        public string? Criteria { get; set; }
        public SimulatorScenario SimulatorScenario { get; set; }
        public Task Task { get; set; }

        public SimulatorScenario_Task_Criteria()
        {

        }
        public SimulatorScenario_Task_Criteria(int simulatorScenarioId, int taskId, string criteria)
        {
            SimulatorScenarioId = simulatorScenarioId;
            TaskId = taskId;
            Criteria = criteria;
        }

        public void UpdateCriteria(string? criteria)
        {
            Criteria = criteria;
        }

        public override void Delete()
        {
            AddDomainEvent(new Domain.Events.Core.OnSimulatorScenario_Task_Criteria_Deleted(this));
            base.Delete();
        }

    }
}
