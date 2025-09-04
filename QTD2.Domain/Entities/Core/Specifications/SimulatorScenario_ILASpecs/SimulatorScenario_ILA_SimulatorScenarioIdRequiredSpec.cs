using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_ILASpecs
{
    public class SimulatorScenario_ILA_SimulatorScenarioIdRequiredSpec : ISpecification<SimulatorScenario_ILA>
    {
        public bool IsSatisfiedBy(SimulatorScenario_ILA entity, params object[] args)
        {
            return entity.SimulatorScenarioID > 0;
        }
    }
}
