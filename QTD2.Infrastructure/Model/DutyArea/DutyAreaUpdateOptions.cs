using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DutyArea
{
    public class DutyAreaUpdateOptions
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Letter { get; set; }

        public int Number { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string ReasonForRevision { get; set; }
    }
}
