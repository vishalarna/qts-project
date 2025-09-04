using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_Task_CriteriaSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_Task_CriteriaValidation : Validation<SimulatorScenario_Task_Criteria>, ISimulatorScenario_Task_CriteriaValidation
    {
        public SimulatorScenario_Task_CriteriaValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Task_Criteria>(new SimulatorScenario_Task_Criteria_SimulatorScenarioIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_TaskSimulatorScenarioIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Task_Criteria>(new SimulatorScenario_Task_Criteria_TaskIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Task_Criteria_TaskIdRequiredSpec"]));
        }
    }
}