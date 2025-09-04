using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_StatusSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_StatusValidation : Validation<SimulatorScenario_Status>, ISimulatorScenario_StatusValidation
    {
        public SimulatorScenario_StatusValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Status>(new SimulatorScenario_Status_StatusrequiredSpec(), _validationStringLocalizer["SimulatorScenario_Status_StatusrequiredSpec"]));
        }
    }
}