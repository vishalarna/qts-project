using System;

namespace QTD2.Infrastructure.Model.SubdutyArea
{
    public class SubdutyAreaCreateOptions
    {
        public string Description { get; set; }

        public int SubNumber { get; set; }

        public string Title { get; set; }

        public string ReasonForRevision { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
