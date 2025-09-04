using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_EnablingObjectiveSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_EnablingObjectiveValidation : Validation<SimulatorScenario_EnablingObjective>, ISimulatorScenario_EnablingObjectiveValidation
    {
        public SimulatorScenario_EnablingObjectiveValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_EnablingObjective>(new SimulatorScenario_EO_EOIDRequired(), _validationStringLocalizer["SimulatorScenario_EO_EOIDRequired"]));
            AddRule(new ValidationRule<SimulatorScenario_EnablingObjective>(new SimulatorScenario_EO_SimScenarioIDRequired(), _validationStringLocalizer["SimulatorScenario_EO_SimScenarioIDRequired"]));
        }
    }
}