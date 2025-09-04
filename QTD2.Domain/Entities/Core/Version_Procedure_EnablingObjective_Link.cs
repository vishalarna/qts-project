namespace QTD2.Domain.Entities.Core
{
    public class Version_Procedure_EnablingObjective_Link : Common.Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_ProcedureId { get; set; }

        public string VersionNumber { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Version_Procedure Version_Procedure { get; set; }

        public Version_Procedure_EnablingObjective_Link()
        {
        }

        public Version_Procedure_EnablingObjective_Link(int version_EnablingObjectiveId, int version_ProcedureId,
            string versionNumber)
        {
            Version_EnablingObjectiveId = version_EnablingObjectiveId;
            Version_ProcedureId = version_ProcedureId;
            VersionNumber = versionNumber;
        }
    }
}
