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
    public class SimulatorScenarioService : Common.Service<SimulatorScenario>, ISimulatorScenarioService
    {
        public SimulatorScenarioService(ISimulatorScenarioRepository repository, ISimulatorScenarioValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<SimulatorScenario> GetForCopy(int id)
        {
            var simulatorScenarioCopy = await FindWithIncludeAsync(r => r.Id == id, new string[] { "ILAs", "Procedures", "TaskCriterias", "Events", "Positions", "Collaborators", "Prerequisites", "EnablingObjectives", "Tasks" });
            return simulatorScenarioCopy.First();
        }

        public async Task<SimulatorScenario> GetSimulatorScenarioWithEventsAndScriptsAsync(int id)
        {
            var obj = await FindWithIncludeAsync(r => r.Id == id, new[] { "Events" });
            return obj.First();
        }

        public async Task<SimulatorScenario> GetAsync(int id)
        {
            var simScenario = await GetWithIncludeAsync(id, new[] { "Collaborators.User.Person", "Events.Scripts.Criterias", "Collaborators.Permission" });
            var simScenarioWithPositions = await GetWithIncludeAsync(id, new[] { "Positions.Position" });
            var simScenarioWithTasks = await GetWithIncludeAsync(id, new[] { "Tasks.Task.SubdutyArea.DutyArea" });
            var simScenarioWithEOs = await GetWithIncludeAsync(id, new[] { "EnablingObjectives.EnablingObjective.EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category" });
            var simScenarioWithProcedures = await GetWithIncludeAsync(id, new[] { "Procedures.Procedure" });
            var simScenarioWithTaskCriterias = await GetWithIncludeAsync(id, new[] { "TaskCriterias.Task.SubdutyArea.DutyArea" });
            var simScenarioWithILAs= await GetWithIncludeAsync(id, new[] { "ILAs.ILA" });
            var simScenarioWithPrerequisites= await GetWithIncludeAsync(id, new[] { "Prerequisites.Prerequisite" });

            if(simScenario != null)
            {
                simScenario.Positions = simScenarioWithPositions.Positions;
                simScenario.Tasks = simScenarioWithTasks.Tasks;
                simScenario.EnablingObjectives = simScenarioWithEOs.EnablingObjectives;
                simScenario.Procedures = simScenarioWithProcedures.Procedures;
                simScenario.TaskCriterias = simScenarioWithTaskCriterias.TaskCriterias;
                simScenario.ILAs = simScenarioWithILAs.ILAs;
                simScenario.Prerequisites = simScenarioWithPrerequisites.Prerequisites;
            }

            return simScenario;
        }

    }
}
