using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_EventAndScriptSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_EventAndScriptValidation : Validation<SimulatorScenario_EventAndScript>, ISimulatorScenario_EventAndScriptValidation
    {
        public SimulatorScenario_EventAndScriptValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_EventAndScript>(new SimulatorScenario_EventAndScript_SimulatorScenarioIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_EventAndScript_SimulatorScenarioIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_EventAndScript>(new SimulatorScenario_EventAndScript_OrderRequiredSpec(), _validationStringLocalizer["SimulatorScenario_EventAndScript_OrderRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_EventAndScript>(new SimulatorScenario_EventAndScript_TitleRequiredSpec(), _validationStringLocalizer["SimulatorScenario_EventAndScript_TitleRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_EventAndScript>(new SimulatorScenario_EventAndScript_InitiatorIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_EventAndScript_InitiatorIdRequiredSpec"]));
        }
    }

}
