using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Certification_History
{
    public class Certification_HistoryCreateOptions
    {
        public int CertId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string Notes { get; set; }

        public string ActionType { get; set; }
        public int[] certIds { get; set; }
    }
}
