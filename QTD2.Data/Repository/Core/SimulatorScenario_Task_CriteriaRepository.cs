using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class SimulatorScenario_Task_CriteriaRepository : Common.Repository<SimulatorScenario_Task_Criteria>, ISimulatorScenario_Task_CriteriaRepository
    {
        public SimulatorScenario_Task_CriteriaRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
