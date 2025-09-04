using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Task_Collaborator_Link : Entity
    {
        public int TaskId { get; set; }

        public int TaskCollabInviteId { get; set; }

        public bool isTaskCreator { get; set; }

        public bool IsActive { get; set; }

        public virtual Task Task { get; set; }

        public virtual Task_CollaboratorInvitation Task_CollaboratorInvitation { get; set; }

        public Task_Collaborator_Link(Task task, Task_CollaboratorInvitation task_CollaboratorInvitation)
        {
            Task = task;
            Task_CollaboratorInvitation = task_CollaboratorInvitation;
            TaskId = task.Id;
            TaskCollabInviteId = task_CollaboratorInvitation.Id;
        }

        public Task_Collaborator_Link()
        {
        }
    }
}
