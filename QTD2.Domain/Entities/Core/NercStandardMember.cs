using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class NercStandardMember : Entity
    {
        public int StdId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public virtual NercStandard NercStandard { get; set; }

        public virtual ICollection<ILA_NercStandard_Link> ILA_NercStandard_Links { get; set; } = new List<ILA_NercStandard_Link>();

        public NercStandardMember(int stdId, string name, string type)
        {
            StdId = stdId;
            Name = name;
            Type = type;
        }

        public NercStandardMember()
        {
        }
    }
}
