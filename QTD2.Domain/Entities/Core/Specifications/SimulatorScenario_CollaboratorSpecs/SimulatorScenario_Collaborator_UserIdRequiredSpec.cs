using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SimulatorScenario_CollaboratorSpecs
{
    public class SimulatorScenario_Collaborator_UserIdRequiredSpec : ISpecification<SimulatorScenario_Collaborator>
    {
        public bool IsSatisfiedBy(SimulatorScenario_Collaborator entity, params object[] args)
        {
            return entity.UserId > 0;
        }
    }
}