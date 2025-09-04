using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_TaskSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
   public class SimulatorScenario_TaskValidation : Validation<SimulatorScenario_Task>, ISimulatorScenario_TaskValidation
    {
        public SimulatorScenario_TaskValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Task>(new SimulatorScenario_TaskSimulatorScenarioIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_TaskSimulatorScenarioIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Task>(new SimulatorScenario_Task_TaskIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Task_TaskIdRequiredSpec"]));
        }
    }
}
