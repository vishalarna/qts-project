namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_Procedure_Link : Common.Entity
    {
        public int Version_TaskId { get; set; }

        public int Version_ProcedureId { get; set; }

        public string VersionNumber { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public virtual Version_Procedure Version_Procedure { get; set; }

        public Version_Task_Procedure_Link()
        {
        }

        public Version_Task_Procedure_Link(int version_TaskId, int version_ProcedureId, string versionNumber = "1.0")
        {
            Version_TaskId = version_TaskId;
            Version_ProcedureId = version_ProcedureId;
            VersionNumber = versionNumber;
        }
    }
}
