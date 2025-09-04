using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILACollaborator : Entity
    {
        public int ILAId { get; set; }

        public int CollaboratorInviteId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual CollaboratorInvitation CollaboratorInvitation { get; set; }

        public ILACollaborator(ILA ila, CollaboratorInvitation collaboratorInvitation)
        {
            ILA = ila;
            CollaboratorInvitation = collaboratorInvitation;
            ILAId = ila.Id;
            CollaboratorInviteId = collaboratorInvitation.Id;
        }

        public ILACollaborator()
        {
        }
    }
}
