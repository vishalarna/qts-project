using System;

namespace QTD2.Infrastructure.Model.Procedure_SaftyHazard_Link
{
    public class Procedure_SaftyHazard_LinkOptions
    {
        public int[] SaftyHazardIds { get; set; }

        public bool IsSignificant { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime? EffectiveDate { get; set; }
    }
}
