using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Infrastructure.Model.ILA_Topic
{
    public class ILA_TopicVM
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public bool IsPriority { get; set; }

        public string Name { get; set; }

        public int ILACount { get; set; }
    }
}
