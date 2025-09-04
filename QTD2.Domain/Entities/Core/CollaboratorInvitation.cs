using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class CollaboratorInvitation : Entity
    {
        public int InvitedByEID { get; set; }

        public int InviteeEID { get; set; }

        public string InviteeEmailID { get; set; }

        public DateTime InviteDate { get; set; }

        public string InvitedMessage { get; set; }

        public virtual Employee InvitedByEmployee { get; set; }

        public virtual Employee InviteeEmployee { get; set; }

        public virtual ICollection<ILACollaborator> ILACollaborators { get; set; } = new List<ILACollaborator>();

        public CollaboratorInvitation()
        {
        }

        public CollaboratorInvitation(int invitedByEID, int inviteeEID, string inviteeEmailID, DateTime inviteDate, string invitedMessage)
        {
            InvitedByEID = invitedByEID;
            InviteeEID = inviteeEID;
            InviteeEmailID = inviteeEmailID;
            InviteDate = inviteDate;
            InvitedMessage = invitedMessage;
        }
    }
}
