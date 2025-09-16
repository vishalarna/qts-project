using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_Script_CriteriaSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_Script_CriteriaValidation : Validation<SimulatorScenario_Script_Criteria>, ISimulatorScenario_Script_CriteriaValidation
    {
        public SimulatorScenario_Script_CriteriaValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Script_Criteria>(new SimulatorScenario_Script_Criteria_CriteriaIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Script_Criteria_CriteriaIdRequiredSpec"]));
        }
    }

}
