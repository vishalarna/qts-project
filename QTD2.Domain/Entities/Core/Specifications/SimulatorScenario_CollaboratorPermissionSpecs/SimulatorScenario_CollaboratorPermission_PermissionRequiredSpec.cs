using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_CollaboratorPermissionSpecs
{
    public class SimulatorScenario_CollaboratorPermission_PermissionRequiredSpec : ISpecification<SimulatorScenario_CollaboratorPermission>
    {
        public bool IsSatisfiedBy(SimulatorScenario_CollaboratorPermission entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Permission);
        }
    }
}

