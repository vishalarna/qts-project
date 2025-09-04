using System;

namespace QTD2.Infrastructure.Model.DutyArea
{
    public class DutyAreaCreateOptions
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Letter { get; set; }

        public int Number { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string ReasonForRevision { get; set; }
    }
}
