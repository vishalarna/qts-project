using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_TasksResponseVM
    {
        public List<SimulatorScenario_Task_VM> SimulatorScenarioTaskVMs { get; set; } = new List<SimulatorScenario_Task_VM>();
        public List<SimulatorScenario_Procedure_VM> SimulatorScenarioProcedureVMs { get; set; } = new List<SimulatorScenario_Procedure_VM>();
        public List<SimulatorScenario_EnablingObjective_VM> SimulatorScenarioEnablingObjectiveVMs { get; set; } = new List<SimulatorScenario_EnablingObjective_VM>();
    }
}
