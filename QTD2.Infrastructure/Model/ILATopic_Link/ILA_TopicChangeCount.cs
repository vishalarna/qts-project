using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_Topic_Link
{
    public class ILA_TopicChangeCount
    {
        public QTD2.Domain.Entities.Core.ILA ILA { get; set; }
        public int TopicsRemoved { get; set; }
        public int TopicsAdded { get; set; }

    }
}
