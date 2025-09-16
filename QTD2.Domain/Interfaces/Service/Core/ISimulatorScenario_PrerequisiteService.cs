using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
   public interface ISimulatorScenario_PrerequisiteService : Common.IService<SimulatorScenario_Prerequisite>
    {
        public Task<List<SimulatorScenario_Prerequisite>> GetPrerequisiteBySimulatorIdAsync(int simScenarioId);
        public Task<List<SimulatorScenario_Prerequisite>> GetPrerequisiteByILAIdAsync(int iLAId);
    }
}
