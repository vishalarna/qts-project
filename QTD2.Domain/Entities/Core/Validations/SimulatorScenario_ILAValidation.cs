using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_ILASpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_ILAValidation : Validation<SimulatorScenario_ILA>, ISimulatorScenario_ILAValidation
    {
        public SimulatorScenario_ILAValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_ILA>(new SimulatorScenario_ILA_SimulatorScenarioIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_ILA_SimulatorScenarioIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_ILA>(new SimulatorScenario_ILA_ILAIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_ILA_ILAIdRequiredSpec"]));
        }
    }
}