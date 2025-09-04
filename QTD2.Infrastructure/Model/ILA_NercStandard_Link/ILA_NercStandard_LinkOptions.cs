using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_NercStandard_Link
{
    public class ILA_NercStandard_LinkOptions
    {
        public int ILAId { get; set; }

        public int StdId { get; set; }

        public int NERCStdMemberId { get; set; }

        public float CreditHoursByStd { get; set; }

        public List<NercStdValues> NercStdValues { get; set; }
    }

    public class NercStdValues
    {
        public int NERCStdMemberId { get; set; }

        public float CreditHoursByStd { get; set; }
    }
}
