using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_TaskSpecs
{
   public class SimulatorScenario_Task_TaskIdRequiredSpec: ISpecification<SimulatorScenario_Task>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Task entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
