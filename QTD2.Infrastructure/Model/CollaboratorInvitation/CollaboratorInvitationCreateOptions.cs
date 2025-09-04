using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.CollaboratorInvitation
{
    public class CollaboratorInvitationCreateOptions
    {
        public int InvitedByEID { get; set; }

        public int InviteeEID { get; set; }

        public string InviteeEmailID { get; set; }

        public DateTime InviteDate { get; set; }

        public string InvitedMessage { get; set; }
    }
}
