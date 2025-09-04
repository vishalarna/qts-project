using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_UpdateTasks_VM
    {
        public bool IncludeEnablingObjectives { get; set; }
        public bool IncludeProcedures { get; set; }
        public List<SimulatorScenario_Task_VM> Tasks { get; set; }

        public SimulatorScenario_UpdateTasks_VM(bool includeEnablingObjectives, bool includeProcedures)
        {
            IncludeEnablingObjectives = includeEnablingObjectives;
            IncludeProcedures = includeProcedures;
        }

        public SimulatorScenario_UpdateTasks_VM() { }
    }
}
