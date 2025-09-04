using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Collaborator_Link
{
    public class Task_Collaborator_LinkOptions
    {
        public int TaskId { get; set; }

        public int TaskCollabInviteId { get; set; }

        public bool isTaskCreator { get; set; }

        public bool IsActive { get; set; }
    }
}
