using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class SimulatorScenario_Script_CriteriaRepository : Common.Repository<SimulatorScenario_Script_Criteria>, ISimulatorScenario_Script_CriteriaRepository
    {
        public SimulatorScenario_Script_CriteriaRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
