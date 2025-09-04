using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class SimulatorScenario_EnablingObjectiveRepository : Common.Repository<SimulatorScenario_EnablingObjective>, ISimulatorScenario_EnablingObjectiveRepository
    {
        public SimulatorScenario_EnablingObjectiveRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
