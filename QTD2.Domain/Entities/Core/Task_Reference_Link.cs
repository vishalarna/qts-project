using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Task_Reference_Link : Entity
    {
        public int TaskReferenceId { get; set; }

        public int TaskId { get; set; }

        public virtual Task Task { get; set; }

        public virtual Task_Reference Task_Reference { get; set; }

        public Task_Reference_Link(Task task, Task_Reference task_Reference)
        {
            Task = task;
            Task_Reference = task_Reference;
            TaskId = task.Id;
            TaskReferenceId = task_Reference.Id;
        }

        public Task_Reference_Link()
        {
        }
    }
}
