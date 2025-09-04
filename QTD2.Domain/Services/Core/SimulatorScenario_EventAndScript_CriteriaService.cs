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
  public  class SimulatorScenario_EventAndScript_CriteriaService : Common.Service<SimulatorScenario_EventAndScript_Criteria>, ISimulatorScenario_EventAndScript_CriteriaService
    {
        public SimulatorScenario_EventAndScript_CriteriaService(ISimulatorScenario_EventAndScript_CriteriaRepository repository, ISimulatorScenario_EventAndScript_CriteriaValidation validation)
            : base(repository, validation)
        {
        }
    }
}