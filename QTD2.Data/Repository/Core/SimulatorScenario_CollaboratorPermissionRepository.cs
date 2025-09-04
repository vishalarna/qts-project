using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class SimulatorScenario_CollaboratorPermissionRepository : Common.Repository<SimulatorScenario_CollaboratorPermission>, ISimulatorScenario_CollaboratorPermissionRepository
    {
        public SimulatorScenario_CollaboratorPermissionRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
