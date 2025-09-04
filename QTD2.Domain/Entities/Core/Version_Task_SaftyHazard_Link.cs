namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_SaftyHazard_Link : Common.Entity
    {
        public int Version_TaskId { get; set; }

        public int Version_SaftyHazardId { get; set; }

        public string VersionNumber { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public virtual Version_SaftyHazard Version_SaftyHazard { get; set; }

        public Version_Task_SaftyHazard_Link()
        {
        }

        public Version_Task_SaftyHazard_Link(int version_TaskId, int version_SaftyHazardId, string versionNumber)
        {
            Version_TaskId = version_TaskId;
            Version_SaftyHazardId = version_SaftyHazardId;
            VersionNumber = versionNumber;
        }
    }
}
