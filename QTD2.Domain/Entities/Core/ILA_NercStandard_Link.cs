using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_NercStandard_Link : Entity
    {
        public int ILAId { get; set; }

        public int StdId { get; set; }

        public int NERCStdMemberId { get; set; }

        public float CreditHoursByStd { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual NercStandard NercStandard { get; set; }

        public virtual NercStandardMember NercStandardMember { get; set; }

        public ILA_NercStandard_Link(ILA ila, NercStandard std, NercStandardMember nercStandardMember, float creditHoursByStd)
        {
            ILAId = ila.Id;
            StdId = std.Id;
            NERCStdMemberId = nercStandardMember.Id;
            ILA = ila;
            NercStandard = std;
            NercStandardMember = nercStandardMember;
            CreditHoursByStd = creditHoursByStd;
        }

        public ILA_NercStandard_Link()
        {
        }
    }
}
