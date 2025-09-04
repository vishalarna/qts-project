using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Task_CollaboratorInvitation : Entity
    {
        public Task_CollaboratorInvitation(int invitedByEId, int invitedForTaskId, int inviteeEId, DateTime inviteDate, string message, string inviteeEmail)
        {
            InvitedByEId = invitedByEId;
            InvitedForTaskId = invitedForTaskId;
            InviteeEId = inviteeEId;
            InviteDate = inviteDate;
            Message = message;
            InviteeEmail = inviteeEmail;
        }

        public Task_CollaboratorInvitation()
        {
        }

        public int InvitedByEId { get; set; }

        public int InvitedForTaskId { get; set; }

        public int? InviteeEId { get; set; }

        public string InviteeEmail { get; set; }

        public DateTime InviteDate { get; set; }

        public string Message { get; set; }

        public virtual ICollection<Task_Collaborator_Link> Task_Collaborator_Links { get; set; } = new List<Task_Collaborator_Link>();
    }
}
