using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public  class SimulatorScenario_EventRepository : Common.Repository<SimulatorScenario_Event>, ISimulatorScenario_EventRepository
    {
        public SimulatorScenario_EventRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
