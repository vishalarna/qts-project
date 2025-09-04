using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.CertifyingBody
{
    public class CertifyingBodyOptions
    {
        public int CertifyingBodyID { get; set; }

        public string ActionType { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
