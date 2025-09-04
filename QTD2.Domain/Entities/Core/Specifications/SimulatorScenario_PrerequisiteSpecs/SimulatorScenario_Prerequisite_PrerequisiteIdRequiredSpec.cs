using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_PrerequisiteSpecs
{
    public class SimulatorScenario_Prerequisite_PrerequisiteIdRequiredSpec : ISpecification<SimulatorScenario_Prerequisite>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Prerequisite entity, params object[] args)
        {
            return entity.PrerequisiteId > 0;
        }
    }
}

