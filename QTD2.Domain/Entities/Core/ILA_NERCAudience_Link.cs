using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_NERCAudience_Link : Entity
    {
        public int ILAId { get; set; }

        public int NERCAudienceID { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual NERCTargetAudience NERCTargetAudience { get; set; }

        public ILA_NERCAudience_Link(ILA ila, NERCTargetAudience nercTargetAudience)
        {
            ILA = ila;
            NERCTargetAudience = nercTargetAudience;
            ILAId = ila.Id;
            NERCAudienceID = nercTargetAudience.Id;
        }

        public ILA_NERCAudience_Link()
        {
        }
    }
}
