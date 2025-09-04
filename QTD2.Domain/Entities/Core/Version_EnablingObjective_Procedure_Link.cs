namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_Procedure_Link : Common.Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_ProcedureId { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Version_Procedure Version_Procedure { get; set; }

        public Version_EnablingObjective_Procedure_Link()
        {
        }

        public Version_EnablingObjective_Procedure_Link(int version_EnablingObjectiveId, int version_ProcedureId)
        {
            Version_EnablingObjectiveId = version_EnablingObjectiveId;
            Version_ProcedureId = version_ProcedureId;
        }
    }
}
