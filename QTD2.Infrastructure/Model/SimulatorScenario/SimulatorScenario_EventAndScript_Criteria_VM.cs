using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_EventAndScript_Criteria_VM
    {
        public int? Id { get; set; }
        public int CriteriaId { get; set; }

        public SimulatorScenario_EventAndScript_Criteria_VM(int id, int criteriaId)
        {
            Id = id;
            CriteriaId = criteriaId;
        }

        public SimulatorScenario_EventAndScript_Criteria_VM() { }
    }
}
