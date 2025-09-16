using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_EventSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_EventValidation : Validation<SimulatorScenario_Event>, ISimulatorScenario_EventValidation
    {
        public SimulatorScenario_EventValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Event>(new SimulatorScenario_Event_SimulatorScenarioIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Event_SimulatorScenarioIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Event>(new SimulatorScenario_Event_OrderRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Event_OrderRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Event>(new SimulatorScenario_Event_TitleRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Event_TitleRequiredSpec"]));
        }
    }

}
