namespace QTD2.Domain.Entities.Core
{
    public class Task_Tool : Common.Entity
    {
        public int TaskId { get; set; }

        public int ToolId { get; set; }

        public virtual Tool Tool { get; set; }

        public virtual Task Task { get; set; }

        public Task_Tool(Task task, Tool tool)
        {
            TaskId = task.Id;
            ToolId = tool.Id;
            Task = task;
            Tool = tool;
        }

        public Task_Tool()
        {
        }

        public Version_Task_Tool_Link CreateSnapshot()
        {
            return new Version_Task_Tool_Link(this.TaskId, this.ToolId);
        }
    }
}
