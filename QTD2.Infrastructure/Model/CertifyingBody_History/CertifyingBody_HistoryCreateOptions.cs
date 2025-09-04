using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.CertifyingBody_History
{
    public class CertifyingBody_HistoryCreateOptions
    {
        public int CertifyingBodyID { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string Notes { get; set; }

        public string ActionType { get; set; }
    }
}
