namespace QTD2.Domain.Entities.Core
{
    public class Task_EnablingObjective_Link : Common.Entity
    {
        public int TaskId { get; set; }

        public int EnablingObjectiveId { get; set; }

        public virtual Task Task { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public Task_EnablingObjective_Link(Task task, EnablingObjective enablingObjective)
        {
            TaskId = task.Id;
            EnablingObjectiveId = enablingObjective.Id;
            EnablingObjective = enablingObjective;
        }

        public Task_EnablingObjective_Link()
        {
        }

        public Version_Task_EnablingObjective_Link CreateSnapshot()
        {
            return new Version_Task_EnablingObjective_Link(this.EnablingObjectiveId, this.TaskId, "NULL");
        }
    }
}
