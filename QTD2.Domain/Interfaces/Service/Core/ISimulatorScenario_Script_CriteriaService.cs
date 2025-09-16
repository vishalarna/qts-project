using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISimulatorScenario_Script_CriteriaService : Common.IService<SimulatorScenario_Script_Criteria>
    {
        public Task<SimulatorScenario_Script_Criteria> GetCriteriasByIdAsync(int scriptId);
    }
}
