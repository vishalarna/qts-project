using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
  public  class SimulatorScenario_CollaboratorRepository : Common.Repository<SimulatorScenario_Collaborator>, ISimulatorScenario_CollaboratorRepository
    {
        public SimulatorScenario_CollaboratorRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
