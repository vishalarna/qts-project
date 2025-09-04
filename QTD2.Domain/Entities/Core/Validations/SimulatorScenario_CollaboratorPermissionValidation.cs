using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_CollaboratorPermissionSpecs;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Entities.Core.Validations
{
    public class SimulatorScenario_CollaboratorPermissionValidation : Validation<SimulatorScenario_CollaboratorPermission>, ISimulatorScenario_CollaboratorPermissionValidation
    {
        public SimulatorScenario_CollaboratorPermissionValidation(IStringLocalizerFactory stringLocalizerFactory)
            : base(stringLocalizerFactory)
        {
            AddRule(new ValidationRule<SimulatorScenario_CollaboratorPermission>(new SimulatorScenario_CollaboratorPermission_PermissionRequiredSpec(), _validationStringLocalizer["SimulatorScenario_CollaboratorPermission_PermissionRequiredSpec"]));
        }
    }
}