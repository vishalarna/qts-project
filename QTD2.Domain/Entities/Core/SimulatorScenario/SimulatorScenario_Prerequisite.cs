using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Prerequisite: Entity
    {
        public int SimulatorScenarioId { get; set; }
        public int PrerequisiteId { get; set; }
        public SimulatorScenario SimulatorScenario { get; set; }
        public ILA Prerequisite { get; set; }

        public SimulatorScenario_Prerequisite()
        {
        }

        public SimulatorScenario_Prerequisite(SimulatorScenario simulatorScenario, ILA prerequisite)
        {
            SimulatorScenario = simulatorScenario;
            Prerequisite = prerequisite;
            SimulatorScenarioId = simulatorScenario.Id;
            PrerequisiteId = prerequisite.Id;
        }
    }
}
