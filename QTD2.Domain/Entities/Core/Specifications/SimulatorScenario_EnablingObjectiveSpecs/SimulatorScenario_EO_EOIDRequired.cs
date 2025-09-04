using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_EnablingObjectiveSpecs
{
    public class SimulatorScenario_EO_EOIDRequired : ISpecification<SimulatorScenario_EnablingObjective>
    {
        public bool IsSatisfiedBy(SimulatorScenario_EnablingObjective entity, params object[] args)
        {
            return entity.EnablingObjectiveID > 0;
        }
    }
}