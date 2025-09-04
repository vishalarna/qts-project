using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class NERCTargetAudience : Entity
    {
        public string Name { get; set; }

        public bool IsOther { get; set; }

        public string OtherName { get; set; }

        public virtual ICollection<ILA_NERCAudience_Link> ILA_NERCAudience_Links { get; set; } = new List<ILA_NERCAudience_Link>();

        public NERCTargetAudience(string name, bool isOther, string otherName)
        {
            Name = name;
            OtherName = otherName;
            IsOther = isOther;
        }

        public NERCTargetAudience()
        {
        }
    }
}
