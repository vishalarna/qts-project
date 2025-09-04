using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_Topic_Link : Entity
    {
        public int ILAId { get; set; }

        public int ILATopicId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual ILA_Topic ILA_Topic { get; set; }

        public ILA_Topic_Link(ILA ila, ILA_Topic ilaTopic)
        {
            ILAId = ila.Id;
            ILATopicId = ilaTopic.Id;
            ILA = ila;
            ILA_Topic = ilaTopic;
        }

        public ILA_Topic_Link()
        {
        }
    }
}
