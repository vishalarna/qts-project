using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_StatusSpecs
{
    public class SimulatorScenario_Status_StatusrequiredSpec : ISpecification<SimulatorScenario_Status>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Status entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Status);
        }
    }
}

