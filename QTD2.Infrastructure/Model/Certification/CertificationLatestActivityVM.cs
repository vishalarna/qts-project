using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Certification
{
    public class CertificationLatestActivityVM
    {
        public int CertId { get; set; }
        public int CertifyingBodyID { get; set; }
        public string CertAcronym { get; set; }
        public string Name { get; set; }
        public string ActivityDesc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

    }
}
