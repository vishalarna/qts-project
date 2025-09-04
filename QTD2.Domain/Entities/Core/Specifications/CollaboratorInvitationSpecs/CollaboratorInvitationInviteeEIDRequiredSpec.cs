using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CollaboratorInvitationSpecs
{
    public class CollaboratorInvitationInviteeEIDRequiredSpec : ISpecification<CollaboratorInvitation>
    {
        public bool IsSatisfiedBy(CollaboratorInvitation entity, params object[] args)
        {
            return entity.InviteeEID > 0;
        }
    }
}
