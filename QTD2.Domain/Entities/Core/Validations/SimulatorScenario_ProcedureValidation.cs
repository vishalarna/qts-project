using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_ProcedureSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_ProcedureValidation : Validation<SimulatorScenario_Procedure>, ISimulatorScenario_ProcedureValidation
    {
        public SimulatorScenario_ProcedureValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Procedure>(new SimulatorScenario_Procedure_SimulatorScenarioIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Procedure_SimulatorScenarioIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Procedure>(new SimulatorScenario_Procedure_ProcedureIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Procedure_ProcedureIdRequiredSpec"]));
        }
    }
}
