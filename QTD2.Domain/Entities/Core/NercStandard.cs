using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class NercStandard : Entity
    {
        public string Name { get; set; }

        public bool IsNercStandard { get; set; }

        public bool IsUserDefined { get; set; }

        public virtual ICollection<ILA_NercStandard_Link> ILA_NercStandard_Links { get; set; } = new List<ILA_NercStandard_Link>();

        public virtual ICollection<NercStandardMember> NercStandardMembers { get; set; } = new List<NercStandardMember>();

        public NercStandard(string name, bool isUserDefined, bool isNercStandard)
        {
            Name = name;
            IsUserDefined = isUserDefined;
            IsNercStandard = isNercStandard;
        }

        public NercStandard()
        {
        }
    }
}
