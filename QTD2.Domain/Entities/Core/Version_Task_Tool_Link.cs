namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_Tool_Link : Common.Entity
    {
        public int Version_TaskId { get; set; }

        public int Version_ToolId { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public virtual Version_Tool Version_Tool { get; set; }

        public Version_Task_Tool_Link()
        {
        }

        public Version_Task_Tool_Link(int version_TaskId, int version_ToolId)
        {
            Version_TaskId = version_TaskId;
            Version_ToolId = version_ToolId;
        }
    }
}
