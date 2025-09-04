using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_EventAndScript_Criteria : Entity
    {
        public int? EventAndScriptId { get; set; }
        public int CriteriaId { get; set; }
        public SimulatorScenario_EventAndScript EventAndScript { get; set; }
        public SimulatorScenario_Task_Criteria Criteria { get; set; }

        public SimulatorScenario_EventAndScript_Criteria(int? eventAndScriptId, int criteriaId)
        {
            EventAndScriptId = eventAndScriptId;
            CriteriaId = criteriaId;
        }
        public SimulatorScenario_EventAndScript_Criteria()
        {
        }
    }
}
