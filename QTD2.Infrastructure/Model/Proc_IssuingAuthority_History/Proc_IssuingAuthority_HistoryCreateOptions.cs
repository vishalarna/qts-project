using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.IssuingAuthorityStatusHistory
{
    public class Proc_IssuingAuthority_HistoryCreateOptions
    {
        public int IssuingAuthorityId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }
    }
}
