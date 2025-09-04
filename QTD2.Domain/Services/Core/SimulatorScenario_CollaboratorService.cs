using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
   public class SimulatorScenario_CollaboratorService : Common.Service<SimulatorScenario_Collaborator>, ISimulatorScenario_CollaboratorService
    {
        public SimulatorScenario_CollaboratorService(ISimulatorScenario_CollaboratorRepository repository, ISimulatorScenario_CollaboratorValidation validation)
            : base(repository, validation)
        {
        }
        public async Task<List<SimulatorScenario_Collaborator>> GetCollaboratorBySimulatorIdAsync(int simulatorScenarioId)
        {
            var simulatorScenario_Collaborator = await FindWithIncludeAsync(x => x.SimulatorScenarioId == simulatorScenarioId, new string[] { "Permission", "User.Person" });
            return simulatorScenario_Collaborator.ToList();
        }
    }
}