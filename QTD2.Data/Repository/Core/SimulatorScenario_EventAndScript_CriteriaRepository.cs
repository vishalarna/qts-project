using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class SimulatorScenario_EventAndScript_CriteriaRepository : Common.Repository<SimulatorScenario_EventAndScript_Criteria>, ISimulatorScenario_EventAndScript_CriteriaRepository
    {
        public SimulatorScenario_EventAndScript_CriteriaRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
