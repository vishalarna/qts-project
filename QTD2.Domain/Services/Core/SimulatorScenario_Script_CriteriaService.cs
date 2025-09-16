using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class SimulatorScenario_Script_CriteriaService : Common.Service<SimulatorScenario_Script_Criteria>, ISimulatorScenario_Script_CriteriaService
    {
        public SimulatorScenario_Script_CriteriaService(ISimulatorScenario_Script_CriteriaRepository repository, ISimulatorScenario_Script_CriteriaValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<SimulatorScenario_Script_Criteria> GetCriteriasByIdAsync(int scriptId)
        {
            return (await FindAsync(r => r.ScriptId == scriptId)).FirstOrDefault();
        }
    }
}