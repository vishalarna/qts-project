using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_EventAndScriptSpecs
{
    public class SimulatorScenario_EventAndScript_InitiatorIdRequiredSpec : ISpecification<SimulatorScenario_EventAndScript>
    {
        public bool IsSatisfiedBy(SimulatorScenario_EventAndScript entity, params object[] args)
        {
            return entity.InitiatorId > 0;
        }
    }
}
