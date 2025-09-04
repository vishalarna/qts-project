using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Procedure : Entity
    {
        public int SimulatorScenarioId { get; set; }
        public int ProcedureId { get; set; }
        public SimulatorScenario SimulatorScenario { get; set; }
        public Procedure Procedure { get; set; }
        public SimulatorScenario_Procedure()
        {
        }

        public SimulatorScenario_Procedure(SimulatorScenario simulatorScenario, Procedure procedure)
        {
            SimulatorScenario = simulatorScenario;
            Procedure = procedure;
            SimulatorScenarioId = simulatorScenario.Id;
            ProcedureId = procedure.Id;
        }

    }
}
