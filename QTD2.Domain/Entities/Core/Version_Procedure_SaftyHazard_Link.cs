namespace QTD2.Domain.Entities.Core
{
    public class Version_Procedure_SaftyHazard_Link : Common.Entity
    {
        public int Version_SaftyHazardId { get; set; }

        public int Version_ProcedureId { get; set; }

        public string VersionNumber { get; set; }

        public virtual Version_SaftyHazard Version_SaftyHazard { get; set; }

        public virtual Version_Procedure Version_Procedure { get; set; }

        public Version_Procedure_SaftyHazard_Link()
        {
        }

        public Version_Procedure_SaftyHazard_Link(int version_SaftyHazardId, int version_ProcedureId, string versionNumber)
        {
            Version_SaftyHazardId = version_SaftyHazardId;
            Version_ProcedureId = version_ProcedureId;
            VersionNumber = versionNumber;
        }
    }
}
