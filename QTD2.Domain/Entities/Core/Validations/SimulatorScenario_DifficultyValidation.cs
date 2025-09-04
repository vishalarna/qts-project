using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenarioDifficultySpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_DifficultyValidation : Validation<SimulatorScenario_Difficulty>, ISimulatorScenario_DifficultyValidation
    {
        public SimulatorScenario_DifficultyValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Difficulty>(new SimulatorScenarioDifficultyRequired(), _validationStringLocalizer["SimulatorScenarioDifficultyRequired"]));
        }
    }
}