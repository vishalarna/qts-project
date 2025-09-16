using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_ScriptSpecs
{
    public class SimulatorScenario_Script_EventIdRequiredSpec : ISpecification<SimulatorScenario_Script>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Script entity, params object[] args)
        {
            return entity.EventId > 0;
        }
    }
}
