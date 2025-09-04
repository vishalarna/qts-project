using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Task_History : Entity
    {
        public Task_History()
        {
        }

        public Task_History(int taskId, bool oldStatus, bool newStatus, DateTime changeEffectiveDate, string changeNotes)
        {
            TaskId = taskId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeEffectiveDate = changeEffectiveDate;
            ChangeNotes = changeNotes;
        }

        public int TaskId { get; set; }

        public int Version_TaskId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public virtual Task Task { get; set; }

        public virtual Version_Task Version_Task { get; set; }
    }
}
