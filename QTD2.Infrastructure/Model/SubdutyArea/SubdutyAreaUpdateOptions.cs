using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SubdutyArea
{
    public class SubdutyAreaUpdateOptions
    {
        public string Description { get; set; }

        public int SubNumber { get; set; }

        public string Title { get; set; }

        public string ReasonForRevision { get; set; }

        public DateTime EffectiveDate { get; set; }

        public int SdaId { get; set; }
    }
}
