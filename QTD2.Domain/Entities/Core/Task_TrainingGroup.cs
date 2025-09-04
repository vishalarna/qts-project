using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Task_TrainingGroup : Common.Entity
    {
        public int TaskId { get; set; }

        public int TrainingGroupId { get; set; }

        public virtual Task Task { get; set; }

        public virtual TrainingGroup TrainingGroup { get; set; }

        public Task_TrainingGroup()
        {
        }

        public Task_TrainingGroup(Task task, TrainingGroup trainingGroup)
        {
            Task = task;
            TrainingGroup = trainingGroup;
            TaskId = task.Id;
            TrainingGroupId = trainingGroup.Id;
        }

        public Version_Task_TrainingGroup CreateSnapshot()
        {
            return new Version_Task_TrainingGroup(this.TaskId, this.TrainingGroupId);
        }
    }
}
