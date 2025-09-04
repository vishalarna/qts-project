using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class Task_Step : Common.Entity
    {
        public int TaskId { get; set; }

        public string Description { get; set; }

        public int? Number { get; set; }

        public int? ParentStepId { get; set; }

        public virtual Task Task { get; set; }

        public virtual ICollection<Version_Task_Step> Version_Task_Steps { get; set; } = new List<Version_Task_Step>();

        public virtual ICollection<TaskReQualificationEmp_Steps> TaskReQualificationEmp_Steps { get; set; } = new List<TaskReQualificationEmp_Steps>();

        public Task_Step(int taskId, string description, int number, int? parentStepId)
        {
            TaskId = taskId;
            Description = description;
            Number = number;
            ParentStepId = parentStepId;
        }

        public Task_Step()
        {
        }

        public Version_Task_Step CreateSnapshot()
        {
            return new Version_Task_Step(this);
        }
    }
}
