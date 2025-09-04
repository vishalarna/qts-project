using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenarioPositonSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_PositonValidation : Validation<SimulatorScenario_Position>, ISimulatorScenario_PositonValidation
    {
        public SimulatorScenario_PositonValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Position>(new SimulatorScenarioPositon_PositionIdRequiredSpec(), _validationStringLocalizer["SimulatorScenarioPositon_PositionIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Position>(new SimulatorScenarioPositon_SimScenarioIdRequiredSpec(), _validationStringLocalizer["SimulatorScenarioPositon_SimScenarioIdRequiredSpec"]));
        }
    }
}