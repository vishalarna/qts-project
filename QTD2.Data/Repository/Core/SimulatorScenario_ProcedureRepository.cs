using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class SimulatorScenario_ProcedureRepository : Common.Repository<SimulatorScenario_Procedure>, ISimulatorScenario_ProcedureRepository
    {
        public SimulatorScenario_ProcedureRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
