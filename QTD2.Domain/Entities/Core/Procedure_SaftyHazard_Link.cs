namespace QTD2.Domain.Entities.Core
{
    public class Procedure_SaftyHazard_Link : Common.Entity
    {
        public int ProcedureId { get; set; }

        public int SaftyHazardId { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual SaftyHazard SaftyHazard { get; set; }

        public Procedure_SaftyHazard_Link(Procedure procedure, SaftyHazard saftyHazard)
        {
            ProcedureId = procedure.Id;
            SaftyHazardId = saftyHazard.Id;
            Procedure = procedure;
            SaftyHazard = saftyHazard;
        }

        public Procedure_SaftyHazard_Link()
        {
        }
    }
}
