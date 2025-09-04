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
   public class SimulatorScenario_DifficultyService : Common.Service<SimulatorScenario_Difficulty>, ISimulatorScenario_DifficultyService
    {
        public SimulatorScenario_DifficultyService(ISimulatorScenario_DifficultyRepository repository, ISimulatorScenario_DifficultyValidation validation)
            : base(repository, validation)
        {
        }
    }
}
