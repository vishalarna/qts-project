using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_MetaEO_Link : Entity
    {
        public int MetaEOId { get; set; }

        public int EOID { get; set; }

        public virtual EnablingObjective MetaEO { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public EnablingObjective_MetaEO_Link()
        {
        }

        public EnablingObjective_MetaEO_Link(EnablingObjective metaEO,EnablingObjective eo)
        {
            MetaEOId = metaEO.Id;
            EOID = eo.Id;
            MetaEO = metaEO;
            eo = EnablingObjective;
        }
    }
}
