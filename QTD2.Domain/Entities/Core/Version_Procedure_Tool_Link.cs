namespace QTD2.Domain.Entities.Core
{
    public class Version_Procedure_Tool_Link : Common.Entity
    {
        public int Version_ProcedureId { get; set; }

        public int Version_ToolId { get; set; }

        public virtual Version_Procedure Version_Procedure { get; set; }

        public virtual Version_Tool Version_Tool { get; set; }

        public Version_Procedure_Tool_Link()
        {
        }

        public Version_Procedure_Tool_Link(int version_ProcedureId, int version_ToolId)
        {
            Version_ProcedureId = version_ProcedureId;
            Version_ToolId = version_ToolId;
        }
    }
}
