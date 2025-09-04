using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_ProcedureSpecs
{
    public class SimulatorScenario_Procedure_SimulatorScenarioIdRequiredSpec : ISpecification<SimulatorScenario_Procedure>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Procedure entity, params object[] args)
        {
            return entity.SimulatorScenarioId > 0;
        }
    }
}
