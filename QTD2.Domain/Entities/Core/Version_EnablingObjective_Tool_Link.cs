namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_Tool_Link : Common.Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_ToolId { get; set; }

        public virtual Version_Tool Version_Tool { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public Version_EnablingObjective_Tool_Link()
        {
        }

        public Version_EnablingObjective_Tool_Link(int version_EnablingObjectiveId, int version_ToolId)
        {
            Version_EnablingObjectiveId = version_EnablingObjectiveId;
            Version_ToolId = version_ToolId;
        }
    }
}
