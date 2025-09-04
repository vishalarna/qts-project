using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_CollaboratorInvitationSpecs
{
    public class Task_CollaboratorInvitationInvitedByEIdRequiredSpec : ISpecification<Task_CollaboratorInvitation>
    {
        public bool IsSatisfiedBy(Task_CollaboratorInvitation entity, params object[] args)
        {
            return entity.InvitedByEId > 0;
        }
    }
}
