using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_Topic : Entity
    {
        public bool IsPriority { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ILA_Topic_Link> ILA_Topic_Links { get; set; } = new List<ILA_Topic_Link>();

        public ILA_Topic(bool isPriority, string name)
        {
            IsPriority = isPriority;
            Name = name;
        }

        public ILA_Topic()
        {
        }
    }
}
