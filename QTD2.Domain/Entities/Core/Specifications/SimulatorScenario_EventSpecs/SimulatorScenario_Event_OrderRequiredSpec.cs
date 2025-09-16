using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_EventSpecs
{
    public class SimulatorScenario_Event_OrderRequiredSpec : ISpecification<SimulatorScenario_Event>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Event entity, params object[] args)
        {
            return entity.Order > 0;
        }
    }
}