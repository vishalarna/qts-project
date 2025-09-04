using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Certification
{
    public class CertificationCompactOptions
    {
        public CertificationCompactOptions (int id, string certAcronym, int certifyingbodyID, string name, bool active, bool? hasLinks)
        {
            Id = id;
            CertAcronym = certAcronym;
            CertifyingBodyID = certifyingbodyID;
            Name = name;
            Active = active;
            this.hasLinks = hasLinks;
        }

        public int Id { get; set; }
        public string CertAcronym { get; set; }
        public int CertifyingBodyID { get; set; }
        public string Name { get; set; }
        public bool? hasLinks { get; set; }
        public bool Active { get; set; }
    }
}
