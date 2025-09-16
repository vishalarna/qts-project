using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_ScriptSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_ScriptValidation : Validation<SimulatorScenario_Script>, ISimulatorScenario_ScriptValidation
    {
        public SimulatorScenario_ScriptValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Script>(new SimulatorScenario_Script_InitiatorIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Script_InitiatorIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Script>(new SimulatorScenario_Script_TitleRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Script_TitleRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Script>(new SimulatorScenario_Script_EventIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Script_EventIdRequiredSpec"]));
        }
    }

}
