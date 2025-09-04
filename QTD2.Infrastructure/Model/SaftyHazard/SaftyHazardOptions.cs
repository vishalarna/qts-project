using System;

namespace QTD2.Infrastructure.Model.SaftyHazard
{
    public class SaftyHazardOptions
    {
        public int[] SaftyHazardIds { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string ActionType { get; set; }

        public int SaftyHazardId { get; set; }
    }
}
