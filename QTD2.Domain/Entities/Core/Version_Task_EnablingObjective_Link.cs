namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_EnablingObjective_Link : Common.Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_TaskId { get; set; }

        public string VersionNumber { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public Version_Task_EnablingObjective_Link()
        {
        }

        public Version_Task_EnablingObjective_Link(int version_EnablingObjectiveId, int version_TaskId, string versionNumber)
        {
            Version_EnablingObjectiveId = version_EnablingObjectiveId;
            Version_TaskId = version_TaskId;
            VersionNumber = versionNumber;
        }
    }
}
