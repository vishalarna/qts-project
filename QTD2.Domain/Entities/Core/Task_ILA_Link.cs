using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Task_ILA_Link : Entity
    {
        public int TaskId { get; set; }

        public int ILAId { get; set; }

        public virtual Task Task { get; set; }

        public virtual ILA ILA { get; set; }

        public Task_ILA_Link(Task task, ILA ila)
        {
            TaskId = task.Id;
            ILAId = ila.Id;
            Task = task;
            ILA = ila;
        }

        public Task_ILA_Link()
        {
        }

        public Version_Task_ILA_Link CreateSnapshot()
        {
            return new Version_Task_ILA_Link(this.TaskId,this.ILAId, "Null");
        }
    }
}
