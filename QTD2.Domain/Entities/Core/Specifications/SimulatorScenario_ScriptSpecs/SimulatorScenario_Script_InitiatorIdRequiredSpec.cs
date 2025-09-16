using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_ScriptSpecs
{
    public class SimulatorScenario_Script_InitiatorIdRequiredSpec : ISpecification<SimulatorScenario_Script>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Script entity, params object[] args)
        {
            return entity.InitiatorId > 0;
        }
    }
}
