using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
  public  class SimulatorScenario_PrerequisiteRepository : Common.Repository<SimulatorScenario_Prerequisite>, ISimulatorScenario_PrerequisiteRepository
    {
        public SimulatorScenario_PrerequisiteRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
