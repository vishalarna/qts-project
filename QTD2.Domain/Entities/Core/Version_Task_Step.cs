namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_Step : Common.Entity
    {
        public int TaskStepId { get; set; }

        public int VersionTaskId { get; set; }

        public string Description { get; set; }

        public int? Number { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public virtual Task_Step Task_Step { get; set; }

        public Version_Task_Step()
        {
        }

        public Version_Task_Step(Task_Step step)
        {
            TaskStepId = step.Id;
            VersionTaskId = step.TaskId;
            Description = step.Description;
            Number = step.Number;
        }

        public Version_Task_Step(Version_Task task, Task_Step step)
        {
            TaskStepId = step.Id;
            VersionTaskId = task.Id;
            Description = step.Description;
            Number = step.Number;
        }
    }
}
