using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISimulatorScenario_CollaboratorService
    {
        public Task<SimulatorScenario_Collaborator> GetAsync(int id);
    }
}
