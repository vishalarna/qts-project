using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Script_Criteria: Entity
    {
        public int? ScriptId { get; set; }
        public int CriteriaId { get; set; }
        public SimulatorScenario_Script Script { get; set; }
        public SimulatorScenario_Task_Criteria Criteria { get; set; }

        public SimulatorScenario_Script_Criteria(int? scriptId, int criteriaId)
        {
            ScriptId = scriptId;
            CriteriaId = criteriaId;
        }
        public SimulatorScenario_Script_Criteria()
        {
        }
    }
}
