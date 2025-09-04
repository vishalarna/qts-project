using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_Task_CriteriaSpecs
{
   public class SimulatorScenario_Task_Criteria_TaskIdRequiredSpec : ISpecification<SimulatorScenario_Task_Criteria>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Task_Criteria entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
