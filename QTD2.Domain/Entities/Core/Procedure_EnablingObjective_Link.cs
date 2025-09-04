namespace QTD2.Domain.Entities.Core
{
    public class Procedure_EnablingObjective_Link : Common.Entity
    {
        public int ProcedureId { get; set; }

        public int EnablingObjectiveId { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public Procedure_EnablingObjective_Link(Procedure procedure, EnablingObjective enablingObjective)
        {
            ProcedureId = procedure.Id;
            EnablingObjectiveId = enablingObjective.Id;
            Procedure = procedure;
            EnablingObjective = enablingObjective;
        }

        public Procedure_EnablingObjective_Link()
        {
        }
    }
}
