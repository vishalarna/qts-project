using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_CollaboratorSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_CollaboratorValidation : Validation<SimulatorScenario_Collaborator>, ISimulatorScenario_CollaboratorValidation
    {
        public SimulatorScenario_CollaboratorValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_Collaborator>(new SimulatorScenario_Collaborator_SimulatorScenarioIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Collaborator_SimulatorScenarioIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Collaborator>(new SimulatorScenario_Collaborator_UserIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Collaborator_UserIdRequiredSpec"]));
            AddRule(new ValidationRule<SimulatorScenario_Collaborator>(new SimulatorScenario_Collaborator_PermissionIdRequiredSpec(), _validationStringLocalizer["SimulatorScenario_Collaborator_PermissionIdRequiredSpec"]));
        }
    }
}