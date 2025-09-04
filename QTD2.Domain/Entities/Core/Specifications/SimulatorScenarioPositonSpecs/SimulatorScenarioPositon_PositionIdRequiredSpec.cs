using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenarioPositonSpecs
{
    public class SimulatorScenarioPositon_PositionIdRequiredSpec : ISpecification<SimulatorScenario_Position>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Position entity, params object[] args)
        {
            return entity.PositionID > 0;
        }
    }
}