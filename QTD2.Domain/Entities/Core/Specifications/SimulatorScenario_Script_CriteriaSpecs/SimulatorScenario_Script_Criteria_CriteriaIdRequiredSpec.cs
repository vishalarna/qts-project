using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_Script_CriteriaSpecs
{
    public class SimulatorScenario_Script_Criteria_CriteriaIdRequiredSpec : ISpecification<SimulatorScenario_Script_Criteria>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Script_Criteria entity, params object[] args)
        {
            return entity.CriteriaId > 0;
        }
    }
}
