using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_EventAndScript_CriteriaSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_EventAndScript_CriteriaValidation : Validation<SimulatorScenario_EventAndScript_Criteria>, ISimulatorScenario_EventAndScript_CriteriaValidation
    {
        public SimulatorScenario_EventAndScript_CriteriaValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_EventAndScript_Criteria>(new SimulatorScenario_EventAndScript_Criteria_EventAndScriptIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_EventAndScript_Criteria_EventAndScriptIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_EventAndScript_Criteria>(new SimulatorScenario_EventAndScript_Criteria_CriteriaIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_EventAndScript_Criteria_CriteriaIdRequiredSpec"]));
        }
    }

}
