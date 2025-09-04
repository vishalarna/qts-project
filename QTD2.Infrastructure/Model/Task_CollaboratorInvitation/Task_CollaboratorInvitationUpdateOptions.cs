using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_CollaboratorInvitation
{
    public class Task_CollaboratorInvitationUpdateOptions
    {
        public int InvitedByEId { get; set; }

        public int InvitedForTaskId { get; set; }

        public int InviteeEId { get; set; }

        public string Email { get; set; }

        public string InviteeEmailId { get; set; }

        public DateTime InviteDate { get; set; }

        public string Message { get; set; }
    }
}
