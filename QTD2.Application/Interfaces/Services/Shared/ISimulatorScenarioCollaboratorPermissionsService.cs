using QTD2.Infrastructure.Model.SimulatorScenario_CollaboratorPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISimulatorScenarioCollaboratorPermissionsService
    {
        public Task<List<SimulatorScenario_CollaboratorPermissions_VM>> GetAllAsync();
    }
}
