using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Task_MetaTask_Link : Common.Entity
    {
        public int Meta_TaskId { get; set; }

        public int TaskId { get; set; }

        public virtual Task Task { get; set; }

        public virtual Task Meta_Task { get; set; }

        public Task_MetaTask_Link()
        {
        }

        public Task_MetaTask_Link(Task task, Task meta_task)
        {
            Meta_TaskId = meta_task.Id;
            TaskId = task.Id;
            Meta_Task = meta_task;
            Task = task;
        }
    }
}
