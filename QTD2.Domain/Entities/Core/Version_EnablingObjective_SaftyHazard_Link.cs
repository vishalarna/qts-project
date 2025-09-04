namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_SaftyHazard_Link : Common.Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_SaftyHazardId { get; set; }

        public string VersionNumber { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Version_SaftyHazard Version_SaftyHazard { get; set; }

        public Version_EnablingObjective_SaftyHazard_Link()
        {
        }

        public Version_EnablingObjective_SaftyHazard_Link(int version_EnablingObjectiveId, int version_SaftyHazardId,
            string versionNumber)
        {
            Version_EnablingObjectiveId = version_EnablingObjectiveId;
            Version_SaftyHazardId = version_SaftyHazardId;
            VersionNumber = versionNumber;
        }
    }
}
