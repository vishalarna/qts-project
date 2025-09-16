
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class SimulatorScenario_ScriptRepository : Common.Repository<SimulatorScenario_Script>, ISimulatorScenario_ScriptRepository
    {
        public SimulatorScenario_ScriptRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
