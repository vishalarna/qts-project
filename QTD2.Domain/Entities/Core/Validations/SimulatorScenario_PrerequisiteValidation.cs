using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_PrerequisiteSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_PrerequisiteValidation : Validation<SimulatorScenario_Prerequisite>, ISimulatorScenario_PrerequisiteValidation
    {
        public SimulatorScenario_PrerequisiteValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Prerequisite>(new SimulatorScenario_Prerequisite_SimulatorScenarioIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Prerequisite_SimulatorScenarioIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Prerequisite>(new SimulatorScenario_Prerequisite_PrerequisiteIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Prerequisite_PrerequisiteIdRequiredSpec"]));
        }
    }
}