using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenarioDifficultySpecs
{
    public class SimulatorScenarioDifficultyRequired : ISpecification<SimulatorScenario_Difficulty>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Difficulty entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Difficulty);
        }
    }
}
