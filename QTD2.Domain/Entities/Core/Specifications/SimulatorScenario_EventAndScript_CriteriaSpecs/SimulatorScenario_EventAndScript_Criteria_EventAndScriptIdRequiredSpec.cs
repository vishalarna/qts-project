using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_EventAndScript_CriteriaSpecs
{
    public class SimulatorScenario_EventAndScript_Criteria_EventAndScriptIdRequiredSpec : ISpecification<SimulatorScenario_EventAndScript_Criteria>
    {
        public bool IsSatisfiedBy(SimulatorScenario_EventAndScript_Criteria entity, params object[] args)
        {
            return entity.EventAndScriptId > 0;
        }
    }
}
