using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class SimulatorScenario_ILARepository : Common.Repository<SimulatorScenario_ILA>, ISimulatorScenario_ILARepository
    {
        public SimulatorScenario_ILARepository(QTDContext context)
            : base(context)
        {
        }
    }
}
