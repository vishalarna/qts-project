using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_CollaboratorInvitation
{
    public class Task_CollaboratorInvitationCreateOptions
    {
        public int? InvitedByEId { get; set; }

        public int InvitedForTaskId { get; set; }

        public int?[] InviteeEIds { get; set; }

        public string[] InviteeEmails { get; set; }

        public DateTime InviteDate { get; set; }

        public string Message { get; set; }
    }
}
